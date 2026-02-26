using APICalculos.Application.DTOs.CustomerHistory;

namespace APICalculos.Application.Interfaces
{
    public interface ICustomerHistoryService
    {
        Task<List<CustomerHistoryDTO>> GetAllCustomerHistoriesAsync(int storeId);
        Task<CustomerHistoryDTO?> GetCustomerHistoryForId(int id, int storeId);
        Task<CustomerHistoryDTO> AddCustomerHistoryAsync(int storeId, CustomerHistoryCreationDTO dto);
        Task UpdateCustomerHistoryAsync(int id, int storeId, CustomerHistoryUpdateDTO dto);
        Task DeleteCustomerHistoriesAsync(int id, int storeId);
    }
}
