using APICalculos.Domain.Entidades;

namespace APICalculos.Application.DTOs.Services
{
    public class ServiceTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceIncrease { get;}
        public string  ServiceCategorieName { get; set; }
        public int ServiceCategorieId { get; set; }


    }
}
