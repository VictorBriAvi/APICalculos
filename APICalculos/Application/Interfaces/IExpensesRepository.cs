using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IExpensesRepository
    {
        Task<IEnumerable<Expenses>> GetAllAsync(
            int storeId,
            string? search,
            int? expenseTypeId,
            int? paymentTypeId,
            DateTime? fromDate,
            DateTime? toDate
        );

        Task<Expenses?> GetByIdAsync(int id, int storeId);
        Task AddAsync(Expenses expense);
        void Update(Expenses expense);
        void Remove(Expenses expense);
        Task<bool> ExistsByNameAsync(string name, int storeId);
    }

}
