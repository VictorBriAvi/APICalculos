using APICalculos.Domain.Entidades;

namespace APICalculos.Domain.Entities
{
    public class SalePayment
    {
        public int Id { get; set; }

        public int SaleId { get; set; }
        public int PaymentTypeId { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal AppliedDiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal AppliedSurchargePercent { get; set; }
        public decimal SurchargeAmount { get; set; }
        public decimal FinalAmount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Sale Sale { get; set; }
        public PaymentTypes PaymentType { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }


    }
}
