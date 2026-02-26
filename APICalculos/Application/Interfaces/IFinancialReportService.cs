using APICalculos.Application.DTOs.Reports;

namespace APICalculos.Application.Interfaces
{
    public interface IFinancialReportService
    {
        Task<FinancialSummaryDTO> GetFinancialSummaryAsync(int storeId, DateTime? fromDate = null, DateTime? toDate = null);

        Task<IEnumerable<DailyFinancialDTO>> GetDailyFinancialSummaryAsync(
            int storeId,
            DateTime fromDate,
            DateTime toDate);

        Task<IEnumerable<EmployeeSalesSummaryDTO>> GetEmployeeSalesSummaryAsync(
            int storeId,
            DateTime fromDate,
            DateTime toDate);

        Task<List<SalesByPaymentReportDTO>> GetSalesReportByPaymentTypeAsync(
            int storeId,
            DateTime start,
            DateTime end);

        Task<List<PaymentTypeBalanceDTO>> GetPaymentTypeBalanceAsync(
            int storeId,
            DateTime start,
            DateTime end);

        Task<IEnumerable<ExpensesByCategoryDTO>> GetExpensesByCategoryAsync(
            int storeId,
            DateTime? fromDate = null,
            DateTime? toDate = null);
    }
}
