using APICalculos.Domain.Entidades;

namespace APICalculos.Application.DTOs
{
    public class ServicioCreacionDTO
    {
        public decimal ValorServicio { get; set; }
        public int TipoDeServicioId { get; set; }
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public int TipoDePagoId { get; set; }
        public DateTime FechaIngresoServicio { get; set; }
    }
}
