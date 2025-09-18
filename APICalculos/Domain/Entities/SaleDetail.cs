namespace APICalculos.Domain.Entidades
{
    public class SaleDetail
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ServiceTypeId { get; set; }
        public int EmployeeId { get; set; }
        public Sale Sale { get; set; }
        public ServiceType ServiceType { get; set; }
        public Employee Employee { get; set; }

    }
}
