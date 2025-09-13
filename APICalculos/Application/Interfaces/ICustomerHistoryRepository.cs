using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface ICustomerHistoryRepository
    {
        Task<IEnumerable<CustomerHistory>> GetAllAsync();
        Task<CustomerHistory> GetByIdAsync(int id);
        Task AddAsync(CustomerHistory customerHistory);
        void Update(CustomerHistory customerHistory);
        void Remove(CustomerHistory customerHistory);
        Task<bool> ExistsByTitleAsync(string name);
    }
}
