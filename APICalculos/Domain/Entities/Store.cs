using APICalculos.Domain.Entidades;

namespace APICalculos.Domain.Entities
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateOn { get; set; }

        // Navegación
        public ICollection<User> Users { get; set; }
    }
}
