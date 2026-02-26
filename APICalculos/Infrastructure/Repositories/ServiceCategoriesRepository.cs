using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class ServiceCategoriesRepository : IServiceCategoriesRepository
    {
        private readonly MyDbContext _dbContext;

        public ServiceCategoriesRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ServiceCategorie>> GetAllAsync(int storeId, string? search)
        {
            var query = _dbContext.ServiceCategories
                .AsNoTracking()
                .Where(x => x.StoreId == storeId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = search.Trim().ToLower();
                query = query.Where(x =>
                    x.Name.ToLower().Contains(normalizedSearch));
            }

            return await query
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<ServiceCategorie?> GetByIdAsync(int id, int storeId)
        {
            return await _dbContext.ServiceCategories
                .FirstOrDefaultAsync(x => x.Id == id && x.StoreId == storeId);
        }

        public async Task AddAsync(ServiceCategorie serviceCategorie)
        {
            await _dbContext.ServiceCategories.AddAsync(serviceCategorie);
        }

        public void Update(ServiceCategorie serviceCategorie)
        {
            _dbContext.ServiceCategories.Update(serviceCategorie);
        }

        public void Remove(ServiceCategorie serviceCategorie)
        {
            _dbContext.ServiceCategories.Remove(serviceCategorie);
        }

        public async Task<bool> ExistsByNameAsync(string name, int storeId)
        {
            var normalized = name.Replace(" ", "").Trim().ToLower();

            return await _dbContext.ServiceCategories.AnyAsync(x =>
                x.StoreId == storeId &&
                x.Name.Replace(" ", "").Trim().ToLower() == normalized);
        }
    }
}
