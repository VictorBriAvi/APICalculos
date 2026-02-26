using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using APICalculos.Application.DTOs.Rol;

namespace APICalculos.API.Controllers
{
    [Route("api/rol")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public RolController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<List<RolDTO>> GetProjectTo()
        {
            return await _context.Roles
                .ProjectTo<RolDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpGet("buscarRolPorId/{id:int}")]
        public async Task<ActionResult<PaymentTypes>> BuscarRolId(int id)
        {
            var rolId = await _context.Roles.FirstOrDefaultAsync(g => g.Id == id);

            if (rolId is null)
            {
                var mensajeError = $"No se encontró ningún producto con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

            }

            var rolDTO = new RolDTO
            {
                RolId = rolId.Id,
                NombreRol = rolId.Name,

            };

            return Ok(rolDTO);

        }
        [HttpPost]
        public async Task<ActionResult<Rol>> Post(RolCreacionDTO rolCreacionDTO)
        {
            var existeNombreRol = await _context.Roles.AnyAsync(g => g.Name.Replace(" ", "").Trim() == rolCreacionDTO.NombreRol.Replace(" ", "").Trim());

            if (string.IsNullOrWhiteSpace(rolCreacionDTO.NombreRol))
            {
                return BadRequest("El nombre no puede estar vacío");
            }
            if (existeNombreRol)
            {
                var texto = $"Este nombre  ya esta existente";
                return BadRequest(texto);

            };

            var rol = _mapper.Map<Rol>(rolCreacionDTO);
            _context.Add(rol);
            await _context.SaveChangesAsync();
            var mensaje = "Se agrego correctamente :D";
            return Ok(mensaje);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(RolCreacionDTO rolCreacionDTO, int id)
        {

            var existeNombreRol = await _context.Roles.AnyAsync(g => g.Name.Replace(" ", "").Trim() == rolCreacionDTO.NombreRol.Replace(" ", "").Trim());
         
            //productoDB busca el primer valor Id con el ingresado
            var rolDB = await _context.Roles.AsTracking().FirstOrDefaultAsync(a => a.Id == id);


            if (!string.IsNullOrWhiteSpace(rolCreacionDTO.NombreRol))
            {
                rolDB.Name = rolCreacionDTO.NombreRol;
            }

            if (existeNombreRol)
            {
                var texto = $"Este nombre de usuario  ya esta existente";
                return BadRequest(texto);

            };

            if (rolDB is null)
            {
                return NotFound();

            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            //productoDB busca el primer valor Id con el ingresado

            var rolId = await _context.Roles.FirstOrDefaultAsync(g => g.Id == id);



            _context.Remove(rolId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se ha Eliminado el Producto";
            return Ok(mensaje);
        }
    }
}
   