//using AutoMapper.QueryableExtensions;
//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Net;
//using BCrypt.Net;
//using APICalculos.Application.DTOs;
//using APICalculos.Domain.Entidades;
//using APICalculos.Infrastructure.Data;

//namespace APICalculos.API.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsuarioController : ControllerBase
//    {


//        private readonly MyDbContext _context;
//        private readonly IMapper _mapper;

//        public UsuarioController(MyDbContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;

//        }

//        [HttpGet]
//        public async Task<List<UsuarioDTO>> Get()
//        {
//            return await _context.Users
//                .Include(u => u.UsuarioRoles)
//                    .ThenInclude(ur => ur.Rol)
//                .ProjectTo<UsuarioDTO>(_mapper.ConfigurationProvider).ToListAsync();

//        }

//        [HttpGet("buscarUsuarioPorId/{id:int}")]
//        public async Task<ActionResult<User>> BuscarUsuarioId(int id)
//        {
//            var usuarioId = await _context.Users
//                .Include(u => u.UsuarioRoles)
//                    .ThenInclude(ur => ur.Rol)
//                .FirstOrDefaultAsync(g => g.Id == id);

//            if (usuarioId is null)
//            {
//                var mensajeError = $"No se encontró ningún producto con el Id '{id}'.";
//                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

//            }

//            var usuarioDTO = new 
//            {
//                usuarioId = usuarioId.Id,
//                nombreCompletoUsuario = usuarioId.Name,
//                nombreDeUsuario = usuarioId.NameUser,
//                TipoRol = usuarioId.UsuarioRoles.FirstOrDefault()?.Rol?.NombreRol,
//                NombreUsuario = usuarioId.UsuarioRoles.FirstOrDefault()?.User?.Name,

//            };

//            return Ok(usuarioDTO);

//        }

//        [HttpPost]
//        public async Task<ActionResult<User>> Post(UsuarioCreacionDTO usuarioCreacionDTO)
//        {
//            var existeNombreCompletoUsuario = await _context.Users.AnyAsync(g => g.Name.Replace(" ", "").Trim() == usuarioCreacionDTO.NombreCompletoUsuario.Replace(" ", "").Trim());
//            var existeCorreo = await _context.Users.AnyAsync(g => g.Email.Replace(" ", "").Trim() == usuarioCreacionDTO.Correo.Replace(" ", "").Trim());
//            var existeNombreEmpleado = await _context.Users.AnyAsync(g => g.NameUser.Replace(" ", "").Trim() == usuarioCreacionDTO.NombreDeUsuario.Replace(" ", "").Trim());

//            if (string.IsNullOrWhiteSpace(usuarioCreacionDTO.NombreCompletoUsuario) ||  
//                string.IsNullOrWhiteSpace(usuarioCreacionDTO.Correo)|| 
//                string.IsNullOrWhiteSpace(usuarioCreacionDTO.NombreDeUsuario) ||
//                string.IsNullOrWhiteSpace(usuarioCreacionDTO.Password))
//            {
//                return BadRequest("No puedes dejar nada vacio todo es obligatorio");
//            }
//            if (existeNombreCompletoUsuario)
//            {
//                var texto = $"Este nombre  ya esta existente";
//                return BadRequest(texto);

//            };
//            if (existeCorreo)
//            {
//                var texto = $"Este nombre  ya esta existente";
//                return BadRequest(texto);

//            };
//            if (existeNombreEmpleado)
//            {
//                var texto = $"Este nombre  ya esta existente";
//                return BadRequest(texto);

//            };

//            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(usuarioCreacionDTO.Password);


//            var usuario = _mapper.Map<User>(usuarioCreacionDTO);
//            usuario.Password = hashedPassword;
//            _context.Add(usuario);
//            await _context.SaveChangesAsync();
//            var mensaje = "Se agrego correctamente :D";
//            return Ok(mensaje);

//        }

//        [HttpPut("{id:int}")]
//        public async Task<ActionResult> Put(UsuarioCreacionDTO usuarioCreacionDTO, int id)
//        {

//            var existeNombreCompletoUsuario = await _context.Users.AnyAsync(g => g.NameUser.Replace(" ", "").Trim() == usuarioCreacionDTO.NombreCompletoUsuario.Replace(" ", "").Trim());
//            var existeCorreo = await _context.Users.AnyAsync(g => g.Email.Replace(" ", "").Trim() == usuarioCreacionDTO.Correo.Replace(" ", "").Trim());
//            var existeNombreDeUsuario = await _context.Users.AnyAsync(g => g.NameUser.Replace(" ", "").Trim() == usuarioCreacionDTO.NombreDeUsuario.Replace(" ", "").Trim());

//            //productoDB busca el primer valor Id con el ingresado
//            var usuarioDB = await _context.Users.AsTracking().FirstOrDefaultAsync(a => a.Id== id);


//            if (!string.IsNullOrWhiteSpace(usuarioCreacionDTO.NombreCompletoUsuario))
//            {
//                usuarioDB.Name = usuarioCreacionDTO.NombreCompletoUsuario;
//            }

//            if (!string.IsNullOrWhiteSpace(usuarioCreacionDTO.NombreDeUsuario))
//            {
//                usuarioDB.NameUser = usuarioCreacionDTO.NombreDeUsuario;
//            }

//            if (!string.IsNullOrWhiteSpace(usuarioCreacionDTO.Correo))
//            {
//                usuarioDB.Email = usuarioCreacionDTO.Correo;
//            }

//            if (!string.IsNullOrWhiteSpace(usuarioCreacionDTO.Password))
//            {
//                usuarioDB.Password = usuarioCreacionDTO.Password;
//            }
//            if (existeNombreCompletoUsuario)
//            {
//                var texto = $"Este nombre  ya esta existente";
//                return BadRequest(texto);

//            };
//            if (existeCorreo)
//            {
//                var texto = $"Este correo  ya esta existente";
//                return BadRequest(texto);

//            };
//            if (existeNombreDeUsuario)
//            {
//                var texto = $"Este nombre de usuario  ya esta existente";
//                return BadRequest(texto);

//            };

//            if (usuarioDB is null)
//            {
//                return NotFound();

//            }
            
//            await _context.SaveChangesAsync();
//            return Ok();
//        }


//        [HttpDelete("{id:int}")]

//        public async Task<ActionResult> Delete(int id)
//        {
//            //productoDB busca el primer valor Id con el ingresado

//            var usuarioId = await _context.Users.FirstOrDefaultAsync(g => g.Id   == id);

//            if (usuarioId is null)
//            {
//                var textoNoEncontrado = "Este producto no existe";
//                return StatusCode((int)HttpStatusCode.NotFound, textoNoEncontrado);

//            }

//            _context.Remove(usuarioId);
//            await _context.SaveChangesAsync();
//            var mensaje = $"Se ha Eliminado el Producto";
//            return Ok(mensaje);
//        }

//    }
//}

