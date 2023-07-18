using APICalculos.Entidades;

namespace APICalculos.DTOs
{
    public class UsuarioDTO
    {
        public int UsuarioId { get; set; }
        public string NombreCompletoUsuario { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }

        public string TipoRol { get; set; } 
        public string NombreUsuario { get; set; } 
      
    }
}
