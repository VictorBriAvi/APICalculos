using APICalculos.Application.DTOs.SaleDetail;
using APICalculos.Domain.Entidades;
using System.ComponentModel.DataAnnotations;

namespace APICalculos.Application.DTOs.Sale
{
    public class SaleDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClientId { get; set; }
        public string NameClient { get; set; }

        public decimal TotalAmount { get; set; }
        public DateTime DateSale { get; set; }
        public bool IsDeleted { get; set; } = false;

        public List<SaleDetailDTO> SaleDetail { get; set; }

        // 👇 Nueva propiedad: lista de métodos de pago
        public List<SalePaymentDTO> Payments { get; set; }
    }

}
