namespace APICalculos.Application.DTOs.Reports
{
    public class FinancialSummaryDTO
    {
        public decimal TotalVentas { get; set; }
        public decimal TotalPagosColaboradores { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal GananciaNeta => TotalVentas - (TotalPagosColaboradores + TotalGastos);
    }
}
