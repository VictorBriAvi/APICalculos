using APICalculos.Application.DTOs.Reports;

namespace APICalculos.Application.Interfaces
{
    public interface IFinancialReportRepository
    {
        Task<FinancialSummaryDTO> GetFinancialSummaryAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<IEnumerable<DailyFinancialDTO>> GetDailyFinancialSummaryAsync(DateTime fromDate , DateTime toDate);
        Task<IEnumerable<EmployeeSalesSummaryDTO>> GetEmployeeSalesSummaryAsync(DateTime fromDate, DateTime toDate);
        Task<List<SalesByPaymentReportDTO>> GetSalesReportByPaymentTypeAsync(DateTime startDate, DateTime endDate);
        Task<List<SalesByPaymentSummaryDTO>> GetSalesSummaryByPaymentTypeAsync(DateTime startDate, DateTime endDate);

    }
}
