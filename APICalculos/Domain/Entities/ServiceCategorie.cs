using APICalculos.Domain.Entities;

namespace APICalculos.Domain.Entidades
{
    public class ServiceCategorie
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ServiceType> ServiceTypes { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }


    }
}
