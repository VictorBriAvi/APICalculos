using APICalculos.Entidades;

namespace APICalculos.DTOs
{
    public class TipoDeServicioDTO
    {
        public int TipoDeServicioId { get; set; }
        public string NombreServicio { get; set; }
        public decimal PrecioServicio { get; set; }
        public decimal PrecioServicioAumento
        {
            get { return PrecioServicio * 1.20m; }
        }

        public string  NombreCategoriaServicio { get; set; }


    }
}
