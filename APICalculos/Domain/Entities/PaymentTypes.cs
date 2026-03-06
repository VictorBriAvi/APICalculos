using APICalculos.Domain.Entities;

namespace APICalculos.Domain.Entidades
{
    public class PaymentTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ApplyDiscount { get; set; } = false;
        public decimal DiscountPercent { get; set; } = 0;
        public bool ApplySurcharge { get; set; } = false;
        public decimal SurchargePercent { get; set; } = 0;
        public ICollection<SalePayment> SalePayments { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }


    }
}
