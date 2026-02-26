using APICalculos.Application.DTOs.ServiceCategories;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Application.Services
{
    public class ServiceCategoriesService : IServiceCategoriesService
    {
        private readonly IServiceCategoriesRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceCategoriesService(
            IServiceCategoriesRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ServiceCategoriesDTO>> GetAllServiceCategoriesAsync(int storeId, string? search)
        {
            var entities = await _repository.GetAllAsync(storeId, search);
            return _mapper.Map<List<ServiceCategoriesDTO>>(entities);
        }

        public async Task<ServiceCategoriesDTO?> GetServiceCategorieForId(int id, int storeId)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);
            return entity == null ? null : _mapper.Map<ServiceCategoriesDTO>(entity);
        }

        public async Task<ServiceCategoriesDTO> AddServiceCategorieAsync(int storeId, ServiceCategoriesCreationDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("El nombre de la categoría no puede estar vacío");

            var exists = await _repository.ExistsByNameAsync(dto.Name, storeId);
            if (exists)
                throw new InvalidOperationException("Ya existe una categoría con ese nombre");

            var entity = _mapper.Map<ServiceCategorie>(dto);
            entity.StoreId = storeId;

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceCategoriesDTO>(entity);
        }

        public async Task UpdateServiceCategorieAsync(int id, int storeId, ServiceCategoriesCreationDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);

            if (entity == null)
                throw new KeyNotFoundException("Categoría de servicio no encontrada");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                entity.Name = dto.Name;

            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteServiceCategorieAsync(int id, int storeId)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);

            if (entity == null)
                throw new KeyNotFoundException("Categoría de servicio no encontrada");

            try
            {
                _repository.Remove(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (
                ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                throw new InvalidOperationException(
                    "No se puede eliminar esta categoría porque está asociada a un servicio.");
            }
        }
    }
}
