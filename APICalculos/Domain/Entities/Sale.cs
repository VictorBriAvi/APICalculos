namespace APICalculos.Domain.Entidades
{
    public class Sale
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateSale { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Client Client { get; set; }
        public PaymentType PaymentType { get; set; }
        public ICollection<SaleDetail> SaleDetail { get; set; }
        public void CalculateTotal()
        {
            TotalAmount = SaleDetail.Sum(d =>
                (d.UnitPrice + d.AdditionalCharge) * (1 - (d.DiscountPercent / 100))
            );
        }

    }
}
