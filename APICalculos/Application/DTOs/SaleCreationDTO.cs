namespace APICalculos.Application.DTOs
{
    public class SaleCreationDTO
    {
        public int ClientId { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<SaleDetailCreationDTO> SaleDetails { get; set; } = new List<SaleDetailCreationDTO>();

    }
}
