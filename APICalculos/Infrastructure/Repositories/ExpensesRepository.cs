using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class ExpensesRepository : IExpensesRepository
    {
        private readonly MyDbContext _dbContext;

        public ExpensesRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Expenses>> GetAllAsync(
            int storeId,
            string? search,
            int? expenseTypeId,
            int? paymentTypeId,
            DateTime? fromDate,
            DateTime? toDate
        )
        {
            IQueryable<Expenses> query = _dbContext.Expenses
                .Where(e => e.StoreId == storeId)
                .AsNoTracking()
                .Include(e => e.ExpenseType)
                .Include(e => e.PaymentType);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = search.Trim().ToLower();
                query = query.Where(e =>
                    e.Description.ToLower().Contains(normalizedSearch));
            }

            if (expenseTypeId.HasValue)
                query = query.Where(e => e.ExpenseTypeId == expenseTypeId.Value);

            if (paymentTypeId.HasValue)
                query = query.Where(e => e.PaymentTypeId == paymentTypeId.Value);

            if (fromDate.HasValue)
                query = query.Where(e => e.ExpenseDate >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(e => e.ExpenseDate <= toDate.Value);

            return await query
                .OrderByDescending(e => e.ExpenseDate)
                .ToListAsync();
        }

        public async Task<Expenses?> GetByIdAsync(int id, int storeId)
        {
            return await _dbContext.Expenses
                .Include(e => e.ExpenseType)
                .Include(e => e.PaymentType)
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.Id == id && x.StoreId == storeId);
        }

        public async Task AddAsync(Expenses expense)
        {
            await _dbContext.Expenses.AddAsync(expense);
        }

        public void Update(Expenses expense)
        {
            _dbContext.Expenses.Update(expense);
        }

        public void Remove(Expenses expense)
        {
            _dbContext.Expenses.Remove(expense);
        }

        public async Task<bool> ExistsByNameAsync(string name, int storeId)
        {
            var convertName = name.Replace(" ", "").Trim();

            return await _dbContext.Expenses.AnyAsync(c =>
                c.StoreId == storeId &&
                c.Description.Replace(" ", "").Trim() == convertName);
        }
    }

}
