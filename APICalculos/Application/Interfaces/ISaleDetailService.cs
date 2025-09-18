using APICalculos.Application.DTOs;

namespace APICalculos.Application.Interfaces
{
    public interface ISaleDetailService
    {
        Task<List<SaleDetailDTO>> GetAllSaleDetailAsync();
        Task<SaleDetailDTO> GetSaleDetailForId(int id);
        Task<SaleDetailDTO> AddSaleDetailAsync(SaleDetailCreationDTO saleDetailCreationDTO);
        Task UpdateSaleDetailAsync(int id, SaleDetailCreationDTO saleDetailCreationDTO);
        Task DeleteSaleDetailAsync(int id);
    }
}
