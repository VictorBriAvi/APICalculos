namespace APICalculos.Application.DTOs.Reports
{
    public class DailyFinancialDTO
    {
        public DateTime Fecha { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        // Bruto del día (servicios + recargo)
        public decimal TotalVentas { get; set; }
        // Desglose del recargo del día
        public decimal TotalRecargo { get; set; }
        // Pagos a colaboradores del día (sobre precio base)
        public decimal TotalPagosColaboradores { get; set; }
        // Gastos del día
        public decimal TotalGastos { get; set; }
        // TotalVentas - TotalPagosColaboradores - TotalGastos
        public decimal TotalGanancia { get; set; }
    }
}
