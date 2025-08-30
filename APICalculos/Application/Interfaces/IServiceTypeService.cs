using APICalculos.Application.DTOs;

namespace APICalculos.Application.Interfaces
{
    public interface IServiceTypeService
    {
        Task<List<ServiceTypeDTO>> GetAllServicesTypesAsync();
        Task<ServiceTypeDTO> GetServiceTypeForId(int id);
        Task<ServiceTypeDTO> AddServiceTypeAsync(ServiceTypeCreationDTO serviceTypeCreationDTO);
        Task UpdateServiceTypeAsync(int id, ServiceTypeCreationDTO serviceTypeCreationDTO);
        Task DeleteServiceTypeAsync(int Id);

    }
}
