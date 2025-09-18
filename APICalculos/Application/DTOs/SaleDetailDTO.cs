using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Application.DTOs
{
    public class SaleDetailDTO
    {
        public int Id { get; set; }
        public string NameClientSale { get; set; }
        public string NameServiceTypeSale { get; set; }
        public decimal PriceServiceType { get; set; }
        public string NameEmployeeSale { get; set; }
    }
}
