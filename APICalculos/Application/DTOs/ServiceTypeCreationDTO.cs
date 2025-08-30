using APICalculos.Domain.Entidades;

namespace APICalculos.Application.DTOs
{
    public class ServiceTypeCreationDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int ServiceCategorieId { get; set; }
    }
}
