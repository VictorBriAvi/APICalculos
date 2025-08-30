using APICalculos.Domain.Entidades;

namespace APICalculos.Application.DTOs
{
    public class ServicioDTO
    {
        public int ServicioId { get; set; }
        public string NombreCompletoEmpleado { get; set; }
        public string NombreCompletoCliente { get; set; }
        public string NombreTipoDePago { get; set; }
        public string NombreServicio { get; set; }
        public decimal ValorServicio { get; set; }

        public DateTime FechaIngresoServicio { get; set; }
        public string FechaServicioFormateada => FechaIngresoServicio.ToString("dd-MM-yyyy");
    }
}
