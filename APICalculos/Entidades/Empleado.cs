namespace APICalculos.Entidades
{
    public class Empleado
    {
        public int EmpleadoId { get; set; }
        public string NombreCompletoEmpleado { get; set; }
        public string DocumentoNacional { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public ICollection<DetalleVenta> DetalleVentas { get; set; }

    }
}
