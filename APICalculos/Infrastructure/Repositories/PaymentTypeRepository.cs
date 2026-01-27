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

        public async Task<IEnumerable<PaymentType>> GetAllAsync(string? search)
        {
            var query = _dbContext.PaymentTypes.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = search.Trim().ToLower();

                query = query.Where(c => c.Name.ToLower().Contains(normalizedSearch));
            }

            return await query.OrderByDescending(x => x.Id).ToListAsync();
        }


        public async Task<PaymentType> GetByIdAsync(int id)
        {
            return await _dbContext.PaymentTypes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(PaymentType paymentType)
        {
            await _dbContext.PaymentTypes.AddAsync(paymentType);
        }
        public void Remove(PaymentType paymentType)
        {
            _dbContext.PaymentTypes.Remove(paymentType);
        }
        public void Update(PaymentType paymentType)
        {
            _dbContext.PaymentTypes.Update(paymentType);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var convertName = name.Replace(" ", "").Trim();
            return await _dbContext.PaymentTypes.AnyAsync(c => c.Name.Replace(" ", "").Trim() == convertName);
        }
    }
}
