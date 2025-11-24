namespace APICalculos.Application.DTOs.Reports
{
    public class DailyFinancialDTO
    {
        public DateTime Fecha { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal TotalGastos { get; set; }
        public DayOfWeek DiaSemana { get; set; }
    }
}
