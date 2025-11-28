using APICalculos.Application.DTOs;

namespace APICalculos.Application.Interfaces
{
    public interface ISaleService
    {
        Task<List<SaleDTO>> GetAllSaleAsync();
        Task<List<SaleDTO>> GetSalesByTodayAsync();
        Task<SaleDTO> GetSaleForId(int id);
        Task<SaleDTO> AddSaleAsync(SaleCreationDTO saleCreationDTO);
        Task<SaleDTO> AddSaleWithDetailsAsync(SaleCreationDTO saleCreationDTO);
        Task UpdateSaleAsync(int id, SaleCreationDTO saleCreationDTO);
        Task DeleteSaleAsync(int Id);
    }
}
