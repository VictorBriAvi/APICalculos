namespace APICalculos.Entidades
{
    public class TiposDeGastos
    {
        public int TipoDeGastosId { get; set; }
        public string NombreTipoDeGastos { get; set; }
        public ICollection<Gastos> Gastos { get; set; }



    }
}
