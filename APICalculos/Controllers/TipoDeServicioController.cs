using APICalculos.DTOs;
using APICalculos.Entidades;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APICalculos.Controllers
{
    [ApiController]
    [Route("api/tipodeservicio")]
    public class TipoDeServicioController : ControllerBase
    {



            private readonly MyDbContext _context;
            private readonly IMapper _mapper;

            public TipoDeServicioController(MyDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;

            }

            [HttpGet]
            public async Task<List<TipoDeServicioDTO>> GetProjectTo()
            {
                return await _context.TipoDeServicios
                    .ProjectTo<TipoDeServicioDTO>(_mapper.ConfigurationProvider).ToListAsync();

            }

            [HttpGet("buscartipoDeServicioId/{id:int}")]
            public async Task<ActionResult<TipoDeServicio>> BuscarTipoDePagoId(int id)
            {
                var tipoDeServicioId = await _context.TipoDeServicios.FirstOrDefaultAsync(g => g.TipoDeServicioId == id);

                if (tipoDeServicioId is null)
                {
                    var mensajeError = $"No se encontró ningún producto con el Id '{id}'.";
                    return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

                }

                var tipoDeServicioDTO = new TipoDeServicioDTO
                {
                    TipoDeServicioId = tipoDeServicioId.TipoDeServicioId,
                    NombreServicio = tipoDeServicioId.NombreServicio,

                };

                return Ok(tipoDeServicioDTO);

            }

            [HttpPost]
            public async Task<ActionResult<TipoDeServicio>> Post(TipoDeServicioCreacionDTO tipoDeServicioCreacionDTO)
            {
                var existeNombreServicio = await _context.TipoDeServicios.AnyAsync(g => g.NombreServicio.Replace(" ", "").Trim() == tipoDeServicioCreacionDTO.NombreServicio.Replace(" ", "").Trim());

                if (string.IsNullOrWhiteSpace(tipoDeServicioCreacionDTO.NombreServicio))
                {
                    return BadRequest("El nombre no puede estar vacío");
                }
                if (existeNombreServicio)
                {
                    var texto = $"Este nombre  ya esta existente";
                    return BadRequest(texto);

                };

                var tipoDeServicio = _mapper.Map<TipoDeServicio>(tipoDeServicioCreacionDTO);
                _context.Add(tipoDeServicio);
                await _context.SaveChangesAsync();
                var mensaje = "Se agrego correctamente :D";
                return Ok(mensaje);

            }

            [HttpPut("{id:int}")]
            public async Task<ActionResult> Put(TipoDeServicioCreacionDTO tipoDeServicioCreacionDTO, int id)
            {

                var existeNombreServicio = await _context.TipoDeServicios.AnyAsync(g => g.NombreServicio.Replace(" ", "").Trim() == tipoDeServicioCreacionDTO.NombreServicio.Replace(" ", "").Trim());


                var tipoDeServicioDB = await _context.TipoDeServicios.AsTracking().FirstOrDefaultAsync(a => a.TipoDeServicioId == id);




            if (existeNombreServicio)
            {
                var texto = $"Este codigo ya esta existente";
                return BadRequest(texto);
            }

            if (!string.IsNullOrWhiteSpace(tipoDeServicioCreacionDTO.NombreServicio))
                {
                tipoDeServicioDB.NombreServicio = tipoDeServicioCreacionDTO.NombreServicio;
                }



                if (tipoDeServicioDB is null)
                {
                    return NotFound();

                }
                if (true)
                {

                }

       
                await _context.SaveChangesAsync();
                var mensaje = "se modifico exitosamente";
                return Ok(mensaje);
            }

            [HttpDelete("{id:int}")]

            public async Task<ActionResult> Delete(int id)
            {
                //productoDB busca el primer valor Id con el ingresado

                var tipoDeServicioId = await _context.TipoDeServicios.FirstOrDefaultAsync(g => g.TipoDeServicioId == id);

                if (tipoDeServicioId is null)
                {
                    var textoNoEncontrado = "Este producto no existe";
                    return StatusCode((int)HttpStatusCode.NotFound, textoNoEncontrado);

                }

                _context.Remove(tipoDeServicioId);
                await _context.SaveChangesAsync();
                var mensaje = $"Se ha Eliminado el Producto";
                return Ok(mensaje);
            }
        }
}
