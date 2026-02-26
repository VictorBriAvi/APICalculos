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

        public async Task<IEnumerable<CustomerHistory>> GetAllAsync(int storeId)
        {
            return await _dbContext.CustomerHistories
                .Where(x => x.StoreId == storeId)
                .Include(x => x.Client)
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<CustomerHistory?> GetByIdAsync(int id, int storeId)
        {
            return await _dbContext.CustomerHistories
                .Include(x => x.Client)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && x.StoreId == storeId);
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

        public async Task<bool> ExistsByTitleAsync(string title, int storeId)
        {
            var normalized = title.Replace(" ", "").Trim();

            return await _dbContext.CustomerHistories
                .AnyAsync(c =>
                    c.StoreId == storeId &&
                    c.Title.Replace(" ", "").Trim() == normalized);
        }
    }
}
