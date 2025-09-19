namespace APICalculos.Domain.Entidades
{
    public class Servicio
    {
        public int ServicioId { get; set; }
        public int TipoDeServicioId { get; set; }
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public int TipoDePagoId { get; set; }
        public decimal ValorServicio { get; set; }
        public DateTime FechaIngresoServicio { get; set; }



        public PaymentType TipoDePago { get; set; }
        public Employee Empleado { get; set; }
        public Client Cliente { get; set; }
        public ServiceType TipoDeServicio { get; set; }
    }
}
