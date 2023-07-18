using APICalculos.Entidades;

namespace APICalculos.DTOs
{
    public class UsuarioCreacionDTO
    {

        public string NombreCompletoUsuario { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
    }
}
