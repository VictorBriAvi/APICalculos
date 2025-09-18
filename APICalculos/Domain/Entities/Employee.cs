namespace APICalculos.Domain.Entidades
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityDocument { get; set; }
        public DateTime DateBirth { get; set; }

        public ICollection<SaleDetail> SaleDetail { get; set; }

    }
}
