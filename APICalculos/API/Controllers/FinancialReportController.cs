using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/report")]
    [Authorize]
    public class FinancialReportController : BaseController
    {
        private readonly IFinancialReportService _service;

        public FinancialReportController(IFinancialReportService service)
        {
            _service = service;
        }

        // =========================================
        // SUMMARY
        // =========================================
        [HttpGet("summary")]
        public async Task<IActionResult> GetFinancialSummary(DateTime? fromDate, DateTime? toDate)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetFinancialSummaryAsync(storeId, fromDate, toDate);
            return Ok(result);
        }

        // =========================================
        // DAILY SUMMARY
        // =========================================
        [HttpGet("daily-summary")]
        public async Task<IActionResult> GetDailyFinancialSummary(DateTime fromDate, DateTime toDate)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetDailyFinancialSummaryAsync(storeId, fromDate, toDate);
            return Ok(result);
        }

        // =========================================
        // EMPLOYEE SALES SUMMARY
        // =========================================
        [HttpGet("employee-sales-summary")]
        public async Task<IActionResult> GetEmployeeSalesSummary(DateTime fromDate, DateTime toDate)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetEmployeeSalesSummaryAsync(storeId, fromDate, toDate);
            return Ok(result);
        }

        // =========================================
        // SALES BY PAYMENT
        // =========================================
        [HttpGet("sales-by-payment")]
        public async Task<IActionResult> GetSalesByPaymentReport(DateTime startDate, DateTime endDate)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetSalesReportByPaymentTypeAsync(storeId, startDate, endDate);
            return Ok(result);
        }

        // =========================================
        // PAYMENT TYPE BALANCE
        // =========================================
        [HttpGet("payment-type-balance")]
        public async Task<IActionResult> GetPaymentTypeBalance(DateTime startDate, DateTime endDate)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetPaymentTypeBalanceAsync(storeId, startDate, endDate);
            return Ok(result);
        }

        // =========================================
        // EXPENSES BY CATEGORY
        // =========================================
        [HttpGet("expenses-by-category")]
        public async Task<IActionResult> GetExpensesByCategory(DateTime? fromDate, DateTime? toDate)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetExpensesByCategoryAsync(storeId, fromDate, toDate);
            return Ok(result);
        }
    }
}
