using APICalculos.Application.DTOs.Reports;

namespace APICalculos.Application.Interfaces
{
    public interface IFinancialReportService
    {
        Task<FinancialSummaryDTO> GetFinancialSummaryAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<IEnumerable<DailyFinancialDTO>> GetDailyFinancialSummaryAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<EmployeeSalesSummaryDTO>> GetEmployeeSalesSummaryAsync(DateTime fromDate, DateTime toDate);
        Task<List<SalesByPaymentReportDTO>> GetSalesReportByPaymentTypeAsync(DateTime start, DateTime end);
        //Task<List<PaymentTypeBalanceDTO>> GetSalesSummaryByPaymentTypeAsync(DateTime start, DateTime end);

        Task<List<PaymentTypeBalanceDTO>> GetPaymentTypeBalanceAsync(
            DateTime start,
            DateTime end);
        Task<IEnumerable<ExpensesByCategoryDTO>> GetExpensesByCategoryAsync(DateTime? fromDate = null, DateTime? toDate = null);



    }
}
