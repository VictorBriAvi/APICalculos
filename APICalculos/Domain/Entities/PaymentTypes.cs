using APICalculos.Domain.Entities;

namespace APICalculos.Domain.Entidades
{
    public class PaymentTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<SalePayment> SalePayments { get; set; }

        public int? StoreId { get; set; }
        public Store? Store { get; set; }


    }
}
