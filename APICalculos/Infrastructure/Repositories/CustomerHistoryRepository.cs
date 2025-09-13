using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class CustomerHistoryRepository : ICustomerHistoryRepository
    {
        private readonly MyDbContext _dbContext;
    
        public CustomerHistoryRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CustomerHistory>> GetAllAsync()
        {
            return await _dbContext.CustomerHistories.Include(st => st.Client).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<CustomerHistory> GetByIdAsync(int id)
        {
            return await _dbContext.CustomerHistories.Include(st => st.Client).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id); 
        }

        public async Task AddAsync(CustomerHistory customerHistory)
        {
            await _dbContext.CustomerHistories.AddAsync(customerHistory);
        }

        public void Update(CustomerHistory customerHistory)
        {
            _dbContext.CustomerHistories.Update(customerHistory);
        }

        public void Remove(CustomerHistory customerHistory)
        {
            _dbContext.CustomerHistories.Remove(customerHistory);
        }

        public async Task<bool> ExistsByTitleAsync(string name)
        {
            var convertName = name.Replace(" ", "").Trim();
            return await _dbContext.CustomerHistories.AnyAsync(c => c.Title.Replace(" ", "").Trim() == convertName);
        }
    }
}
