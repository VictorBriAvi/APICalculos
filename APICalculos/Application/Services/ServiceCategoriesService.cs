using APICalculos.Application.DTOs;
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
        private readonly IServiceCategoriesRepository _serviceCategoriesRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ServiceCategoriesService(IServiceCategoriesRepository serviceCategoriesRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _serviceCategoriesRepository = serviceCategoriesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ServiceCategoriesDTO>> GetAllServiceCategoriesAsync(string? search)
        {
            var serviceCategories =
                await _serviceCategoriesRepository.GetAllAsync(search);

            return _mapper.Map<List<ServiceCategoriesDTO>>(serviceCategories);
        }


        public async Task<ServiceCategoriesDTO> GetServiceCategorieForId(int id)
        {
            var serviceCategorie = await _serviceCategoriesRepository.GetByIdAsync(id);
            if (serviceCategorie == null)
            {
                return null;
            }

            return _mapper.Map<ServiceCategoriesDTO>(serviceCategorie);
        }

        public async Task<ServiceCategoriesDTO> AddServiceCategorieAsync(ServiceCategoriesCreationDTO serviceCategoriesCreationDTO)
        {
            if (string.IsNullOrWhiteSpace(serviceCategoriesCreationDTO.Name))
            {
                throw new ArgumentException("El nombre de la categoria de servicio no puede estar vacio");
            }

            var existsName = await _serviceCategoriesRepository.ExistsByNameAsync(serviceCategoriesCreationDTO.Name);
            if (existsName)
            {
                throw new InvalidOperationException("El nombre de la categoria de servicio ya existe");
            }

            var serviceCategorie = _mapper.Map<ServiceCategorie>(serviceCategoriesCreationDTO);

            await _unitOfWork.ServiceCategories.AddAsync(serviceCategorie);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ServiceCategoriesDTO>(serviceCategorie);
        }

        public async Task UpdateServiceCategorieAsync(int id, ServiceCategoriesCreationDTO serviceCategoriesCreationDTO)
        {
            var serviceCategorieDB = await _serviceCategoriesRepository.GetByIdAsync(id);

            if (serviceCategorieDB == null)
                throw new KeyNotFoundException("Categoria servicio no encontrada");

            if (!string.IsNullOrWhiteSpace(serviceCategoriesCreationDTO.Name))
                serviceCategorieDB.Name = serviceCategoriesCreationDTO.Name;

            _serviceCategoriesRepository.Update(serviceCategorieDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteServiceCategorieAsync(int id)
        {
            var serviceCategorieDB = await _serviceCategoriesRepository.GetByIdAsync(id);
            if (serviceCategorieDB == null)
                throw new KeyNotFoundException("Servicio Categoria no encontrada");

            try
            {

                _serviceCategoriesRepository.Remove(serviceCategorieDB);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                throw new InvalidOperationException("No se puede eliminar esta categoria del servicio porque está asociado a una venta.");
            }
        }
    }
}
