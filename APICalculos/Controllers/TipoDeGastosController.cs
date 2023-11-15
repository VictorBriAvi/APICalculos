using APICalculos.DTOs;
using APICalculos.Entidades;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APICalculos.Controllers
{
    [Route("api/tipoDeGastos")]
    [ApiController]
    public class TipoDeGastosController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public TipoDeGastosController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        // ACA OBTENEMOS TODOS LOS GASTOS
        [HttpGet]
        public async Task<IEnumerable<TiposDeGastosDTO>> ObtenerTodosLosGastos()
        {
            return await _context.TiposDeGastos.ProjectTo<TiposDeGastosDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        // ACA OBTENEMOS LOS GASTOS SEGUN EL ID

        [HttpGet("buscarTipoDeGastoPorId/{id:int}")]
        public async Task<ActionResult<TiposDeGastos>> BuscarGastosPorId(int id)
        {
            var tipoDeGastoId = await _context.TiposDeGastos.FirstOrDefaultAsync(g => g.TipoDeGastosId == id);
            if (tipoDeGastoId is null)
            {
                var mensajeError = $"No se encontró ningún tipo de gasto con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }

            return tipoDeGastoId;
        }

        [HttpPost]
        public async Task<ActionResult> AgregarGastosConMapperDTO (TipoDeGastosCreacionDTO tipoDeGastosCreacionDTO)
        {
            var existeNombreTipoDeGasto = await _context.TiposDeGastos.AnyAsync(g => g.NombreTipoDeGastos.Replace("", "").Trim() == tipoDeGastosCreacionDTO.NombreTipoDeGastos.Replace(" ", "").Trim());

            if (string.IsNullOrWhiteSpace(tipoDeGastosCreacionDTO.NombreTipoDeGastos))  
            {
                return BadRequest("no puede estar vacío");
            };

            if (existeNombreTipoDeGasto)
            {
                var texto = $"Este codigo ya esta existente";
                return BadRequest(texto);
            };

            var tipoDeGasto = _mapper.Map<TiposDeGastos>(tipoDeGastosCreacionDTO);
            _context.Add(tipoDeGasto);
            await _context.SaveChangesAsync();
            var mensaje = "Se agrego correctamente el tipo de gasto :D";
            return Ok(mensaje);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> ModificarTiposDeGastos(TipoDeGastosCreacionDTO tipoDeGastosCreacionDTO, int id)
        {
            var existeNombreDeTipoDeGasto = await _context.TiposDeGastos.AnyAsync(g => g.NombreTipoDeGastos.Replace(" ", "").Trim() == tipoDeGastosCreacionDTO.NombreTipoDeGastos.Replace(" ", "").Trim());

            var tipoDeGastosDB = await _context.TiposDeGastos.AsTracking().FirstOrDefaultAsync(a => a.TipoDeGastosId == id);

            if (existeNombreDeTipoDeGasto)
            {
                var texto = $"Este tipo de nombre de gasto ya existe";
                return BadRequest(texto);
            }

            if (tipoDeGastosDB is null)
            {
                return NotFound();
                
            }

            if (!string.IsNullOrWhiteSpace(tipoDeGastosCreacionDTO.NombreTipoDeGastos))
            {
                tipoDeGastosDB.NombreTipoDeGastos = tipoDeGastosCreacionDTO.NombreTipoDeGastos;

            }
            await _context.SaveChangesAsync();
            var mensaje = "Se modifico el tipo de gasto exitosamente";
            return Ok(mensaje);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteTipoDeGasto(int id)
        {
            var tipoDeGastoId = await _context.TiposDeGastos.FirstOrDefaultAsync(g => g.TipoDeGastosId == id);

            if (tipoDeGastoId is null)
            {
                var mensajeNoSeEncuentra = "Este tipo de gasto no se encuentra";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeNoSeEncuentra);
            }

            _context.Remove(tipoDeGastoId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se ha eliminado un tipo de gasto";
            return Ok(mensaje);

        }

    }
}
