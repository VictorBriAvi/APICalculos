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

        public async Task<List<SaleDTO>> GetFilteredSalesAsync(int storeId, DateTime? fromDate, DateTime? toDate, int? clientId, int? paymentTypeId, int? employeeId, int? serviceTypeId)
        {
            if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
                throw new ArgumentException("Rango de fechas inválido.");

            if (fromDate.HasValue)
                fromDate = fromDate.Value.Date;

            if (toDate.HasValue)
                toDate = toDate.Value.Date.AddDays(1).AddTicks(-1);

            var sales = await _saleRepository.GetFilteredAsync(storeId, fromDate, toDate, clientId, paymentTypeId, employeeId, serviceTypeId);

            return _mapper.Map<List<SaleDTO>>(sales);
        }

        public async Task<SaleDTO?> GetSaleForId(int id, int storeId)
        {
            var sale = await _saleRepository.GetByIdAsync(id, storeId);
            if (sale == null)
                return null;

            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task<SaleDTO> AddSaleWithDetailsAsync(
            int storeId,
            SaleCreationDTO dto)
        {
            var sale = new Sale
            {
                StoreId = storeId,
                DateSale = DateTime.Now,
                ClientId = dto.ClientId,
                SaleDetail = new List<SaleDetail>(),
                Payments = new List<SalePayment>()
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

            foreach (var payment in dto.Payments)
            {
                var paymentType = await _paymentTypeRepository.GetByIdAsync(payment.PaymentTypeId, storeId) ?? throw new KeyNotFoundException($"Tipo de pago no encontrado: {payment.PaymentTypeId}");

                var appliedDiscountPercent = paymentType.ApplyDiscount ? paymentType.DiscountPercent : 0;
                var discountAmount = payment.AmountPaid * (appliedDiscountPercent / 100m);
                var netAmount = payment.AmountPaid - discountAmount;

                sale.Payments.Add(new SalePayment
                {
                    PaymentTypeId = payment.PaymentTypeId,
                    AmountPaid = payment.AmountPaid,
                    AppliedDiscountPercent = appliedDiscountPercent,
                    DiscountAmount = discountAmount,
                    NetAmount = netAmount,
                    PaymentDate = DateTime.Now,
                    StoreId = storeId


                });
            }

            sale.CalculateTotal();

            await _saleRepository.AddAsync(sale);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SaleDTO>(sale);
        }

        public async Task UpdateSaleAsync(
            int id,
            int storeId,
            SaleCreationDTO dto)
        {
            var saleDB = await _saleRepository.GetByIdAsync(id, storeId);

            if (saleDB == null)
                throw new KeyNotFoundException("Venta no encontrada");

            saleDB.ClientId = dto.ClientId;

            saleDB.Payments.Clear();
            foreach (var paymentDTO in dto.Payments)
            {
                var paymentType = await _paymentTypeRepository.GetByIdAsync(paymentDTO.PaymentTypeId, storeId)
    ?? throw new KeyNotFoundException($"Tipo de pago no encontrado: {paymentDTO.PaymentTypeId}");

                var appliedDiscountPercent = paymentType.ApplyDiscount ? paymentType.DiscountPercent : 0;
                var discountAmount = paymentDTO.AmountPaid * (appliedDiscountPercent / 100m);
                var netAmount = paymentDTO.AmountPaid - discountAmount;

                saleDB.Payments.Add(new SalePayment
                {
                    PaymentTypeId = paymentDTO.PaymentTypeId,
                    AmountPaid = paymentDTO.AmountPaid,
                    AppliedDiscountPercent = appliedDiscountPercent,
                    DiscountAmount = discountAmount,
                    NetAmount = netAmount,
                    PaymentDate = DateTime.Now
                });
            }

            saleDB.CalculateTotal();

            _saleRepository.Update(saleDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteSaleAsync(int id, int storeId)
        {
            var saleDB = await _saleRepository.GetByIdAsync(id, storeId);

            if (saleDB == null)
                throw new KeyNotFoundException("Venta no encontrada");

            _saleRepository.Remove(saleDB);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
