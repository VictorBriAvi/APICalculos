using APICalculos.Application.DTOs.SaleDetail;

namespace APICalculos.Application.DTOs.Sale
{
    // Lo que manda el frontend al crear/editar una venta
    public class SaleCreationDTO
    {
        public int ClientId { get; set; }
        public List<SalePaymentCreationDTO> Payments { get; set; }
        public List<SaleDetailCreationDTO> SaleDetails { get; set; }
    }

    // Un medio de pago dentro del POST — el frontend solo manda estos dos campos.
    // El backend calcula todo lo demás.
    public class SalePaymentCreationDTO
    {
        public int PaymentTypeId { get; set; }
        public decimal AmountPaid { get; set; }
    }
}