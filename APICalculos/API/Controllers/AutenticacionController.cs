using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using APICalculos.Domain.Entidades;
using System.Text;
using AutoMapper;
using APICalculos.Application.DTOs;
using APICalculos.Infrastructure.Data;

namespace APICalculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secrectKey;
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;


        public AutenticacionController(IConfiguration config, MyDbContext context, IMapper mapper)
        {
            secrectKey = config.GetSection("settings").GetSection("secretkey").ToString();
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Validar")]

        public IActionResult Validar([FromBody] ValidarUsuarioDTO validarUsuarioDTO) 
        {

            var correoDB = _context.Users.FirstOrDefault(u => u.Email == validarUsuarioDTO.Correo);

            if (correoDB != null)
            {
                bool passwordValido = BCrypt.Net.BCrypt.Verify(validarUsuarioDTO.Password, correoDB.PasswordHash);

                if (passwordValido)
                {
                    var keyBytes = Encoding.ASCII.GetBytes(secrectKey);
                    var claims = new ClaimsIdentity();

                    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, validarUsuarioDTO.Correo));

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = claims,
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                    string tokenCreado = tokenHandler.WriteToken(tokenConfig);

                    return StatusCode(StatusCodes.Status200OK, new { token = tokenCreado });
                }
            }
            return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });




        }


    }
}
