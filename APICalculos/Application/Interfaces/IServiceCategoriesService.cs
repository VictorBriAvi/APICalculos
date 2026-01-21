using APICalculos.Application.DTOs;

namespace APICalculos.Application.Interfaces
{
    public interface IServiceCategoriesService
    {
        Task<List<ServiceCategoriesDTO>> GetAllServiceCategoriesAsync(string? search);
        Task<ServiceCategoriesDTO> GetServiceCategorieForId(int id);
        Task<ServiceCategoriesDTO> AddServiceCategorieAsync(ServiceCategoriesCreationDTO serviceCategoriesCreationDTO);
        Task UpdateServiceCategorieAsync(int id, ServiceCategoriesCreationDTO serviceCategoriesCreationDTO);
        Task DeleteServiceCategorieAsync(int id);
    }
}
