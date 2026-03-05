namespace APICalculos.Application.DTOs.PaymentType
{
    public class PaymentTypeCreationDTO
    {
        public string Name { get; set; }
        public bool ApplyDiscount { get; set; }
        public decimal DiscountPercent { get; set; }
    }
}
