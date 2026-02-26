using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDbContext _context;

        public UserRepository(MyDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllByStoreAsync(int storeId)
        {
            return await _context.Users
                .Where(x => x.StoreId == storeId)
                .ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _context.Users
                .AnyAsync(x => x.Username == username);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(x => x.UserRoles)
                .FirstOrDefaultAsync(x => x.Username == username);
        }

    }

}
