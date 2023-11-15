namespace APICalculos.Entidades
{
    public class HistorialClientes
    {

        public int HistorialClientesId { get; set; }
        public string NombreDeHistorialCliente { get; set; }
        public string DescripcionHistorialCliente { get; set; }
        public DateTime FechaHistorial { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set;}

    }
}
