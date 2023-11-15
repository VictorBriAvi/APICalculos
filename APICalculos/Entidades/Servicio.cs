namespace APICalculos.Entidades
{
    public class Servicio
    {
        public int ServicioId { get; set; }
        public int TipoDeServicioId { get; set; }
        public TipoDeServicio TipoDeServicio { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
        public int TipoDePagoId { get; set; }
        public TipoDePago TipoDePago { get; set; }
        public decimal ValorServicio { get; set; }
        public DateTime FechaIngresoServicio { get; set; }

    }
}
