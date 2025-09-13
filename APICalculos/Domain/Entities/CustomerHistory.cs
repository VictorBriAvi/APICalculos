namespace APICalculos.Domain.Entidades
{
    public class CustomerHistory
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime  DateHistory{ get; set; }
        public int ClientId { get; set; }
        public ClientModel Client { get; set;}

    }
}
