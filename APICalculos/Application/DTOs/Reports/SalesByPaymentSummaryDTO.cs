namespace APICalculos.Application.DTOs.Reports
{
    public class SalesByPaymentSummaryDTO
    {
        public string MedioDePago { get; set; }
        public decimal TotalRecaudado { get; set; }
        public int CantidadOperaciones { get; set; }
    }
}
