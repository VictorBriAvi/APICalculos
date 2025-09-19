using APICalculos.Domain.Entidades;

namespace APICalculos.Application.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public string NameClient { get; set; }
        public string NamePaymentType { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateSale { get; set; }
        public List<SaleDetailDTO> SaleDetail { get; set; }
    }
}
