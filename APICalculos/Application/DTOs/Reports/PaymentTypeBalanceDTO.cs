namespace APICalculos.Application.DTOs.Reports
{
    public class PaymentTypeBalanceDTO
    {
        public int PaymentTypeId { get; set; }
        public string MedioDePago { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal TotalAppDiscount { get; set; }
        public decimal TotalNeto { get; set; }
        public decimal TotalGastos { get; set; }
        // TotalNeto - TotalGastos
        public decimal TotalDisponible { get; set; }
    }
}
