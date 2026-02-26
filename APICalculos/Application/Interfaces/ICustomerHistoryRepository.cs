using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface ICustomerHistoryRepository
    {
        Task<IEnumerable<CustomerHistory>> GetAllAsync(int storeId);
        Task<CustomerHistory?> GetByIdAsync(int id, int storeId);
        Task AddAsync(CustomerHistory customerHistory);
        void Update(CustomerHistory customerHistory);
        void Remove(CustomerHistory customerHistory);
        Task<bool> ExistsByTitleAsync(string title, int storeId);
    }
}
