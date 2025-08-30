using System.Text.Json.Serialization;

namespace APICalculos.Application.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentityDocument { get; set; }
        [JsonIgnore]
        public DateTime DateBirth { get; set; }
        public string ParsedDateOfBirth => DateBirth.ToString("dd-MM-yyyy");

    }
}
