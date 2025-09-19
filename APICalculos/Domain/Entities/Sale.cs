namespace APICalculos.Domain.Entidades
{
    public class Sale
    {
            public int Id { get; set; }
            public int ClientId { get; set; }
            public int PaymentTypeId { get; set; }
            public decimal TotalAmount { get; set; }
            public DateTime DateSale { get; set; }

            public Client Client { get; set; }
            public PaymentType PaymentType { get; set; }
            public ICollection<SaleDetail> SaleDetail { get; set; }
    }
}
