namespace APICalculos.Application.DTOs
{
    public class SaleDetailCreationDTO
    {
        public int SaleId { get; set; }
        public int ServiceTypeId { get; set; }
        public int EmployeeId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DiscountPercent {  get; set; }
        public decimal AdditionalCharge { get; set; }

    }
}
