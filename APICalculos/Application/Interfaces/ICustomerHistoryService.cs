using APICalculos.Application.DTOs;

namespace APICalculos.Application.Interfaces
{
    public interface ICustomerHistoryService
    {
        Task<List<CustomerHistoryDTO>> GetAllCustomerHistoriesAsync();
        Task<CustomerHistoryDTO> GetCustomerHistoryForId(int id);
        Task<CustomerHistoryDTO> AddCustomerHistoryAsync(CustomerHistoryCreationDTO customerHistoryCreationDTO);
        Task UpdateCustomerHistoryAsync(int id, CustomerHistoryUpdateDTO dto);
        Task DeleteCustomerHistoriesAsync(int id);
    }
}
