namespace APICalculos.Application.DTOs.Reports
{
    public class EmployeeSalesSummaryDTO
    {
        public int EmpleadoId { get; set; }
        public string Empleado { get; set; }
        public decimal TotalVentas { get; set; }
        public int PorcentajePago { get; set; }
        public decimal TotalAPagar { get; set; }
        public int TotalServicios { get; set; }

        public List<ServiceCountDTO> ServiciosRealizados { get; set; }

    }

    public class ServiceCountDTO
    {
        public string Servicio { get; set; }
        public int Cantidad { get; set; }
    }
}
