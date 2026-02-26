using APICalculos.Application.DTOs.User;

namespace APICalculos.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAllByStoreAsync(int storeId);
        Task<UserResponseDTO?> GetByIdAsync(int id);
        Task<UserResponseDTO> CreateAsync(UserCreateDTO dto);
        Task<bool> UpdateAsync(int id, UserUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
