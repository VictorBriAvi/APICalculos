using APICalculos.Entidades;

namespace APICalculos.DTOs
{
    public class GastosDTO
    {
        public int GastosId { get; set; }

        public string DescripcionGastos { get; set; }
        public decimal PrecioGasto { get; set; }
        public string NombreTipoDeGastos { get; set; }
        public DateTime FechaGastos { get; set; }

        public string FechaGastoFormateada => FechaGastos.ToString("dd-MM-yyyy");

    }
}
