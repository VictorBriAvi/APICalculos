using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
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

        public async Task<IEnumerable<Client>> GetAllAsync(string? search)
        {
            var query = _dbContext.Clients.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var normalizedSearch = search.Trim().ToLower();

                query = query.Where(c => c.Name.ToLower().Contains(normalizedSearch));
            }

            return await query.OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _dbContext.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
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

        public async Task<bool> ExistsByNombreAsync(string nombre)
        {
            var nombreNormalizado = nombre.Replace(" ", "").Trim();
            return await _dbContext.Clients
                .AnyAsync(c => c.Name.Replace(" ", "").Trim() == nombreNormalizado);
        }

        public async Task<bool> ExistsByDocumentoAsync(string documento)
        {
            var documentoNormalizado = documento.Replace(" ", "").Trim();
            return await _dbContext.Clients
                .AnyAsync(c => c.IdentityDocument.Replace(" ", "").Trim() == documentoNormalizado);
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            var emailNormalizado = email.Trim().ToLower();

            return await _dbContext.Clients
                .AnyAsync(c =>
                    c.Email != null &&
                    c.Email.Trim().ToLower() == emailNormalizado
                );
        }


        public async Task<bool> ExistsByPhoneAsync(string phone)
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
                    c.Phone
                        .Replace(" ", "")
                        .Replace("-", "")
                        .Trim() == phoneNormalizado
                );
        }


        public async Task<List<Client>> SearchAsync(string query, int limit)
        {
            return await _dbContext.Clients
                .AsNoTracking()
                .Where(c =>
                    c.Name.Contains(query) ||
                    c.IdentityDocument.Contains(query)
                )
                .OrderBy(c => c.Name)
                .Take(limit)
                .ToListAsync();
        }


    }
}
