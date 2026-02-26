using APICalculos.Application.DTOs.Store;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entities;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;

namespace APICalculos.Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StoreResponseDTO>> GetAllAsync()
        {
            var stores = await _unitOfWork.Stores.GetAllAsync();
            return _mapper.Map<IEnumerable<StoreResponseDTO>>(stores);
        }

        public async Task<StoreResponseDTO?> GetByIdAsync(int id)
        {
            var store = await _unitOfWork.Stores.GetByIdAsync(id);

            if (store == null)
                return null;

            return _mapper.Map<StoreResponseDTO>(store);
        }

        public async Task<StoreResponseDTO> CreateAsync(StoreCreateDto dto)
        {
            var store = _mapper.Map<Store>(dto);

            await _unitOfWork.Stores.AddAsync(store);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<StoreResponseDTO>(store);
        }

        public async Task<bool> UpdateAsync(int id, StoreUpdateDTO dto)
        {
            var store = await _unitOfWork.Stores.GetByIdAsync(id);

            if (store == null)
                return false;

            _mapper.Map(dto, store);

            _unitOfWork.Stores.Update(store);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var store = await _unitOfWork.Stores.GetByIdAsync(id);

            if (store == null)
                return false;

            _unitOfWork.Stores.Delete(store);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }

}
