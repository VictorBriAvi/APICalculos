namespace APICalculos.Entidades
{
    public class CategoriasServicios
    {

        public int CategoriasServiciosId { get; set; }
        public string NombreCategoriaServicio { get; set; }

        public ICollection<TipoDeServicio> TipoDeServicios { get; set; }
    }
}
