using APICalculos.Application.DTOs.SaleDetail;

namespace APICalculos.Application.Interfaces
{
    public interface ISaleDetailService
    {
        Task<List<SaleDetailDTO>> GetAllSaleDetailAsync(int storeId);
        Task<SaleDetailDTO> GetSaleDetailForId(int id, int storeId);
        Task<SaleDetailDTO> AddSaleDetailAsync(SaleDetailCreationDTO dto, int storeId);
        Task UpdateSaleDetailAsync(int id, SaleDetailCreationDTO dto, int storeId);
        Task DeleteSaleDetailAsync(int id, int storeId);
    }
}
