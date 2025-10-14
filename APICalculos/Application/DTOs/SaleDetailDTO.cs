using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace APICalculos.Application.DTOs
{
    public class SaleDetailDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El tipo de servicio es obligatorio")]
        public int ServiceTypeId {  get; set; }
        public string NameServiceTypeSale { get; set; }
        [Required(ErrorMessage = "El colaborador es obligatorio")]
        public int EmployeeId { get; set; }
        public string NameEmployeeSale { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal AdditionalCharge { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
