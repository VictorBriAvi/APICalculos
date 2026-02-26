using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly MyDbContext _dbContext;

        public PaymentTypeRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PaymentTypes>> GetAllAsync(int storeId, string? search)
        {
            var query = _dbContext.PaymentTypes
                .AsNoTracking()
                .Where(x => x.StoreId == storeId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = search.Trim().ToLower();
                query = query.Where(x => x.Name.ToLower().Contains(normalizedSearch));
            }

            return await query
                .OrderByDescending(x => x.Id)
                .ToListAsync();
        }

        public async Task<PaymentTypes?> GetByIdAsync(int id, int storeId)
        {
            return await _dbContext.PaymentTypes
                .FirstOrDefaultAsync(x => x.Id == id && x.StoreId == storeId);
        }

        public async Task AddAsync(PaymentTypes paymentType)
        {
            await _dbContext.PaymentTypes.AddAsync(paymentType);
        }

        public void Update(PaymentTypes paymentType)
        {
            _dbContext.PaymentTypes.Update(paymentType);
        }

        public void Remove(PaymentTypes paymentType)
        {
            _dbContext.PaymentTypes.Remove(paymentType);
        }

        public async Task<bool> ExistsByNameAsync(string name, int storeId)
        {
            var normalized = name.Replace(" ", "").Trim().ToLower();

            return await _dbContext.PaymentTypes.AnyAsync(x =>
                x.StoreId == storeId &&
                x.Name.Replace(" ", "").Trim().ToLower() == normalized);
        }
    }
}
