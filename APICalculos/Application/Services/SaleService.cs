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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SaleService(
            ISaleRepository saleRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // =========================================
        // GET ALL
        // =========================================
        public async Task<List<SaleDTO>> GetAllSaleAsync(int storeId)
        {
            var sales = await _saleRepository.GetAllAsync(storeId);
            return _mapper.Map<List<SaleDTO>>(sales);
        }

        // =========================================
        // RESUMEN AGRUPADO POR DIA
        // =========================================
        //public async Task<List<SaleDTO>> GetSalesByDateRangeAsync(
        //    int storeId,
        //    DateTime fromDate,
        //    DateTime toDate)
        //{
        //    if (fromDate > toDate)
        //        throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin.");

        //    fromDate = fromDate.Date;
        //    toDate = toDate.Date.AddDays(1).AddTicks(-1);

        //    var sales = await _saleRepository
        //        .GetByDateRangeAsync(fromDate, toDate, storeId);

        //    var mappedSales = _mapper.Map<List<SaleDTO>>(sales);

        //    foreach (var sale in mappedSales)
        //    {
        //        foreach (var detail in sale.SaleDetail)
        //        {
        //            var baseAmount = detail.UnitPrice + detail.AdditionalCharge;
        //            var discount = baseAmount * (detail.DiscountPercent / 100m);
        //            detail.TotalCalculated = baseAmount - discount;
        //        }

        //        sale.TotalAmount = sale.SaleDetail.Sum(d => d.TotalCalculated);
        //    }

        //    var groupedByDate = mappedSales
        //        .GroupBy(s => s.DateSale.Date)
        //        .Select(group => new SaleDTO
        //        {
        //            Id = 0,
        //            DateSale = group.Key,
        //            NameClient = "Resumen diario",
        //            ClientId = 0,
        //            SaleDetail = group.SelectMany(s => s.SaleDetail).ToList(),
        //            Payments = group.SelectMany(s => s.Payments).ToList(),
        //            TotalAmount = group.Sum(s => s.TotalAmount),
        //            IsDeleted = false
        //        })
        //        .OrderByDescending(s => s.DateSale)
        //        .ToList();

        //    return groupedByDate;
        //}

        // =========================================
        // LISTADO NORMAL POR RANGO
        // =========================================
        public async Task<List<SaleDTO>> GetFilteredSalesAsync(int storeId,DateTime? fromDate, DateTime? toDate,int? clientId,int? paymentTypeId,int? employeeId,int? serviceTypeId)
        {
            if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
                throw new ArgumentException("Rango de fechas inválido.");

            if (fromDate.HasValue)
                fromDate = fromDate.Value.Date;

            if (toDate.HasValue)
                toDate = toDate.Value.Date.AddDays(1).AddTicks(-1);

            var sales = await _saleRepository.GetFilteredAsync(storeId,fromDate,toDate,clientId,paymentTypeId,employeeId,serviceTypeId);

            return _mapper.Map<List<SaleDTO>>(sales);
        }

        // =========================================
        // GET BY ID
        // =========================================
        public async Task<SaleDTO?> GetSaleForId(int id, int storeId)
        {
            var sale = await _saleRepository.GetByIdAsync(id, storeId);
            if (sale == null)
                return null;

            return _mapper.Map<SaleDTO>(sale);
        }

        // =========================================
        // CREATE
        // =========================================
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
                sale.Payments.Add(new SalePayment
                {
                    PaymentTypeId = payment.PaymentTypeId,
                    AmountPaid = payment.AmountPaid,
                    PaymentDate = DateTime.Now,
                    StoreId = storeId
                    
                    
                });
            }

            sale.CalculateTotal();

            await _saleRepository.AddAsync(sale);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SaleDTO>(sale);
        }

        // =========================================
        // UPDATE
        // =========================================
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
                saleDB.Payments.Add(new SalePayment
                {
                    PaymentTypeId = paymentDTO.PaymentTypeId,
                    AmountPaid = paymentDTO.AmountPaid,
                    PaymentDate = DateTime.Now
                });
            }

            saleDB.CalculateTotal();

            _saleRepository.Update(saleDB);
            await _unitOfWork.SaveChangesAsync();
        }

        // =========================================
        // DELETE
        // =========================================
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
