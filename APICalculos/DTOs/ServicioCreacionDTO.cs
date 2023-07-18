using APICalculos.Entidades;

namespace APICalculos.DTOs
{
    public class ServicioCreacionDTO
    {
        public decimal ValorServicio { get; set; }
        public int TipoDeServicioId { get; set; }
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public int TipoDePagoId { get; set; }

    }
}
