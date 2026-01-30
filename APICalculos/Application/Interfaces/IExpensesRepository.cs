using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IExpensesRepository
    {
        Task<IEnumerable<Expenses>> GetAllAsync(
                    string? search,
                    int? expenseTypeId,
                    DateTime? fromDate,
                    DateTime? toDate
                );
        Task<Expenses> GetByIdAsync(int id);
        Task AddAsync(Expenses expense);
        void Update(Expenses expense);
        void Remove(Expenses expense);
        Task<bool> ExistsByNameAsync(string name);
    }
}
