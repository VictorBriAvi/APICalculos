using APICalculos.Application.DTOs;

namespace APICalculos.Application.Interfaces
{
    public interface IExpenseTypeService
    {
        Task<List<ExpenseTypeDTO>> GetAllExpensesTypesAsync(string? search);
        Task<ExpenseTypeDTO> GetExpenseTypeForId(int id);
        Task<ExpenseTypeDTO> AddExpenseTypeAsync(ExpenseTypeCreationDTO expenseTypeCreationDTO);
        Task UpdateExpenseTypeAsync(int id, ExpenseTypeCreationDTO expenseTypeCreationDTO);
        Task DeleteExpenseTypeAsync(int id);
    }
}
