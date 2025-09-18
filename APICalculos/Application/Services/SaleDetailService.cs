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
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SaleDetailService(ISaleDetailRepository saleDetailRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _saleDetailRepository = saleDetailRepository;
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

            if (saleDetailCreationDTO.Id > 0)
                saleDetailDB.Id = saleDetailCreationDTO.Id;

            if (saleDetailCreationDTO.ServiceTypeId > 0)
                saleDetailDB.ServiceTypeId = saleDetailCreationDTO.ServiceTypeId;

            if (saleDetailCreationDTO.EmployeeId > 0)
                saleDetailDB.EmployeeId = saleDetailCreationDTO.EmployeeId;

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
