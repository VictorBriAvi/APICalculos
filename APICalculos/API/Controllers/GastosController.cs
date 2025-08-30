using APICalculos.Application.DTOs;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;

namespace APICalculos.API.Controllers
{
    [Route("api/Gasto")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;

        public GastosController(MyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<GastosDTO>> GetAllGastos ()
        {
            return await _dbContext.Gastos
                .ProjectTo<GastosDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("buscarGastoId/{id:int}")]
        public async Task<ActionResult<Gastos>> GetGastosForId (int id)
        {
            var gastoId = await _dbContext.Gastos
                .Include(s => s.TiposDeGastos).FirstOrDefaultAsync(g => g.GastosId == id);

            if(gastoId is null)
            {
                var mensajeError = $"No se encontro ningun gasto con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }
            var gastosDTO = new GastosDTO
            {
                GastosId = gastoId.GastosId,
                DescripcionGastos = gastoId.DescripcionGastos,
                NombreTipoDeGastos = gastoId.TiposDeGastos.Name,
                FechaGastos = gastoId.FechaGastos,
                PrecioGasto = gastoId.PrecioGasto,
                TipoDeGastosId = gastoId.TipoDeGastosId,
            };

            return Ok(gastosDTO);
        }
        [HttpGet("buscarGastosPorFecha/{fecha}")]
        public async Task<ActionResult<IEnumerable<GastosDTO>>> GetGastosPorFecha( DateTime fecha)
        {
            var gastosPorFecha = await _dbContext.Gastos
            .Include(s => s.TiposDeGastos)
            .Where(g => g.FechaGastos.Date == fecha.Date)
            .ProjectTo<GastosDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();

            if (gastosPorFecha.Count == 0)
            {
                var mensajeError = $"No se encontraron gastos para la fecha '{fecha:yyyy-MM-dd}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }



            return Ok(gastosPorFecha);
        }

        [HttpGet("buscarGastosPorRangoFecha/{fechaInicio}/{fechaFin}")]
        public async Task<ActionResult<IEnumerable<GastosDTO>>> GetGastosPorRangoFecha( DateTime fechaInicio,  DateTime fechaFin)
        {
            var gastosPorRangoFecha = await _dbContext.Gastos
                .Include(s => s.TiposDeGastos)
                .Where(g => g.FechaGastos.Date >= fechaInicio.Date && g.FechaGastos.Date <= fechaFin.Date)
                .ProjectTo<GastosDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            if (gastosPorRangoFecha.Count == 0)
            {
                var mensajeError = $"No se encontraron gastos para el rango de fechas entre '{fechaInicio:yyyy-MM-dd}' y '{fechaFin:yyyy-MM-dd}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }

            return Ok(gastosPorRangoFecha);
        }

        [HttpPost]
        public async Task<ActionResult<Gastos>> PostGasto(GastosCreacionDTO gastosCreacionDTO)
        {
            var tipoDeGastoExiste = await _dbContext.ExpenseTypes.FindAsync(gastosCreacionDTO.TipoDeGastosId);

            if (tipoDeGastoExiste == null)
            {
                return BadRequest("El tipo de gasto no existe");
            }


            var gasto = _mapper.Map<Gastos>(gastosCreacionDTO);
            gasto.FechaGastos = DateTime.Now;

            _dbContext.Add(gasto);
            await _dbContext.SaveChangesAsync();
            var mensaje = "Se agrego correctamente un nuevo gasto :D";
            return Ok(mensaje);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> ModificarGasto(GastosCreacionDTO gastosCreacionDTO, int id)
        {
            var gastoDB = await _dbContext.Gastos.AsTracking().FirstOrDefaultAsync(a => a.GastosId == id);
            if (gastoDB != null)
            {
                // Comprueba si el valor de DescripcionGastos es diferente de null o vacío antes de asignarlo.
                if (!string.IsNullOrWhiteSpace(gastosCreacionDTO.DescripcionGastos))
                {
                    gastoDB.DescripcionGastos = gastosCreacionDTO.DescripcionGastos;
                }

                // Comprueba si el valor de TipoDeGastosId es diferente de 0 antes de asignarlo.
                if (gastosCreacionDTO.TipoDeGastosId != 0)
                {
                    gastoDB.TipoDeGastosId = gastosCreacionDTO.TipoDeGastosId;
                }
                // Comprueba si el valor de PrecioGasto es diferente de 0 antes de asignarlo.
                if (gastosCreacionDTO.PrecioGasto != 0)
                {
                    gastoDB.PrecioGasto = gastosCreacionDTO.PrecioGasto;
                }


                gastoDB.FechaGastos = gastoDB.FechaGastos;

                await _dbContext.SaveChangesAsync();
                var mensaje = "Se modifico el gasto correctamente";
                return Ok(mensaje);

            }
            else
            {
                return NotFound();
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteGasto(int id)
        {
            var gastoId = await _dbContext.Gastos.FirstOrDefaultAsync(g => g.GastosId == id);

            _dbContext.Remove(gastoId);
            await _dbContext.SaveChangesAsync();
            var mensaje = $"Se ha eliminado un gasto";
            return Ok(mensaje);
        }
    }
}
