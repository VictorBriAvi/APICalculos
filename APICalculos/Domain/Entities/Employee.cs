using APICalculos.Domain.Entities;

namespace APICalculos.Domain.Entidades
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityDocument { get; set; }
        public DateTime DateBirth { get; set; }
        public int PaymentPercentage { get; set; }
        public ICollection<SaleDetail> SaleDetail { get; set; }

        public int? StoreId { get; set; }
        public Store? Store { get; set; }


    }
}
