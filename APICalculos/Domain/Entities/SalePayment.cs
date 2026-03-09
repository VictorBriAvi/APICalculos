using APICalculos.Domain.Entidades;

namespace APICalculos.Domain.Entities
{
    public class SalePayment
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int PaymentTypeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int StoreId { get; set; }

        // Lo que entró por este medio de pago (lo que el cliente entregó)
        public decimal AmountPaid { get; set; }

        // Descuento que cobra la app/procesador al negocio (ej: 3% débito)
        // No es el recargo al cliente — es lo que el negocio pierde por usar ese medio
        public decimal AppDiscountPercent { get; set; }
        public decimal AppDiscountAmount { get; set; }

        // Lo que el negocio realmente cobra después del descuento de la app
        public decimal NetAmountReceived { get; set; }

        public Sale Sale { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public Store Store { get; set; }
    }
}