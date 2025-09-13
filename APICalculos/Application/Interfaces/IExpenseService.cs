using APICalculos.Application.DTOs;

namespace APICalculos.Application.Interfaces
{
    public interface IExpenseService
    {
        Task<List<ExpenseDTO>> GetAllExpenseAsync();
        Task<ExpenseDTO> GetExpenseForIdAsync(int id);
        Task<ExpenseDTO> AddExpensesAsync(ExpenseCreationDTO expenseCreationDTO);
        Task UpdateExpenseAsync(int id, ExpenseCreationDTO expenseCreationDTO);
        Task DeleteExpenseAsync(int id);
    }
}
