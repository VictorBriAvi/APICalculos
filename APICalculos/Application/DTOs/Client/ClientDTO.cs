using System.Text.Json.Serialization;

namespace APICalculos.Application.DTOs.Client
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? IdentityDocument { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        [JsonIgnore]
        public DateTime? DateBirth { get; set; }

        public string? ParsedDateOfBirth =>
            DateBirth.HasValue
                ? DateBirth.Value.ToString("dd-MM-yyyy")
                : null;
    }

}
