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

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            return await _dbContext.Expenses.Include(st => st.ExpenseType).AsNoTracking().OrderByDescending(st => st.Id).ToListAsync();
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
