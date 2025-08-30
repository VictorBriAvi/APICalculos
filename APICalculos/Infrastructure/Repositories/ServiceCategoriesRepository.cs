using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace APICalculos.Infrastructure.Repositories
{
    public class ServiceCategoriesRepository : IServiceCategoriesRepository
    {
        private readonly MyDbContext _dbContext;

        public ServiceCategoriesRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ServiceCategorie>> GetAllAsync()
        {
            return await _dbContext.ServiceCategories.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ServiceCategorie> GetByIdAsync(int id)
        {
            return await _dbContext.ServiceCategories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var convertName = name.Replace(" ", "").Trim();
            return await _dbContext.ServiceCategories.AnyAsync(c => c.Name.Replace(" ", "").Trim() == convertName);
        }


    }
}
