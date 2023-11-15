using APICalculos.Entidades;

namespace APICalculos.DTOs
{
    public class TipoDeServicioCreacionDTO
    {
        public string NombreServicio { get; set; }
        public decimal PrecioServicio { get; set; }
        public int CategoriasServiciosId { get; set; }

    }
}
