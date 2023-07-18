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
    [Route("api/tipodepago")]
    public class TipoDePagoController : ControllerBase
    {

        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public TipoDePagoController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<List<TipoDePagoDTO>> GetProjectTo()
        {
            return await _context.TipoDePagos
                .ProjectTo<TipoDePagoDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpGet("buscartipoDePagoId/{id:int}")]
        public async Task<ActionResult<TipoDePago>> BuscarTipoDePagoId(int id)
        {
            var tipoDePagoId = await _context.TipoDePagos.FirstOrDefaultAsync(g => g.TipoDePagoId == id);

            if (tipoDePagoId is null)
            {
                var mensajeError = $"No se encontró ningún producto con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

            }

            var tipoDePagoDTO = new TipoDePagoDTO
            {
                TipoDePagoId = tipoDePagoId.TipoDePagoId,
                NombreTipoDePago = tipoDePagoId.NombreTipoDePago,

            };

            return Ok(tipoDePagoDTO);

        }

        [HttpPost]
        public async Task<ActionResult<TipoDePago>> Post(TipoDePagoCreacionDTO tipoDePagoCreacionDTO)
        {
            var existeNombreDeTipoDePago = await _context.TipoDePagos.AnyAsync(g => g.NombreTipoDePago.Replace(" ", "").Trim() == tipoDePagoCreacionDTO.NombreTipoDePago.Replace(" ", "").Trim());

            if (string.IsNullOrWhiteSpace(tipoDePagoCreacionDTO.NombreTipoDePago))
            {
                return BadRequest("El nombre no puede estar vacío");
            }
            if (existeNombreDeTipoDePago)
            {
                var texto = $"Este nombre  ya esta existente";
                return BadRequest(texto);

            };

            var tipoDePago = _mapper.Map<TipoDePago>(tipoDePagoCreacionDTO);
            _context.Add(tipoDePago);
            await _context.SaveChangesAsync();
            var mensaje = "Se agrego correctamente :D";
            return Ok(mensaje);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(TipoDePagoCreacionDTO tipoDePagoCreacionDTO, int id)
        {

            var ExisteNombreDeTipoDePago = await _context.TipoDePagos.AnyAsync(g => g.NombreTipoDePago.Replace(" ", "").Trim() == tipoDePagoCreacionDTO.NombreTipoDePago.Replace(" ", "").Trim());


            var tipoDePagoDB = await _context.Empleados.AsTracking().FirstOrDefaultAsync(a => a.EmpleadoId == id);

            if (string.IsNullOrWhiteSpace(tipoDePagoCreacionDTO.NombreTipoDePago))
            {
                return BadRequest("El nombre no puede estar vacío");
            }
            if (ExisteNombreDeTipoDePago)
            {
                var texto = $"Este codigo ya esta existente";
                return BadRequest(texto);
            }

            if (tipoDePagoDB is null)
            {
                return NotFound();

            }
            if (true)
            {

            }

            tipoDePagoDB = _mapper.Map(tipoDePagoCreacionDTO, tipoDePagoDB);
            await _context.SaveChangesAsync();
            var mensaje = "se modifico exitosamente";
            return Ok(mensaje);
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            //productoDB busca el primer valor Id con el ingresado

            var tipoDePagoId = await _context.TipoDePagos.FirstOrDefaultAsync(g => g.TipoDePagoId == id);

            if (tipoDePagoId is null)
            {
                var textoNoEncontrado = "Este producto no existe";
                return StatusCode((int)HttpStatusCode.NotFound, textoNoEncontrado);

            }

            _context.Remove(tipoDePagoId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se ha Eliminado el Producto";
            return Ok(mensaje);
        }

    }
}
