namespace APICalculos.Application.DTOs
{
    public class SalePaymentDTO
    {
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AppliedDiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
    }
}
