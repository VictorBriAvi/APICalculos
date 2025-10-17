using System.Text.Json.Serialization;

namespace APICalculos.Application.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityDocument { get; set; }
        [JsonIgnore]
        public DateTime DateBirth { get; set; }
        public int PaymentPercentage { get; set; }
        public string EmployeeDateBirth => DateBirth.ToString("dd-MM-yyyy");

     
    }
}
