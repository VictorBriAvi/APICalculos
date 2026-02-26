using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly MyDbContext _dbContext;

        public SaleRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Sale>> GetAllAsync(int storeId)
        {
            return await _dbContext.Sales
                .Where(s => s.StoreId == storeId)
                .Include(st => st.SaleDetail).ThenInclude(x => x.ServiceType)
                .Include(st => st.SaleDetail).ThenInclude(x => x.Employee)
                .Include(st => st.Client)
                .Include(st => st.Payments).ThenInclude(p => p.PaymentType)
                .AsNoTracking()
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }
        public async Task<List<Sale>> GetFilteredAsync(
            int storeId,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? clientId = null,
            int? paymentTypeId = null,
            int? employeeId = null,
            int? serviceTypeId = null)
        {
            IQueryable<Sale> query = _dbContext.Sales
                .Where(s => s.StoreId == storeId)

                .Include(s => s.Client)

                .Include(s => s.SaleDetail)
                    .ThenInclude(d => d.ServiceType)

                .Include(s => s.SaleDetail)
                    .ThenInclude(d => d.Employee)

                .Include(s => s.Payments)
                    .ThenInclude(p => p.PaymentType)

                .AsNoTracking();

            if (fromDate.HasValue)
                query = query.Where(s => s.DateSale >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(s => s.DateSale <= toDate.Value);

            if (clientId.HasValue)
                query = query.Where(s => s.ClientId == clientId.Value);

            if (paymentTypeId.HasValue)
                query = query.Where(s =>
                    s.Payments.Any(p => p.PaymentTypeId == paymentTypeId.Value));

            if (employeeId.HasValue)
                query = query.Where(s =>
                    s.SaleDetail.Any(d => d.EmployeeId == employeeId.Value));

            if (serviceTypeId.HasValue)
                query = query.Where(s =>
                    s.SaleDetail.Any(d => d.ServiceTypeId == serviceTypeId.Value));

            return await query
                .OrderByDescending(s => s.DateSale)
                .ToListAsync();
        }

        public async Task<Sale?> GetByIdAsync(int id, int storeId)
        {
            return await _dbContext.Sales
                .Include(st => st.SaleDetail).ThenInclude(x => x.ServiceType)
                .Include(st => st.SaleDetail).ThenInclude(x => x.Employee)
                .Include(st => st.Client)
                .Include(st => st.Payments).ThenInclude(p => p.PaymentType)
                .FirstOrDefaultAsync(x => x.Id == id && x.StoreId == storeId);
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
