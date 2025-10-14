using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;

namespace APICalculos.Application.Services
{
    public class SaleDetailService : ISaleDetailService
    {
        private readonly ISaleDetailRepository _saleDetailRepository;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SaleDetailService(ISaleDetailRepository saleDetailRepository, IMapper mapper, IUnitOfWork unitOfWork, IServiceTypeService serviceTypeService)
        {
            _saleDetailRepository = saleDetailRepository;
            _serviceTypeService = serviceTypeService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<SaleDetailDTO>> GetAllSaleDetailAsync()
        {
            var salesDetails = await _saleDetailRepository.GetAllAsync();
            return _mapper.Map<List<SaleDetailDTO>>(salesDetails);
        }

        public async Task<SaleDetailDTO> GetSaleDetailForId (int id)
        {
            var saleDetail = await _saleDetailRepository.GetByIdAsync(id);
            
            if(saleDetail == null)
                return null;

            return _mapper.Map<SaleDetailDTO>(saleDetail);
        }

        public async Task<SaleDetailDTO> AddSaleDetailAsync(SaleDetailCreationDTO saleDetailCreationDTO)
        {
            var saleDetail = _mapper.Map<SaleDetail>(saleDetailCreationDTO);

            await _unitOfWork.SaleDetail.AddAsync(saleDetail);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SaleDetailDTO>(saleDetail);
        }

        public async Task UpdateSaleDetailAsync(int id, SaleDetailCreationDTO saleDetailCreationDTO)
        {
            var saleDetailDB = await _saleDetailRepository.GetByIdAsync(id);
            if (saleDetailDB == null)
                throw new KeyNotFoundException("Detalle Venta no encontrado");

            // Actualizar ServiceType solo si se envió un cambio
            if (saleDetailCreationDTO.ServiceTypeId > 0 && saleDetailCreationDTO.ServiceTypeId != saleDetailDB.ServiceTypeId)
            {
                var serviceTypeDB = await _serviceTypeService.GetServiceTypeForId(saleDetailCreationDTO.ServiceTypeId);
                if (serviceTypeDB == null)
                    throw new KeyNotFoundException("Tipo de servicio no encontrado");

                saleDetailDB.ServiceType = null; // desasocia navegación
                saleDetailDB.ServiceTypeId = serviceTypeDB.Id;
                saleDetailDB.UnitPrice = serviceTypeDB.Price; // actualizar precio
            }

            // Actualizar Employee solo si se envió un cambio
            if (saleDetailCreationDTO.EmployeeId > 0 && saleDetailCreationDTO.EmployeeId != saleDetailDB.EmployeeId)
            {
                saleDetailDB.Employee = null;
                saleDetailDB.EmployeeId = saleDetailCreationDTO.EmployeeId;
            }

            // Ediciones parciales de valores numéricos
            if (saleDetailCreationDTO.DiscountPercent >= 0)
                saleDetailDB.DiscountPercent = saleDetailCreationDTO.DiscountPercent;

            if (saleDetailCreationDTO.AdditionalCharge >= 0)
                saleDetailDB.AdditionalCharge = saleDetailCreationDTO.AdditionalCharge;

            _saleDetailRepository.Update(saleDetailDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteSaleDetailAsync(int id)
        {
            var saleDetailDB = await _saleDetailRepository.GetByIdAsync(id);

            if (saleDetailDB == null)
                throw new KeyNotFoundException("No se encuenta detalle venta");

            _saleDetailRepository.Remove(saleDetailDB);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
