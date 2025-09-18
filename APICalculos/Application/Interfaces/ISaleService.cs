using APICalculos.Application.DTOs;

namespace APICalculos.Application.Interfaces
{
    public interface ISaleService
    {
        Task<List<SaleDTO>> GetAllSaleAsync();
        Task<SaleDTO> GetSaleForId(int id);
        Task<SaleDTO> AddSaleAsync(SaleCreationDTO saleCreationDTO);
        Task UpdateSaleAsync(int id, SaleCreationDTO saleCreationDTO);
        Task DeleteSaleAsync(int Id);
    }
}
