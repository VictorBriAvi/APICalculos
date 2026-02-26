using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Domain.Entities;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly MyDbContext _dbContext;


        public ClientRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<IEnumerable<Client>> GetAllAsync(int storeId, string? search)
        {
            var query = _dbContext.Clients
                .AsNoTracking()
                .Where(c => c.StoreId == storeId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = search.Trim().ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(normalizedSearch));
            }

            return await query.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id, int storeId)
        {
            return await _dbContext.Clients
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id && c.StoreId == storeId);
        }



        public async Task<Client?> GetByDocumentoAsync(string doc)
        {
            return await _dbContext.Clients
                .FirstOrDefaultAsync(c => c.Name == doc);
        }

        public async Task AddAsync(Client cliente)
        {
            await _dbContext.Clients.AddAsync(cliente);
        }

        public void Remove(Client cliente)
        {
            _dbContext.Clients.Remove(cliente);
        }

        public void Update(Client cliente)
        {
            _dbContext.Clients.Update(cliente); 
        }

        public async Task<bool> ExistsByNombreAsync(string nombre, int storeId)
        {
            var nombreNormalizado = nombre.Replace(" ", "").Trim();

            return await _dbContext.Clients
                .AnyAsync(c =>
                    c.StoreId == storeId &&
                    c.Name.Replace(" ", "").Trim() == nombreNormalizado);
        }


        public async Task<bool> ExistsByDocumentoAsync(string documento)
        {
            var documentoNormalizado = documento.Replace(" ", "").Trim();
            return await _dbContext.Clients
                .AnyAsync(c => c.IdentityDocument.Replace(" ", "").Trim() == documentoNormalizado);
        }

        public async Task<bool> ExistsByEmailAsync(string email, int storeId)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailNormalizado = email.Trim().ToLower();

            return await _dbContext.Clients
                .AnyAsync(c =>
                    c.Email != null &&
                    c.StoreId == storeId &&
                    c.Email.Trim().ToLower() == emailNormalizado
                );
        }


        public async Task<bool> ExistsByPhoneAsync(string phone, int storeId)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            var phoneNormalizado = phone
                .Replace(" ", "")
                .Replace("-", "")
                .Trim();

            return await _dbContext.Clients
                .AnyAsync(c =>
                    c.Phone != null &&
                    c.StoreId == storeId &&
                    c.Phone
                        .Replace(" ", "")
                        .Replace("-", "")
                        .Trim() == phoneNormalizado
                );
        }


        public async Task<List<Client>> SearchAsync(string query, int limit, int storeId)
        {
            return await _dbContext.Clients
                .AsNoTracking()
                .Where(c =>
                    c.StoreId == storeId &&
                    (c.Name.Contains(query) ||
                     c.IdentityDocument.Contains(query)))
                .OrderBy(c => c.Name)
                .Take(limit)
                .ToListAsync();
        }



    }
}
