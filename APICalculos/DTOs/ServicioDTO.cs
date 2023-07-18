using APICalculos.Entidades;

namespace APICalculos.DTOs
{
    public class ServicioDTO
    {
        public int ServicioId { get; set; }
        public string NombreCompletoEmpleado { get; set; }
        public string NombreCompletoCliente { get; set; }
        public string NombreTipoDePago { get; set; }
        public string NombreServicio { get; set; }
        public decimal ValorServicio { get; set; }
    }
}
