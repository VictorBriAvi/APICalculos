using APICalculos.Application.DTOs.Client;

namespace APICalculos.Application.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientDTO>> GetAllClientsAsync();
        Task<ClientDTO> GetClientForIdAsync(int id);
        Task<ClientDTO> AddAsync(ClientCreationDTO clienteCreacionDTO);
        Task UpdateAsync(int id, ClientCreationDTO clienteCreacionDTO);
        Task DeleteAsync(int id);
        Task<List<ClientSearchDTO>> SearchClientsAsync(string query);

    }
}
