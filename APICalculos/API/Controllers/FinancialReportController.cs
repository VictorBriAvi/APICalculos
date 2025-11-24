using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class FinancialReportController : ControllerBase
    {
        private readonly IFinancialReportService _financialReportService;

        public FinancialReportController(IFinancialReportService financialReportService)
        {
            _financialReportService = financialReportService;
        }

        /// <summary>
        /// Obtiene el resumen financiero (ventas, pagos y gastos)
        /// </summary>
        /// <param name="fromDate">Fecha de inicio (opcional)</param>
        /// <param name="toDate">Fecha de fin (opcional)</param>
        /// <returns>Totales de ventas, pagos y gastos</returns>
        [HttpGet("summary")]
        public async Task<IActionResult> GetFinancialSummary([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            try
            {
                var result = await _financialReportService.GetFinancialSummaryAsync(fromDate, toDate);

                return Ok(new
                {

                    data = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                // 🔥 Nunca expongas excepciones internas en producción
                return StatusCode(500, new { success = false, message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpGet("daily-summary")]
        public async Task<IActionResult> GetDailyFinancialSummary([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            try
            {
                var result = await _financialReportService.GetDailyFinancialSummaryAsync(fromDate, toDate);
                return Ok(new
                {
                    success = true,
                    message = "Resumen diario obtenido correctamente",
                    data = result
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Error interno del servidor", error = ex.Message });
            }
        }

        [HttpGet("employee-sales-summary")]
        public async Task<IActionResult> GetEmployeeSalesSummary([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var result = await _financialReportService.GetEmployeeSalesSummaryAsync(fromDate, toDate);
            return Ok(result);
        }

        [HttpGet("sales-by-payment")]
        public async Task<IActionResult> GetSalesByPaymentReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var report = await _financialReportService.GetSalesReportByPaymentTypeAsync(startDate, endDate);
            return Ok(report);
        }

        [HttpGet("sales-summary-by-payment")]
        public async Task<IActionResult> GetSalesSummaryByPayment([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var report = await _financialReportService.GetSalesSummaryByPaymentTypeAsync(startDate, endDate);
            return Ok(report);
        }

    }
}
