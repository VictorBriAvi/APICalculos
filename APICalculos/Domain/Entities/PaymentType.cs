using APICalculos.Domain.Entities;

namespace APICalculos.Domain.Entidades
{
    public class PaymentType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<SalePayment> SalePayments { get; set; }
    }
}
