using APICalculos.Application.DTOs.Services;

namespace APICalculos.Application.Interfaces
{
    public interface IServiceTypeService
    {
        Task<List<ServiceTypeDTO>> GetAllServiceTypesAsync(
            int storeId,
            string? search,
            int? serviceCategorieId);

        Task<ServiceTypeDTO?> GetServiceTypeForId(int id, int storeId);

        Task<List<ServiceTypeDTO>> SearchServicesAsync(
            int storeId,
            int? categoryId = null);

        Task<ServiceTypeDTO> AddServiceTypeAsync(
            int storeId,
            ServiceTypeCreationDTO dto);

        Task UpdateServiceTypeAsync(
            int id,
            int storeId,
            ServiceTypeCreationDTO dto);

        Task DeleteServiceTypeAsync(int id, int storeId);

        Task<List<ServicesSearchDTO>> SearchServiceAsync(
            int storeId,
            string query);
    }
}
