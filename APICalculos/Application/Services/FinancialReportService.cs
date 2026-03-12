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
            ValidateStore(storeId);
            NormalizeDateRange(ref fromDate, ref toDate);
            return _repository.GetFinancialSummaryAsync(storeId, fromDate, toDate);
        }

        public Task<IEnumerable<DailyFinancialDTO>> GetDailyFinancialSummaryAsync(
            int storeId,
            DateTime fromDate,
            DateTime toDate)
        {
            ValidateStore(storeId);
            ValidateDateRange(fromDate, toDate);
            return _repository.GetDailyFinancialSummaryAsync(storeId, fromDate.Date, toDate.Date);
        }

        public Task<IEnumerable<EmployeeSalesSummaryDTO>> GetEmployeeSalesSummaryAsync(
            int storeId,
            DateTime fromDate,
            DateTime toDate)
        {
            ValidateStore(storeId);
            ValidateDateRange(fromDate, toDate);
            return _repository.GetEmployeeSalesSummaryAsync(storeId, fromDate.Date, toDate.Date);
        }

        public Task<List<SalesByPaymentReportDTO>> GetSalesReportByPaymentTypeAsync(
            int storeId,
            DateTime startDate,
            DateTime endDate)
        {
            ValidateStore(storeId);
            ValidateDateRange(startDate, endDate);
            return _repository.GetSalesReportByPaymentTypeAsync(storeId, startDate.Date, endDate.Date);
        }

        public Task<List<PaymentTypeBalanceDTO>> GetPaymentTypeBalanceAsync(
            int storeId,
            DateTime startDate,
            DateTime endDate)
        {
            ValidateStore(storeId);
            ValidateDateRange(startDate, endDate);
            return _repository.GetPaymentTypeBalanceAsync(storeId, startDate.Date, endDate.Date);
        }

        public Task<IEnumerable<ExpensesByCategoryDTO>> GetExpensesByCategoryAsync(
            int storeId,
            DateTime? fromDate = null,
            DateTime? toDate = null)
        {
            ValidateStore(storeId);
            NormalizeDateRange(ref fromDate, ref toDate);
            return _repository.GetExpensesByCategoryAsync(storeId, fromDate, toDate);
        }

        // ── Helpers ───────────────────────────────────────────────────────────

        private static void ValidateStore(int storeId)
        {
            if (storeId <= 0)
                throw new ArgumentException("StoreId inválido.");
        }

        private static void ValidateDateRange(DateTime from, DateTime to)
        {
            if (from > to)
                throw new ArgumentException(
                    $"La fecha de inicio ({from:dd/MM/yyyy}) no puede ser mayor que la de fin ({to:dd/MM/yyyy}).");

            if ((to - from).TotalDays > 366)
                throw new ArgumentException(
                    "El rango no puede superar 366 días.");
        }

        private static void NormalizeDateRange(ref DateTime? from, ref DateTime? to)
        {
            if (from.HasValue && to.HasValue && from.Value > to.Value)
                throw new ArgumentException("La fecha de inicio no puede ser mayor que la de fin.");

            if (from.HasValue) from = from.Value.Date;
            if (to.HasValue) to = to.Value.Date.AddDays(1).AddTicks(-1);
        }
    }
}