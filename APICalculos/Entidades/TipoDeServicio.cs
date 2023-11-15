namespace APICalculos.Entidades
{
    public class TipoDeServicio
    {
        public int TipoDeServicioId { get; set; }
        public string NombreServicio { get; set; }
        public decimal PrecioServicio { get; set; }
        public decimal PrecioServicioAumento { 
            get { return PrecioServicio * 1.20m; } 
        }

        public int CategoriasServiciosId { get; set; }
        public CategoriasServicios CategoriasServicios { get; set; }
    }
}
