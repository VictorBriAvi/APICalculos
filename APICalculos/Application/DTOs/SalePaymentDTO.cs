namespace APICalculos.Application.DTOs
{
    public class SalePaymentDTO
    {
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal AmountPaid { get; set; }
    }
}
