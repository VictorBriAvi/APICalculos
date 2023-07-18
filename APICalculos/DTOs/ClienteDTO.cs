namespace APICalculos.DTOs
{
    public class ClienteDTO
    {
        public int ClienteId { get; set; }
        public string NombreCompletoCliente { get; set; }
        public string NumeroDocumento { get; set; }
        public string Historial { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}
