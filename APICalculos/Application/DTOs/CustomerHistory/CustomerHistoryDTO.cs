using System.Text.Json.Serialization;

namespace APICalculos.Application.DTOs.CustomerHistory
{
    public class CustomerHistoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public DateTime DateHistory { get; set; }
        public string ParsedDateHistory => DateHistory.ToString("dd-MM-yyyy");
        public string ClientName { get; set; }
        public int ClientId { get; set; }
    }
}
