namespace APICalculos.Application.DTOs
{
    public class HistorialClienteDTO
    {
        public int HistorialClientesId { get; set; }
        public string NombreDeHistorialCliente { get; set; }
        public string DescripcionHistorialCliente { get; set; }
        public DateTime FechaHistorial { get; set; }
        public string FechaClienteFormateada => FechaHistorial.ToString("dd-MM-yyyy");
        public string NombreCompletoCliente { get; set; }
    }
}
