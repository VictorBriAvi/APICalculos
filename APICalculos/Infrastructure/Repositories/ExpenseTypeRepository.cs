using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class ExpenseTypeRepository : IExpenseTypeRepository
    {
        private readonly MyDbContext _dbContext;

        public ExpenseTypeRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ExpenseType>> GetAllAsync()
        {
            return await _dbContext.ExpenseTypes.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ExpenseType> GetByIdAsync(int id)
        {
            return await _dbContext.ExpenseTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ExpenseType expenseType)
        {
            await _dbContext.ExpenseTypes.AddAsync(expenseType);
        }

        public void Update(ExpenseType expenseType)
        {
            _dbContext.ExpenseTypes.Update(expenseType);
        }

        public void Remove(ExpenseType expenseType)
        {
            _dbContext.ExpenseTypes.Remove(expenseType);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var convertName = name.Replace(" ", "").Trim();
            return await _dbContext.ExpenseTypes.AnyAsync(c => c.Name.Replace(" ", "").Trim() == convertName);
        }
    }
}
