using APICalculos.Domain.Entidades;
using System.ComponentModel.DataAnnotations;

namespace APICalculos.Application.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClientId { get; set; }
        public string NameClient { get; set; }
        [Required(ErrorMessage = "El tipo de pago es obligatorio")]
        public int PaymentTypeId { get; set; }
        public string NamePaymentType { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateSale { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<SaleDetailDTO> SaleDetail { get; set; }
    }
}
