using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Repositories;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace APICalculos.Application.Services
{
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IServiceCategoriesService _serviceCategoriesService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceTypeService(IServiceTypeRepository serviceTypeRepository, IMapper mapper, IUnitOfWork unitOfWork, IServiceCategoriesService serviceCategoriesService)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _serviceCategoriesService = serviceCategoriesService;
        }

        public async Task<List<ServiceTypeDTO>> GetAllServicesTypesAsync()
        {
            var servicesTypes = await _serviceTypeRepository.GetAllAsync();
            return _mapper.Map<List<ServiceTypeDTO>>(servicesTypes);
        }

        public async Task<ServiceTypeDTO> GetServiceTypeForId(int id)
        {
            var serviceType = await _serviceTypeRepository.GetByIdAsync(id);
            if (serviceType == null)
            {
                return null;
            }
            return _mapper.Map<ServiceTypeDTO>(serviceType);
        }

        public async Task<List<ServiceTypeDTO>> SearchServicesAsync(int? categoryId = null)
        {
            // En el futuro: podrías agregar más filtros (precio, nombre, etc.)
            Expression<Func<ServiceType, bool>> filter = s => true;

            if (categoryId.HasValue)
            {
                filter = s => s.ServiceCategorieId == categoryId.Value;
            }

            var results = await _serviceTypeRepository.SearchAsync(filter);

            return _mapper.Map<List<ServiceTypeDTO>>(results);
        }


        public async Task<ServiceTypeDTO> AddServiceTypeAsync(ServiceTypeCreationDTO serviceTypeCreationDTO)
        {
            if (string.IsNullOrWhiteSpace(serviceTypeCreationDTO.Name))
            {
                throw new ArgumentException("El nombre de tipo de servicio no puede estar vacio ");
            }

            var existName = await _serviceTypeRepository.ExistsByNameAsync(serviceTypeCreationDTO.Name);
            if (existName)
            {
                throw new InvalidOperationException("EL nombre del cliente ya existe");
            }

            var serviceType = _mapper.Map<ServiceType>(serviceTypeCreationDTO);

            await _unitOfWork.ServiceTypes.AddAsync(serviceType);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceTypeDTO>(serviceType);
               
        }

        public async Task UpdateServiceTypeAsync(int id, ServiceTypeCreationDTO serviceTypeCreationDTO)
        {
            var serviceTypeDB = await _serviceTypeRepository.GetByIdAsync(id);
            if (serviceTypeDB == null)
                throw new KeyNotFoundException("Tipo de servicio no encontrado");

            if (!string.IsNullOrWhiteSpace(serviceTypeCreationDTO.Name))
                serviceTypeDB.Name = serviceTypeCreationDTO.Name;

            if (serviceTypeCreationDTO.Price > 0) // sin ToString()
                serviceTypeDB.Price = serviceTypeCreationDTO.Price;

            if (serviceTypeCreationDTO.ServiceCategorieId != 0)
            {
                // Desasociamos la navegación para que EF tome solo la FK
                serviceTypeDB.ServiceCategories = null;
                serviceTypeDB.ServiceCategorieId = serviceTypeCreationDTO.ServiceCategorieId;
            }

            _serviceTypeRepository.Update(serviceTypeDB);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task DeleteServiceTypeAsync(int Id)
        {
            var serviceTypeDB = await _serviceTypeRepository.GetByIdAsync(Id);

            if (serviceTypeDB == null)
                throw new KeyNotFoundException("Tipo de servicio no encontrado");

            try
            {
                _serviceTypeRepository.Remove(serviceTypeDB);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                throw new InvalidOperationException("No se puede eliminar este tipo de servicio porque está asociado a una venta.");
            }
        }
    }
}
