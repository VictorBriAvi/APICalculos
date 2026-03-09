using APICalculos.Application.DTOs.Sale;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Domain.Entities;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;

namespace APICalculos.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SaleService(
            ISaleRepository saleRepository,
            IPaymentTypeRepository paymentTypeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _saleRepository = saleRepository;
            _paymentTypeRepository = paymentTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SaleDTO>> GetAllSaleAsync(int storeId)
        {
            var sales = await _saleRepository.GetAllAsync(storeId);
            return _mapper.Map<List<SaleDTO>>(sales);
        }

        public async Task<List<SaleDTO>> GetFilteredSalesAsync(
            int storeId,
            DateTime? fromDate, DateTime? toDate,
            int? clientId, int? paymentTypeId,
            int? employeeId, int? serviceTypeId)
        {
            if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
                throw new ArgumentException("Rango de fechas inválido.");

            if (fromDate.HasValue) fromDate = fromDate.Value.Date;
            if (toDate.HasValue) toDate = toDate.Value.Date.AddDays(1).AddTicks(-1);

            var sales = await _saleRepository.GetFilteredAsync(
                storeId, fromDate, toDate, clientId, paymentTypeId, employeeId, serviceTypeId);

            return _mapper.Map<List<SaleDTO>>(sales);
        }

        public async Task<SaleDTO?> GetSaleForId(int id, int storeId)
        {
            var sale = await _saleRepository.GetByIdAsync(id, storeId);
            return sale is null ? null : _mapper.Map<SaleDTO>(sale);
        }

        public async Task<SaleDTO> AddSaleWithDetailsAsync(int storeId, SaleCreationDTO dto)
        {
            // ── 1. Cargar todos los tipos de pago de una sola vez ─────────────────
            var paymentTypes = new Dictionary<int, PaymentTypes>();
            foreach (var p in dto.Payments)
            {
                if (!paymentTypes.ContainsKey(p.PaymentTypeId))
                {
                    var pt = await _paymentTypeRepository.GetByIdAsync(p.PaymentTypeId, storeId)
                        ?? throw new KeyNotFoundException($"Tipo de pago no encontrado: {p.PaymentTypeId}");
                    paymentTypes[p.PaymentTypeId] = pt;
                }
            }

            // ── 2. Recargo: pertenece a la VENTA, no al pago individual ──────────
            //    Se aplica el MAYOR porcentaje entre todos los medios de pago usados.
            //    El recargo lo paga el CLIENTE y sube el TotalAmount de la venta.
            var surchargePercent = paymentTypes.Values
                .Where(pt => pt.ApplySurcharge)
                .Select(pt => pt.SurchargePercent)
                .DefaultIfEmpty(0m)
                .Max();

            // BaseAmount = suma de los AmountPaid del frontend
            // (el frontend ya calculó el total efectivo y distribuyó los montos)
            var baseAmount = dto.Payments.Sum(p => p.AmountPaid);
            var surchargeAmount = baseAmount * (surchargePercent / 100m);
            var totalAmount = baseAmount + surchargeAmount;

            // ── 3. Construir la venta ─────────────────────────────────────────────
            var sale = new Sale
            {
                StoreId = storeId,
                DateSale = DateTime.Now,
                ClientId = dto.ClientId,
                SaleDetail = new List<SaleDetail>(),
                Payments = new List<SalePayment>(),

                // Recargo a nivel venta
                SurchargePercent = surchargePercent,
                SurchargeAmount = surchargeAmount,
                // TotalAmount lo setea CalculateTotal() pero lo pre-seteamos por claridad
            };

            foreach (var detail in dto.SaleDetails)
            {
                sale.SaleDetail.Add(new SaleDetail
                {
                    ServiceTypeId = detail.ServiceTypeId,
                    EmployeeId = detail.EmployeeId,
                    UnitPrice = detail.UnitPrice,
                    AdditionalCharge = detail.AdditionalCharge,
                    DiscountPercent = detail.DiscountPercent,
                    StoreId = storeId
                });
            }

            // ── 4. Pagos: solo registrar el descuento que cobra la app ────────────
            //    El descuento (ej: 3% débito) es lo que la app le descuenta al NEGOCIO.
            //    No tiene nada que ver con el recargo que pagó el cliente.
            foreach (var payment in dto.Payments)
            {
                var paymentType = paymentTypes[payment.PaymentTypeId];

                var appDiscountPercent = paymentType.ApplyDiscount ? paymentType.DiscountPercent : 0m;
                var appDiscountAmount = payment.AmountPaid * (appDiscountPercent / 100m);
                var netAmountReceived = payment.AmountPaid - appDiscountAmount;

                sale.Payments.Add(new SalePayment
                {
                    PaymentTypeId = payment.PaymentTypeId,
                    AmountPaid = payment.AmountPaid,      // lo que entró por este medio
                    AppDiscountPercent = appDiscountPercent,       // lo que cobra la app (ej: 3%)
                    AppDiscountAmount = appDiscountAmount,        // $372 si pagó $12.400 con débito
                    NetAmountReceived = netAmountReceived,        // lo que el negocio realmente cobra
                    PaymentDate = DateTime.Now,
                    StoreId = storeId
                });
            }

            sale.CalculateTotal(); // suma SaleDetails + SurchargeAmount
            await _saleRepository.AddAsync(sale);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task UpdateSaleAsync(int id, int storeId, SaleCreationDTO dto)
        {
            var saleDB = await _saleRepository.GetByIdAsync(id, storeId)
                ?? throw new KeyNotFoundException("Venta no encontrada");

            var paymentTypes = new Dictionary<int, PaymentTypes>();
            foreach (var p in dto.Payments)
            {
                if (!paymentTypes.ContainsKey(p.PaymentTypeId))
                {
                    var pt = await _paymentTypeRepository.GetByIdAsync(p.PaymentTypeId, storeId)
                        ?? throw new KeyNotFoundException($"Tipo de pago no encontrado: {p.PaymentTypeId}");
                    paymentTypes[p.PaymentTypeId] = pt;
                }
            }

            var surchargePercent = paymentTypes.Values
                .Where(pt => pt.ApplySurcharge)
                .Select(pt => pt.SurchargePercent)
                .DefaultIfEmpty(0m)
                .Max();

            var baseAmount = dto.Payments.Sum(p => p.AmountPaid);
            var surchargeAmount = baseAmount * (surchargePercent / 100m);

            saleDB.ClientId = dto.ClientId;
            saleDB.SurchargePercent = surchargePercent;
            saleDB.SurchargeAmount = surchargeAmount;

            saleDB.Payments.Clear();
            foreach (var paymentDTO in dto.Payments)
            {
                var paymentType = paymentTypes[paymentDTO.PaymentTypeId];
                var appDiscountPercent = paymentType.ApplyDiscount ? paymentType.DiscountPercent : 0m;
                var appDiscountAmount = paymentDTO.AmountPaid * (appDiscountPercent / 100m);
                var netAmountReceived = paymentDTO.AmountPaid - appDiscountAmount;

                saleDB.Payments.Add(new SalePayment
                {
                    PaymentTypeId = paymentDTO.PaymentTypeId,
                    AmountPaid = paymentDTO.AmountPaid,
                    AppDiscountPercent = appDiscountPercent,
                    AppDiscountAmount = appDiscountAmount,
                    NetAmountReceived = netAmountReceived,
                    PaymentDate = DateTime.Now,
                    StoreId = storeId
                });
            }

            saleDB.CalculateTotal();
            _saleRepository.Update(saleDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteSaleAsync(int id, int storeId)
        {
            var saleDB = await _saleRepository.GetByIdAsync(id, storeId)
                ?? throw new KeyNotFoundException("Venta no encontrada");

            _saleRepository.Remove(saleDB);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}