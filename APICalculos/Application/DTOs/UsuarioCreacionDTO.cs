using APICalculos.Domain.Entidades;

namespace APICalculos.Application.DTOs
{
    public class UsuarioCreacionDTO
    {

        public string NombreCompletoUsuario { get; set; }
        public string NombreDeUsuario { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
    }
}
