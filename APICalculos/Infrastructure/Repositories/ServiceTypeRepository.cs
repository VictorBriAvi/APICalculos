using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICalculos.Infrastructure.Repositories
{
    public class ServiceTypeRepository : IServiceTypeRepository
    {
        private readonly MyDbContext _dbContext;

        public ServiceTypeRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ServiceType>> GetAllAsync(
            int storeId,
            string? search,
            int? serviceCategorieId)
        {
            var query = _dbContext.ServiceTypes
                .AsNoTracking()
                .Include(s => s.ServiceCategories)
                .Where(x => x.StoreId == storeId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalized = search.Trim().ToLower();
                query = query.Where(s =>
                    s.Name.ToLower().Contains(normalized));
            }

            if (serviceCategorieId.HasValue)
            {
                query = query.Where(s =>
                    s.ServiceCategorieId == serviceCategorieId.Value);
            }

            return await query
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<ServiceType>> SearchAsync(
            int storeId,
            Expression<Func<ServiceType, bool>> predicate)
        {
            return await _dbContext.ServiceTypes
                .Include(s => s.ServiceCategories)
                .AsNoTracking()
                .Where(x => x.StoreId == storeId)
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<List<ServiceType>> SearchAsync(
            int storeId,
            string query,
            int limit)
        {
            return await _dbContext.ServiceTypes
                .AsNoTracking()
                .Where(x => x.StoreId == storeId &&
                            x.Name.Contains(query))
                .OrderBy(x => x.Name)
                .Take(limit)
                .ToListAsync();
        }

        public async Task<ServiceType?> GetByIdAsync(int id, int storeId)
        {
            return await _dbContext.ServiceTypes
                .Include(s => s.ServiceCategories)
                .FirstOrDefaultAsync(x =>
                    x.Id == id &&
                    x.StoreId == storeId);
        }

        public async Task AddAsync(ServiceType serviceType)
        {
            await _dbContext.ServiceTypes.AddAsync(serviceType);
        }

        public void Update(ServiceType serviceType)
        {
            _dbContext.ServiceTypes.Update(serviceType);
        }

        public void Remove(ServiceType serviceType)
        {
            _dbContext.ServiceTypes.Remove(serviceType);
        }

        public async Task<bool> ExistsByNameAsync(string name, int storeId)
        {
            var normalized = name.Replace(" ", "")
                                 .Trim()
                                 .ToLower();

            return await _dbContext.ServiceTypes.AnyAsync(x =>
                x.StoreId == storeId &&
                x.Name.Replace(" ", "")
                      .Trim()
                      .ToLower() == normalized);
        }
    }
}
