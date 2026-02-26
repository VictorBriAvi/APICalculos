using APICalculos.Application.DTOs.SaleDetail;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;

namespace APICalculos.Application.Services
{
    public class SaleDetailService : ISaleDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IMapper _mapper;

        public SaleDetailService(
            IUnitOfWork unitOfWork,
            IServiceTypeService serviceTypeService,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _serviceTypeService = serviceTypeService;
            _mapper = mapper;
        }

        public async Task<List<SaleDetailDTO>> GetAllSaleDetailAsync(int storeId)
        {
            var salesDetails = await _unitOfWork.SaleDetail.GetAllAsync(storeId);
            return _mapper.Map<List<SaleDetailDTO>>(salesDetails);
        }

        public async Task<SaleDetailDTO> GetSaleDetailForId(int id, int storeId)
        {
            var saleDetail = await _unitOfWork.SaleDetail.GetByIdAsync(id, storeId);

            if (saleDetail == null)
                return null;

            return _mapper.Map<SaleDetailDTO>(saleDetail);
        }

        public async Task<SaleDetailDTO> AddSaleDetailAsync(SaleDetailCreationDTO dto, int storeId)
        {
            var saleDetail = _mapper.Map<SaleDetail>(dto);
            saleDetail.StoreId = storeId;

            await _unitOfWork.SaleDetail.AddAsync(saleDetail);
            await _unitOfWork.SaveChangesAsync();

            await RecalculateSaleTotalAsync(saleDetail.SaleId, storeId);

            return _mapper.Map<SaleDetailDTO>(saleDetail);
        }

        public async Task UpdateSaleDetailAsync(int id, SaleDetailCreationDTO dto, int storeId)
        {
            var saleDetailDB = await _unitOfWork.SaleDetail.GetByIdAsync(id, storeId);

            if (saleDetailDB == null)
                throw new KeyNotFoundException("Detalle Venta no encontrado.");

            if (dto.ServiceTypeId > 0 && dto.ServiceTypeId != saleDetailDB.ServiceTypeId)
            {
                var serviceTypeDB = await _serviceTypeService.GetServiceTypeForId(dto.ServiceTypeId, storeId);

                saleDetailDB.ServiceType = null;
                saleDetailDB.ServiceTypeId = serviceTypeDB.Id;
                saleDetailDB.UnitPrice = serviceTypeDB.Price;
            }

            if (dto.EmployeeId > 0)
            {
                saleDetailDB.Employee = null;
                saleDetailDB.EmployeeId = dto.EmployeeId;
            }

            saleDetailDB.DiscountPercent = dto.DiscountPercent;
            saleDetailDB.AdditionalCharge = dto.AdditionalCharge;

            _unitOfWork.SaleDetail.Update(saleDetailDB);
            await _unitOfWork.SaveChangesAsync();

            await RecalculateSaleTotalAsync(saleDetailDB.SaleId, storeId);
        }

        public async Task DeleteSaleDetailAsync(int id, int storeId)
        {
            var saleDetailDB = await _unitOfWork.SaleDetail.GetByIdAsync(id, storeId);

            if (saleDetailDB == null)
                throw new KeyNotFoundException("Detalle Venta no encontrado.");

            int saleId = saleDetailDB.SaleId;

            _unitOfWork.SaleDetail.Remove(saleDetailDB);
            await _unitOfWork.SaveChangesAsync();

            await RecalculateSaleTotalAsync(saleId, storeId);
        }

        private async Task RecalculateSaleTotalAsync(int saleId, int storeId)
        {
            var saleDetails = await _unitOfWork.SaleDetail.GetBySaleIdAsync(saleId, storeId);

            var total = saleDetails.Sum(d =>
                (d.UnitPrice + d.AdditionalCharge) * (1 - (d.DiscountPercent / 100))
            );

            var sale = await _unitOfWork.Sale.GetByIdAsync(saleId, storeId);

            if (sale == null)
                throw new KeyNotFoundException("Venta no encontrada.");

            sale.TotalAmount = total;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
