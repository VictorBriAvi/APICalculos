using APICalculos.Application.DTOs.Reports;
using APICalculos.Application.Interfaces;
using APICalculos.Infrastructure.Repositories;
using APICalculos.Infrastructure.Repositories.Reports;
using System.Globalization;

namespace APICalculos.Application.Services
{
    public class FinancialReportService : IFinancialReportService
    {
        private readonly IFinancialReportRepository _financialReportRepository;
        
        public FinancialReportService (IFinancialReportRepository financialReportRepository)
        {
            _financialReportRepository = financialReportRepository;
        }

        public async Task<FinancialSummaryDTO> GetFinancialSummaryAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {

            if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
                throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha final.");

            var summary = await _financialReportRepository.GetFinancialSummaryAsync(fromDate, toDate);



            return summary;
        }

        public async Task<IEnumerable<DailyFinancialDTO>> GetDailyFinancialSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
                throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha de fin.");

            var report = await _financialReportRepository.GetDailyFinancialSummaryAsync(fromDate, toDate);

            var finalReport = report.Select(r => new DailyFinancialDTO
            {
                Fecha = r.Fecha,
                TotalVentas = r.TotalVentas,
                TotalGastos = r.TotalGastos,
                DiaSemana = r.Fecha.DayOfWeek
            });

            return report;
        }

        public async Task<IEnumerable<EmployeeSalesSummaryDTO>> GetEmployeeSalesSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
                throw new ArgumentException("La fecha de inicio no puede ser mayor que la fecha final.");

            var summary = await _financialReportRepository.GetEmployeeSalesSummaryAsync(fromDate, toDate);
            return summary;
        }

        public async Task<List<SalesByPaymentReportDTO>> GetSalesReportByPaymentTypeAsync(DateTime start, DateTime end)
        {
            return await _financialReportRepository.GetSalesReportByPaymentTypeAsync(start, end);
        }

        public async Task<List<SalesByPaymentSummaryDTO>> GetSalesSummaryByPaymentTypeAsync(DateTime start, DateTime end)
        {
            return await _financialReportRepository.GetSalesSummaryByPaymentTypeAsync(start, end);
        }


    }
}
