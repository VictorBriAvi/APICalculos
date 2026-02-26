using APICalculos.Domain.Entities;

namespace APICalculos.Application.Interfaces
{
    public interface IStoreRepository
    {
        Task<Store?> GetByIdAsync(int id);
        Task<IEnumerable<Store>> GetAllAsync();
        Task AddAsync(Store store);
        void Update(Store store);
        void Delete(Store store);
    }
}
