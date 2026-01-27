using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IExpensesRepository
    {
        Task<IEnumerable<Expense>> GetAllAsync(
                    string? search,
                    int? expenseTypeId,
                    DateTime? fromDate,
                    DateTime? toDate
                );
        Task<Expense> GetByIdAsync(int id);
        Task AddAsync(Expense expense);
        void Update(Expense expense);
        void Remove(Expense expense);
        Task<bool> ExistsByNameAsync(string name);
    }
}
