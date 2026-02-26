using APICalculos.Application.DTOs.Store;

namespace APICalculos.Application.Interfaces
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreResponseDTO>> GetAllAsync();
        Task<StoreResponseDTO?> GetByIdAsync(int id);
        Task<StoreResponseDTO> CreateAsync(StoreCreateDto dto);
        Task<bool> UpdateAsync(int id, StoreUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
