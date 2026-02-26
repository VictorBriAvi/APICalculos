using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class SaleDetailRepository : ISaleDetailRepository
    {
        private readonly MyDbContext _dbContext;

        public SaleDetailRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region GET

        public async Task<IEnumerable<SaleDetail>> GetAllAsync(int storeId)
        {
            return await _dbContext.SaleDetails
                .Where(x => x.StoreId == storeId && !x.IsDeleted)
                .Include(st => st.Sale)
                .Include(st => st.ServiceType)
                .Include(st => st.Employee)
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<SaleDetail> GetByIdAsync(int id, int storeId)
        {
            return await _dbContext.SaleDetails
                .Where(x => x.Id == id && x.StoreId == storeId && !x.IsDeleted)
                .Include(st => st.Sale)
                .Include(st => st.ServiceType)
                .Include(st => st.Employee)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<List<SaleDetail>> GetBySaleIdAsync(int saleId, int storeId)
        {
            return await _dbContext.SaleDetails
                .Where(d => d.SaleId == saleId && d.StoreId == storeId && !d.IsDeleted)
                .Include(d => d.ServiceType)
                .Include(d => d.Employee)
                .AsNoTracking()
                .ToListAsync();
        }

        #endregion

        public async Task AddAsync(SaleDetail saleDetail)
        {
            await _dbContext.SaleDetails.AddAsync(saleDetail);
        }

        public void Update(SaleDetail saleDetail)
        {
            _dbContext.SaleDetails.Update(saleDetail);
        }

        public void Remove(SaleDetail saleDetail)
        {
            _dbContext.SaleDetails.Remove(saleDetail);
        }
    }
}
