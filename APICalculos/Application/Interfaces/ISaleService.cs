using APICalculos.Application.DTOs.Sale;

namespace APICalculos.Application.Interfaces
{
    public interface ISaleService
    {
        Task<List<SaleDTO>> GetAllSaleAsync(int storeId);

        //Task<List<SaleDTO>> GetSalesByDateRangeAsync(
        //    int storeId,
        //    DateTime fromDate,
        //    DateTime toDate);

        Task<List<SaleDTO>> GetFilteredSalesAsync(
                    int storeId,
                    DateTime? fromDate,
                    DateTime? toDate,
                    int? clientId,
                    int? paymentTypeId,
                    int? employeeId,
                    int? serviceTypeId);

        Task<SaleDTO?> GetSaleForId(int id, int storeId);

        Task<SaleDTO> AddSaleWithDetailsAsync(
            int storeId,
            SaleCreationDTO dto);

        Task UpdateSaleAsync(
            int id,
            int storeId,
            SaleCreationDTO dto);

        Task DeleteSaleAsync(int id, int storeId);
    }
}
