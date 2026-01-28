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

        public async Task<IEnumerable<Expense>> GetAllAsync(string? search, int? expenseTypeId, DateTime? fromDate,DateTime? toDate)
        {
            IQueryable<Expense> query = _dbContext.Expenses.AsNoTracking().Include(e => e.ExpenseType).Include(e => e.PaymentType);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = search.Trim().ToLower();

                query = query.Where(e => e.Description.ToLower().Contains(normalizedSearch));
            }

            if (expenseTypeId.HasValue)
            {
                query = query.Where(e => e.ExpenseTypeId == expenseTypeId.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(e => e.ExpenseDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(e => e.ExpenseDate <= toDate.Value);
            }

            return await query.OrderByDescending(e => e.ExpenseDate).ToListAsync();
        }


        public async Task<Expense> GetByIdAsync(int id)
        {
            return await _dbContext.Expenses.Include(st => st.ExpenseType).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Expense expense)
        {
            await _dbContext.Expenses.AddAsync(expense);
        }

        public void Update(Expense expense)
        {
            _dbContext.Expenses.Update(expense);
        }

        public void Remove(Expense expense)
        {
            _dbContext.Expenses.Remove(expense);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var convertName = name.Replace(" ", "").Trim();
            return await _dbContext.Expenses.AnyAsync(c => c.Description.Replace(" ", "").Trim() == convertName);
        }
    }
}
