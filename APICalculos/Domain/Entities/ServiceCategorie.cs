namespace APICalculos.Domain.Entidades
{
    public class ServiceCategorie
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ServiceType> ServiceTypes { get; set; }
    }
}
