using APICalculos.Application.DTOs.Expense;

namespace APICalculos.Application.Interfaces
{
    public interface IExpenseService
    {
        Task<List<ExpenseDTO>> GetAllExpenseAsync(
            int storeId,
            string? search,
            int? expenseTypeId,
            int? paymentTypeId,
            DateTime? fromDate,
            DateTime? toDate
        );

        Task<ExpenseDTO?> GetExpenseForIdAsync(int id, int storeId);
        Task<ExpenseDTO> AddExpensesAsync(int storeId, ExpenseCreationDTO dto);
        Task UpdateExpenseAsync(int id, int storeId, ExpenseCreationDTO dto);
        Task DeleteExpenseAsync(int id, int storeId);
    }

}
