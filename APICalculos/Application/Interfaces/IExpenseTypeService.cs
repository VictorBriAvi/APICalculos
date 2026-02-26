using APICalculos.Application.DTOs.ExpenseType;

namespace APICalculos.Application.Interfaces
{
    public interface IExpenseTypeService
    {
        Task<List<ExpenseTypeDTO>> GetAllExpensesTypesAsync(int storeId, string? search);
        Task<ExpenseTypeDTO?> GetExpenseTypeForId(int id, int storeId);
        Task<ExpenseTypeDTO> AddExpenseTypeAsync(int storeId, ExpenseTypeCreationDTO dto);
        Task UpdateExpenseTypeAsync(int id, int storeId, ExpenseTypeCreationDTO dto);
        Task DeleteExpenseTypeAsync(int id, int storeId);
    }

}
