namespace APICalculos.Application.DTOs.PaymentType
{
    public class PaymentTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ApplyDiscount { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool ApplySurcharge { get; set; }
        public decimal SurchargePercent { get; set; }
    }
}
