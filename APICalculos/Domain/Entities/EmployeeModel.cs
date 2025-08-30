namespace APICalculos.Domain.Entidades
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityDocument { get; set; }
        public DateTime DateBirth { get; set; }

        public ICollection<DetalleVenta> DetalleVentas { get; set; }

    }
}
