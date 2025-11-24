using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace APICalculos.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly MyDbContext _dbContext;

        public SaleRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync()
        {
            return await _dbContext.Sales
                .Include(st => st.SaleDetail).ThenInclude(x => x.ServiceType)
                .Include(st => st.SaleDetail).ThenInclude(x => x.Employee)
                .Include(st => st.Client)
                .Include(st => st.Payments)
                    .ThenInclude(p => p.PaymentType)
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToListAsync();

        }

        public async Task<Sale> GetByIdAsync(int id)
        {
            return await _dbContext.Sales
                .Include(st => st.SaleDetail).ThenInclude(x => x.ServiceType)
                .Include(st => st.SaleDetail).ThenInclude(x => x.Employee)
                .Include(st => st.Client)
                .Include(st => st.Payments)
                    .ThenInclude(p => p.PaymentType)
                //.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Sale sale)
        {
            await _dbContext.Sales.AddAsync(sale);
        }

        public void Update(Sale sale)
        {
           _dbContext.Sales.Update(sale);
        }

        public void Remove(Sale sale)
        {
            _dbContext.Sales.Remove(sale);
        }
    }
}
