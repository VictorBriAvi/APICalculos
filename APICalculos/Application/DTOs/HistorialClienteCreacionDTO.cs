using APICalculos.Domain.Entidades;


namespace APICalculos.Application.DTOs
{
    public class HistorialClienteCreacionDTO
    {
        public string NombreDeHistorialCliente { get; set; }
        public string DescripcionHistorialCliente { get; set; }
        public DateTime FechaHistorial { get; set; }

        public int ClienteId { get; set; }

    }
}
