namespace APICalculos.DTOs
{
    public class EmpleadoDTO
    {
        public int EmpleadoId { get; set; }
        public string NombreCompletoEmpleado { get; set; }
        public string DocumentoNacional { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public string FechaClienteFormateada => FechaNacimiento.ToString("dd-MM-yyyy");
    }
}
