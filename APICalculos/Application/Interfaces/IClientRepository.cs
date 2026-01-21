using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client> GetByIdAsync(int id);
        Task<Client> GetByDocumentoAsync(string doc);
        Task AddAsync(Client cliente);
        void Remove(Client cliente);
        Task<bool> ExistsByNombreAsync(string nombre);
        Task<bool> ExistsByDocumentoAsync(string documento);
        void Update(Client cliente);

        Task<List<Client>> SearchAsync(string query, int limit);
    }
}
