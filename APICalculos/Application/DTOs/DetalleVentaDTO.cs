using APICalculos.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APICalculos.Application.DTOs
{
    public class DetalleVentaDTO
    {
        public int DetalleVentaId { get; set; }
        public string NombreClienteVenta { get; set; }
        public string NombreTipoDeServicioVenta { get; set; }
        public decimal PrecioTipoDeServicio { get; set; }
        public string NombreEmpleadoVenta { get; set; }
    }
}
