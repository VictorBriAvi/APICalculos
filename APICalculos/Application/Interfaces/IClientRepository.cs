using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync(int storeId, string? search);
        Task<Client?> GetByIdAsync(int id, int storeId);
        Task<Client> GetByDocumentoAsync(string doc);


        Task AddAsync(Client cliente);
        void Remove(Client cliente);
        void Update(Client cliente);

        Task<bool> ExistsByNombreAsync(string nombre, int storeId);
        Task<bool> ExistsByEmailAsync(string email, int storeId);
        Task<bool> ExistsByPhoneAsync(string phone, int storeId);


        Task<List<Client>> SearchAsync(string query, int limit, int storeId);
    }
}
