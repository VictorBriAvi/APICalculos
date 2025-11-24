namespace APICalculos.Application.DTOs.Reports
{
    public class EmployeeSalesSummaryDTO
    {
        public string Empleado { get; set; }
        public decimal TotalVentas { get; set; }
        public int PorcentajePago { get; set; }
        public decimal TotalAPagar { get; set; }

    }
}
