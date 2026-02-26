using APICalculos.Application.DTOs;
using APICalculos.Application.DTOs.CustomerHistory;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;

namespace APICalculos.Application.Services
{
    public class CustomerHistoryService : ICustomerHistoryService
    {
        private readonly ICustomerHistoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerHistoryService(
            ICustomerHistoryRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CustomerHistoryDTO>> GetAllCustomerHistoriesAsync(int storeId)
        {
            var histories = await _repository.GetAllAsync(storeId);
            return _mapper.Map<List<CustomerHistoryDTO>>(histories);
        }

        public async Task<CustomerHistoryDTO?> GetCustomerHistoryForId(int id, int storeId)
        {
            var history = await _repository.GetByIdAsync(id, storeId);
            if (history == null)
                return null;

            return _mapper.Map<CustomerHistoryDTO>(history);
        }

        public async Task<CustomerHistoryDTO> AddCustomerHistoryAsync(int storeId, CustomerHistoryCreationDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                throw new ArgumentException("El título es obligatorio.");

            var exists = await _repository.ExistsByTitleAsync(dto.Title, storeId);
            if (exists)
                throw new InvalidOperationException("El título ya existe en esta tienda.");

            var entity = _mapper.Map<CustomerHistory>(dto);

            entity.DateHistory = DateTime.UtcNow;
            entity.StoreId = storeId;

            await _unitOfWork.CustomerHistory.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CustomerHistoryDTO>(entity);
        }

        public async Task UpdateCustomerHistoryAsync(int id, int storeId, CustomerHistoryUpdateDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);

            if (entity == null)
                throw new KeyNotFoundException("Historial no encontrado");

            if (!string.IsNullOrWhiteSpace(dto.Title))
                entity.Title = dto.Title;

            if (!string.IsNullOrWhiteSpace(dto.Description))
                entity.Description = dto.Description;

            if (dto.ClientId > 0)
            {
                entity.Client = null;
                entity.ClientId = dto.ClientId;
            }

            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCustomerHistoriesAsync(int id, int storeId)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);

            if (entity == null)
                throw new KeyNotFoundException("Historial no encontrado");

            _repository.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
