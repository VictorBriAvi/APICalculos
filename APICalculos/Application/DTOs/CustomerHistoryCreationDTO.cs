using APICalculos.Domain.Entidades;


namespace APICalculos.Application.DTOs
{
    public class CustomerHistoryCreationDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ParseDateHistory { get; set; }
        public int ClientId { get; set; }

    }
}
