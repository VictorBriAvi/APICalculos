using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IExpenseTypeRepository
    {
        Task<IEnumerable<ExpenseType>> GetAllAsync();
        Task<ExpenseType> GetByIdAsync(int id);
        Task AddAsync(ExpenseType expenseType);
        void Update(ExpenseType expenseType);
        void Remove(ExpenseType expenseType);
        Task<bool> ExistsByNameAsync(string name);
    }
}
