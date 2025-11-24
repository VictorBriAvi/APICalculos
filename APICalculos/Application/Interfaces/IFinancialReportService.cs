using APICalculos.Application.DTOs.Reports;

namespace APICalculos.Application.Interfaces
{
    public interface IFinancialReportService
    {
        Task<FinancialSummaryDTO> GetFinancialSummaryAsync(DateTime? fromDate = null, DateTime? toDate = null);

        Task<IEnumerable<DailyFinancialDTO>> GetDailyFinancialSummaryAsync(DateTime fromDate, DateTime toDate);

        Task<IEnumerable<EmployeeSalesSummaryDTO>> GetEmployeeSalesSummaryAsync(DateTime fromDate, DateTime toDate);

        Task<List<SalesByPaymentReportDTO>> GetSalesReportByPaymentTypeAsync(DateTime start, DateTime end);
        Task<List<SalesByPaymentSummaryDTO>> GetSalesSummaryByPaymentTypeAsync(DateTime start, DateTime end);

    }
}
