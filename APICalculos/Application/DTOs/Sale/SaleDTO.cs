using APICalculos.Application.DTOs.SaleDetail;
using System.ComponentModel.DataAnnotations;

namespace APICalculos.Application.DTOs.Sale
{
    public class SaleDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El cliente es obligatorio")]
        public int ClientId { get; set; }
        public string NameClient { get; set; }

        public string DateSale { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Recargo a nivel venta
        public decimal SurchargePercent { get; set; }
        public decimal SurchargeAmount { get; set; }

        // Totales
        public decimal BaseAmount { get; set; }   // lo que valían los servicios
        public decimal TotalAmount { get; set; }  // lo que pagó el cliente (con recargo)

        public List<SaleDetailDTO> SaleDetail { get; set; }
        public List<SalePaymentDTO> Payments { get; set; }
    }

    // Lo que devuelve el backend al leer un pago de una venta
    public class SalePaymentDTO
    {
        public int Id { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }

        public decimal AmountPaid { get; set; }          // lo que entró por este medio
        public decimal AppDiscountPercent { get; set; }  // % que cobra la app
        public decimal AppDiscountAmount { get; set; }   // monto que cobra la app
        public decimal NetAmountReceived { get; set; }   // lo que el negocio realmente cobra
    }
}