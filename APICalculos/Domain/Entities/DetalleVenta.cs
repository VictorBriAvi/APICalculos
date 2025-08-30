namespace APICalculos.Domain.Entidades
{
    public class DetalleVenta
    {
        public int DetalleVentaId { get; set; }
        public int VentaId { get; set; }
        public int TipoDeServicioId { get; set; }
        public int EmpleadoId { get; set; }
        public Venta Venta { get; set; }
        public ServiceType TipoDeServicio { get; set; }
        public EmployeeModel Empleado { get; set; }


            public decimal Precio
            {
                get
                {
                    // Retorna el precio del servicio más el aumento si es necesario
                    return TipoDeServicio.Price;
                }
            }

    }
}
