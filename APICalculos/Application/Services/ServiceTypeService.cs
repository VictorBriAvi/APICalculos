using APICalculos.Application.DTOs.Services;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICalculos.Application.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceTypeService(
            IServiceTypeRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ServiceTypeDTO>> GetAllServiceTypesAsync(
            int storeId,
            string? search,
            int? serviceCategorieId)
        {
            var entities =
                await _repository.GetAllAsync(storeId, search, serviceCategorieId);

            return _mapper.Map<List<ServiceTypeDTO>>(entities);
        }

        public async Task<ServiceTypeDTO?> GetServiceTypeForId(int id, int storeId)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);
            return entity == null ? null : _mapper.Map<ServiceTypeDTO>(entity);
        }

        public async Task<List<ServiceTypeDTO>> SearchServicesAsync(
            int storeId,
            int? categoryId = null)
        {
            Expression<Func<ServiceType, bool>> filter = s => true;

            if (categoryId.HasValue)
                filter = s => s.ServiceCategorieId == categoryId.Value;

            var results = await _repository.SearchAsync(storeId, filter);

            return _mapper.Map<List<ServiceTypeDTO>>(results);
        }

        public async Task<ServiceTypeDTO> AddServiceTypeAsync(
            int storeId,
            ServiceTypeCreationDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("El nombre no puede estar vacío");

            var exists = await _repository.ExistsByNameAsync(dto.Name, storeId);
            if (exists)
                throw new InvalidOperationException("Ya existe un servicio con ese nombre");

            var entity = _mapper.Map<ServiceType>(dto);
            entity.StoreId = storeId;

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceTypeDTO>(entity);
        }

        public async Task UpdateServiceTypeAsync(
            int id,
            int storeId,
            ServiceTypeCreationDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);

            if (entity == null)
                throw new KeyNotFoundException("Tipo de servicio no encontrado");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                entity.Name = dto.Name;

            if (dto.Price > 0)
                entity.Price = dto.Price;

            if (dto.ServiceCategorieId != 0)
                entity.ServiceCategorieId = dto.ServiceCategorieId;

            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteServiceTypeAsync(int id, int storeId)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);

            if (entity == null)
                throw new KeyNotFoundException("Tipo de servicio no encontrado");

            try
            {
                _repository.Remove(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (
                ex.InnerException is SqlException sqlEx &&
                sqlEx.Number == 547)
            {
                throw new InvalidOperationException(
                    "No se puede eliminar este tipo de servicio porque está asociado a una venta.");
            }
        }

        public async Task<List<ServicesSearchDTO>> SearchServiceAsync(
            int storeId,
            string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<ServicesSearchDTO>();

            var results =
                await _repository.SearchAsync(storeId, query.Trim(), 15);

            return _mapper.Map<List<ServicesSearchDTO>>(results);
        }
    }
}
