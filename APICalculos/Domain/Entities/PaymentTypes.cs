using APICalculos.Domain.Entities;

namespace APICalculos.Domain.Entidades
{
    public class PaymentTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ApplyDiscount { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool ApplySurcharge { get; set; }
        public decimal SurchargePercent { get; set; }

        public ICollection<SalePayment> SalePayments { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }


    }
}
