namespace APICalculos.Domain.Entidades
{
    public class UserRol
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; }
    }

}
