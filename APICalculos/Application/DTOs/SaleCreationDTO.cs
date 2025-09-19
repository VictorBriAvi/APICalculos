namespace APICalculos.Application.DTOs
{
    public class SaleCreationDTO
    {
        public int ClientId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime DateSale { get; set; }
    }
}
