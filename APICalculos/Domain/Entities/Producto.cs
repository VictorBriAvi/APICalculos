namespace APICalculos.Domain.Entidades
{
    public class Producto
    {
        public int ProductoId { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string DescripcionProducto { get; set; }
        public decimal PrecioProducto { get; set; }
        public int Stock { get; set; }
    }
}
