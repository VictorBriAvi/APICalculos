namespace APICalculos.Application.DTOs.User
{
    public class UserCreateDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int StoreId { get; set; }
    }
}
