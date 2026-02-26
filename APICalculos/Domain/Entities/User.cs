using APICalculos.Domain.Entities;

namespace APICalculos.Domain.Entidades
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public ICollection<UserRol> UserRoles { get; set; }
    }

}
