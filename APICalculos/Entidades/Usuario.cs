namespace APICalculos.Entidades
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string NombreCompletoUsuario { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
        public ICollection<UsuarioRol> UsuarioRoles { get; set; }
    }
}
