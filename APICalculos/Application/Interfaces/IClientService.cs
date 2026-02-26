using APICalculos.Application.DTOs.Client;

namespace APICalculos.Application.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientDTO>> GetAllClientsAsync(int storeId, string? search);
        Task<ClientDTO?> GetClientForIdAsync(int id, int storeId);
        Task<ClientDTO> AddAsync(int storeId, ClientCreationDTO dto);
        Task UpdateAsync(int id, int storeId, ClientUpdateDTO dto);
        Task DeleteAsync(int id, int storeId);
        Task<List<ClientSearchDTO>> SearchClientsAsync(int storeId, string query);


    }
}
