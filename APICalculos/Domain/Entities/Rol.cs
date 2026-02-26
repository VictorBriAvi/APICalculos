namespace APICalculos.Domain.Entidades
{
    public class Rol
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRol> UserRoles { get; set; }
    }

}
