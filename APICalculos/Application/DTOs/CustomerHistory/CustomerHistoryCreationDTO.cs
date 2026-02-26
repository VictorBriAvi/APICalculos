using APICalculos.Domain.Entidades;


namespace APICalculos.Application.DTOs.CustomerHistory
{
    public class CustomerHistoryCreationDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int ClientId { get; set; }

    }
}
