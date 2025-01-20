using APICalculos.Entidades;

namespace APICalculos.DTOs
{
    public class VentaDTO
    {

        public int VentaId { get; set; }


        public string NombreCliente { get; set; }
        public string NombreTipoDePago { get; set; }

        public decimal ValorTotal { get; set; }
        public DateTime FechaVenta { get; set; }
        public List<DetalleVentaDTO> Detalle { get; set; }


    }
}
