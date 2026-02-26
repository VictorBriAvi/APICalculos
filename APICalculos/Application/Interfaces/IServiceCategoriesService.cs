using APICalculos.Application.DTOs.ServiceCategories;

namespace APICalculos.Application.Interfaces
{
    public interface IServiceCategoriesService
    {
        Task<List<ServiceCategoriesDTO>> GetAllServiceCategoriesAsync(int storeId, string? search);
        Task<ServiceCategoriesDTO?> GetServiceCategorieForId(int id, int storeId);
        Task<ServiceCategoriesDTO> AddServiceCategorieAsync(int storeId, ServiceCategoriesCreationDTO dto);
        Task UpdateServiceCategorieAsync(int id, int storeId, ServiceCategoriesCreationDTO dto);
        Task DeleteServiceCategorieAsync(int id, int storeId);
    }
}
