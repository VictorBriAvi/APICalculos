namespace APICalculos.Domain.Entidades
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityDocument { get; set; }
        public DateTime DateBirth { get; set; }
   
        public ICollection<Sale> Sale { get; set; }
        public ICollection<CustomerHistory> CustomerHistories { get; set; }

    }
}
