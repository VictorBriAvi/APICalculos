namespace APICalculos.Application.DTOs.Reports
{
    public class SalesByPaymentReportDTO
    {
        public DateTime Fecha { get; set; }
        public string MedioDePago { get; set; }
        // Lo que entró por ese medio
        public decimal TotalRecaudado { get; set; }
        // Lo que la app se quedó de ese medio
        public decimal TotalAppDiscount { get; set; }
        // Lo que el negocio realmente cobró
        public decimal TotalNeto { get; set; }
    }
}
