namespace APICalculos.Application.DTOs.Client
{
    public class ClientUpdateDTO
    {
        public string? Name { get; set; }
        public string? IdentityDocument { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateBirth { get; set; }
    }
}
