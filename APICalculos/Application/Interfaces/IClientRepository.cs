using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IClientRepository
    {
        Task<IEnumerable<ClientModel>> GetAllAsync();
        Task<ClientModel> GetByIdAsync(int id);
        Task<ClientModel> GetByDocumentoAsync(string doc);
        Task AddAsync(ClientModel cliente);
        void Remove(ClientModel cliente);
        Task<bool> ExistsByNombreAsync(string nombre);
        Task<bool> ExistsByDocumentoAsync(string documento);
        void Update(ClientModel cliente); 
    }
}
