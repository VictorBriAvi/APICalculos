namespace APICalculos.Entidades
{
    public class Venta
    {
            public int VentaId { get; set; }
            public int ClienteId { get; set; }
            public int TipoDePagoId { get; set; }
            public DateTime FechaVenta { get; set; }

            public Cliente Cliente { get; set; }
            public TipoDePago TipoDePago { get; set; }
        
            public ICollection<DetalleVenta> Detalle { get; set; }


            // Propiedad calculada para obtener el valor total
            public decimal ValorTotal
            {
                get
                {
                    // Sumamos los precios de todos los detalles asociados a la venta
                    return Detalle?.Sum(d => d.Precio) ?? 0;
                }
            }

        // Propiedad calculada para obtener el nombre del cliente desde el primer detalle
            public string NombreCliente => Detalle?.FirstOrDefault()?.Venta?.Cliente?.NombreCompletoCliente;

            // Propiedad calculada para obtener el nombre del tipo de pago desde el primer detalle
            public string NombreTipoDePago => Detalle?.FirstOrDefault()?.Venta?.TipoDePago?.NombreTipoDePago;
    }
}
