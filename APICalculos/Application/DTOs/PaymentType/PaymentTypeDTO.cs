namespace APICalculos.Application.DTOs.PaymentType
{
    public class PaymentTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ApplyDiscount { get; set; }
        public decimal DiscountPercent { get; set; }
        public string ApplyDiscountStr => ApplyDiscount ? "Sí" : "No";
        public bool ApplySurcharge { get; set; }
        public string ApplySurchargeStr => ApplySurcharge ? "Sí" : "No";
        public decimal SurchargePercent { get; set; }
    }
}
