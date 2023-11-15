namespace APICalculos.Entidades
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string NombreCompletoCliente { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime FechaNacimiento { get; set; }

        // Establezco una clave foranea para la relacion con historial clientes
        public ICollection<HistorialClientes> HistorialClientes { get; set; }

    }
}
