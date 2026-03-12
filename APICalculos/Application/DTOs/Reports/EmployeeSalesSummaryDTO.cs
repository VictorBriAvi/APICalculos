namespace APICalculos.Application.DTOs.Reports
{
    // ── Resumen por empleado ──────────────────────────────────────────────────
    public class EmployeeSalesSummaryDTO
    {
        public int EmpleadoId { get; set; }
        public string Empleado { get; set; }
        public decimal PorcentajePago { get; set; }
        public int TotalServicios { get; set; }
        // Suma de los precios base de sus servicios (sin recargo — el recargo es de la venta)
        public decimal TotalVentas { get; set; }
        // TotalVentas * PorcentajePago / 100
        public decimal TotalAPagar { get; set; }
        public List<ServiceCountDTO> ServiciosRealizados { get; set; }
    }

    public class ServiceCountDTO
    {
        public string Servicio { get; set; }
        public int Cantidad { get; set; }
    }
}
