namespace APICalculos.Application.DTOs.PaymentType
{
    public class PaymentTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ApplyDiscount { get; set; }
        public decimal DiscountPercent { get; set; }
        public string ApplyDiscountStr => ApplyDiscount ? "Sí" : "No";
    }
}
