using APICalculos.Application.DTOs.Reports;
using APICalculos.Application.Interfaces;

namespace APICalculos.Application.Services
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly IFinancialReportRepository _repository;

        public FinancialReportService(IFinancialReportRepository repository)
        {
            _repository = repository;
        }

        public Task<FinancialSummaryDTO> GetFinancialSummaryAsync(
            int storeId,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            return _repository.GetFinancialSummaryAsync(storeId, fromDate, toDate);
        }

        public Task<IEnumerable<DailyFinancialDTO>> GetDailyFinancialSummaryAsync(
            int storeId,
            DateTime fromDate,
            DateTime toDate)
        {
            return _repository.GetDailyFinancialSummaryAsync(storeId, fromDate, toDate);
        }

        public Task<IEnumerable<EmployeeSalesSummaryDTO>> GetEmployeeSalesSummaryAsync(
            int storeId,
            DateTime fromDate,
            DateTime toDate)
        {
            return _repository.GetEmployeeSalesSummaryAsync(storeId, fromDate, toDate);
        }

        public Task<List<SalesByPaymentReportDTO>> GetSalesReportByPaymentTypeAsync(
            int storeId,
            DateTime start,
            DateTime end)
        {
            return _repository.GetSalesReportByPaymentTypeAsync(storeId, start, end);
        }

        public Task<List<PaymentTypeBalanceDTO>> GetPaymentTypeBalanceAsync(
            int storeId,
            DateTime start,
            DateTime end)
        {
            return _repository.GetPaymentTypeBalanceAsync(storeId, start, end);
        }

        public Task<IEnumerable<ExpensesByCategoryDTO>> GetExpensesByCategoryAsync(
            int storeId,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            return _repository.GetExpensesByCategoryAsync(storeId, fromDate, toDate);
        }
    }
}
