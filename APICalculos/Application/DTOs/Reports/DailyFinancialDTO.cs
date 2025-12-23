namespace APICalculos.Application.DTOs.Reports
{
    public class DailyFinancialDTO
    {
        public DateTime Fecha { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal TotalPagosColaboradores { get; set; }
        public decimal TotalGastos { get; set; }
        public decimal TotalGanancia { get; set; }
        public DayOfWeek DiaSemana { get; set; }
    }
}
