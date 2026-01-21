namespace APICalculos.Application.DTOs.Employee
{
    public class EmployeeCreationDTO
    {
        public string Name { get; set; }
        public string IdentityDocument { get; set; }
        public int PaymentPercentage { get; set; }
        public DateTime DateBirth { get; set; }

    }
}
