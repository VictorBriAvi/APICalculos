using APICalculos.Application.DTOs;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APICalculos.API.Controllers
{
    [Route("api/UsuarioRoles")]
    [ApiController]
    public class UsuarioRolController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public UsuarioRolController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        [HttpPost]
        public async Task<ActionResult<UserRol>> Post(UsuarioRolCreacionDTO usuarioRolCreacionDTO)
        {

            if (usuarioRolCreacionDTO.UsuarioId <= 0)
            {
                var mensajeError = "El ID del Usuario debe ser un valor válido";
                return BadRequest(mensajeError);
            }

            if (usuarioRolCreacionDTO.RolId <= 0)
            {
                var mensajeError = "El ID del rol debe ser un valor válido";
                return BadRequest(mensajeError);
            }

            var usuarioRol = _mapper.Map<UserRol>(usuarioRolCreacionDTO);
            _context.Add(usuarioRol);
            await _context.SaveChangesAsync();
            var mensaje = "Se agrego correctamente :D";
            return Ok(mensaje);

        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(UsuarioRolCreacionDTO usuarioRolCreacionDTO,int id)
        {

            var usuarioRolDb = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == id);

            if (usuarioRolDb == null)
            {
                return NotFound();
            }


            if (usuarioRolCreacionDTO.RolId != 0)
            {
                usuarioRolDb.RolId = usuarioRolCreacionDTO.RolId;
            }

            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            //productoDB busca el primer valor Id con el ingresado

            var usuarioId = await _context.Users.FirstOrDefaultAsync(g => g.Id == id);

            if (usuarioId is null)
            {
                var textoNoEncontrado = "Este producto no existe";
                return StatusCode((int)HttpStatusCode.NotFound, textoNoEncontrado);

            }

            _context.Remove(usuarioId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se ha Eliminado el Producto";
            return Ok(mensaje);
        }
    }
}
