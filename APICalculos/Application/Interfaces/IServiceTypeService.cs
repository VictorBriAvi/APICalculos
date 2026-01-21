using APICalculos.Application.DTOs;
using APICalculos.Application.DTOs.Services;

namespace APICalculos.Application.Interfaces
{
    public interface IServiceTypeService
    {
        Task<List<ServiceTypeDTO>> GetAllServiceTypesAsync(string? search);
        Task<List<ServiceTypeDTO>> SearchServicesAsync(int? categoryId = null);
        Task<ServiceTypeDTO> GetServiceTypeForId(int id);
        Task<ServiceTypeDTO> AddServiceTypeAsync(ServiceTypeCreationDTO serviceTypeCreationDTO);
        Task UpdateServiceTypeAsync(int id, ServiceTypeCreationDTO serviceTypeCreationDTO);
        Task DeleteServiceTypeAsync(int Id);
        Task<List<ServicesSearchDTO>> SearchServiceAsync(string query);

    }
}
