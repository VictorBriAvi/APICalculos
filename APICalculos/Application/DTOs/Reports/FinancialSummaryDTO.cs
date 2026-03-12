namespace APICalculos.Application.DTOs.Reports
{
    // ── Resumen financiero general ────────────────────────────────────────────
    public class FinancialSummaryDTO
    {
        public decimal TotalVentas { get; set; }

        public decimal TotalPagosColaboradores { get; set; }

        public decimal TotalGastos { get; set; }

        public decimal GananciaNeta { get; set; }
    }

}
