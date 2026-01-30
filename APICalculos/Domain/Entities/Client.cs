namespace APICalculos.Domain.Entidades
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? IdentityDocument { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateBirth { get; set; }
        public ICollection<Sale> Sale { get; set; }
        public ICollection<CustomerHistory> CustomerHistories { get; set; }

    }
}
