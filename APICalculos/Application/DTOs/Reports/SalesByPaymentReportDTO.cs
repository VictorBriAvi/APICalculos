namespace APICalculos.Application.DTOs.Reports
{
    public class SalesByPaymentReportDTO
    {
        public DateTime Fecha { get; set; }
        public string MedioDePago { get; set; }
        public decimal TotalRecaudado { get; set; }
    }
}
