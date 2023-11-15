namespace APICalculos.Entidades
{
    public class Gastos
    {

        public int GastosId { get; set; }
        public string DescripcionGastos { get; set; }
        public decimal PrecioGasto { get; set; }
        public DateTime FechaGastos { get; set; }

        // Establezco una clave foranea para la relacion con tiposDeGastos
        public int TipoDeGastosId { get; set; }
        public TiposDeGastos TiposDeGastos { get; set; }


    }
}
