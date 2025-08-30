namespace APICalculos.Domain.Entidades
{
    public class Rol
    {
        public int RolId { get; set; }
        public string NombreRol { get; set; }

        public ICollection<UsuarioRol> RolesUsuario { get; set;}
    }
}
