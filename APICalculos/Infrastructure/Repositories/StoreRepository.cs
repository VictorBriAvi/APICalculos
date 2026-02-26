using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entities;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly MyDbContext _context;

        public StoreRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<Store?> GetByIdAsync(int id)
        {
            return await _context.Stores
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Store store)
        {
            await _context.Stores.AddAsync(store);
        }

        public void Update(Store store)
        {
            _context.Stores.Update(store);
        }

        public void Delete(Store store)
        {
            _context.Stores.Remove(store);
        }
    }


}
