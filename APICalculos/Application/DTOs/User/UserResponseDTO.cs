namespace APICalculos.Application.DTOs.User
{
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public int StoreId { get; set; }
    }
}
