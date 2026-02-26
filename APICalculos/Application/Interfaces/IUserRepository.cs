using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllByStoreAsync(int storeId);
        Task AddAsync(User user);
        void Update(User user);
        void Delete(User user);
        Task<bool> UsernameExistsAsync(string username);
        Task<User?> GetByUsernameAsync(string username);

    }
}
