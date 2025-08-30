using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using APICalculos.Application.DTOs;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;

namespace APICalculos.API.Controllers
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
            public async Task<List<ServiceTypeDTO>> GetProjectTo()
            {
                return await _context.ServiceTypes
                    .ProjectTo<ServiceTypeDTO>(_mapper.ConfigurationProvider).ToListAsync();

            }

        

            [HttpGet("buscartipoDeServicioId/{id:int}")]
            public async Task<ActionResult<ServiceType>> BuscarTipoDePagoId(int id)
            {
                var tipoDeServicioId = await _context.ServiceTypes
                .Include(s => s.ServiceCategorie)
                .FirstOrDefaultAsync(g => g.Id == id);

                if (tipoDeServicioId is null)
                {
                    var mensajeError = $"No se encontró ningún producto con el Id '{id}'.";
                    return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

                }

                var tipoDeServicioDTO = new ServiceTypeDTO
                {
                Id = tipoDeServicioId.Id,
                Name = tipoDeServicioId.Name,
                Price = tipoDeServicioId.Price,
                ServiceCategorieName = tipoDeServicioId.ServiceCategorie.Name,
                ServiceCategorieId = tipoDeServicioId.ServiceCategorieId,
                
                };

                return Ok(tipoDeServicioDTO);

            }

        [HttpGet("buscarPorNombre/{nombreServicio}")]
        public async Task<ActionResult<IEnumerable<ServiceTypeDTO>>> BuscarPorNombre(string nombreServicio)
        {
            var nombreSinEspacios = nombreServicio.Replace(" ", "").ToLower();

            var tiposDeServicio = await _context.ServiceTypes
                .Include(s => s.ServiceCategorie)
                .Where(g => g.Name.Replace(" ", "").ToLower().Contains(nombreSinEspacios))
                .Select(tipoDeServicio => new ServiceTypeDTO
                {
                    Id = tipoDeServicio.Id,
                    Name = tipoDeServicio.Name,
                    Price = tipoDeServicio.Price,
                    ServiceCategorieName = tipoDeServicio.ServiceCategorie.Name
                })
                .ToListAsync();

            if (!tiposDeServicio.Any())
            {
                var mensajeError = $"No se encontró ningún producto con el nombre '{nombreServicio}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }

            return Ok(tiposDeServicio);
        }


        [HttpPost]
            public async Task<ActionResult<ServiceType>> Post(ServiceTypeCreationDTO tipoDeServicioCreacionDTO)
            {
                var existeNombreServicio = await _context.ServiceTypes.AnyAsync(g => g.Name.Replace(" ", "").Trim() == tipoDeServicioCreacionDTO.Name.Replace(" ", "").Trim());

                if (string.IsNullOrWhiteSpace(tipoDeServicioCreacionDTO.Name))
                {
                    return BadRequest("El nombre no puede estar vacío");
                }
                if (existeNombreServicio)
                {
                    var texto = $"Este nombre  ya esta existente";
                    return BadRequest(texto);

                };

                var tipoDeServicio = _mapper.Map<ServiceType>(tipoDeServicioCreacionDTO);
                _context.Add(tipoDeServicio);
                await _context.SaveChangesAsync();
                var mensaje = "Se agrego correctamente :D";
                return Ok(mensaje);

            }

            [HttpPut("{id:int}")]
            public async Task<ActionResult> Put(ServiceTypeCreationDTO tipoDeServicioCreacionDTO, int id)
            {

                var existeNombreServicio = await _context.ServiceTypes.AnyAsync(g => g.Name.Replace(" ", "").Trim() == tipoDeServicioCreacionDTO.Name.Replace(" ", "").Trim());
                var tipoDeServicioDB = await _context.ServiceTypes.AsTracking().FirstOrDefaultAsync(a => a.Id == id);

            if (existeNombreServicio)
            {
                var texto = $"Este codigo ya esta existente";
                return BadRequest(texto);
            }

            if (tipoDeServicioDB != null)
            {
                if (!string.IsNullOrWhiteSpace(tipoDeServicioCreacionDTO.Name))
                {
                    tipoDeServicioDB.Name = tipoDeServicioCreacionDTO.Name;
                }

                if (tipoDeServicioCreacionDTO.ServiceCategorieId != 0)
                {
                    tipoDeServicioDB.ServiceCategorieId = tipoDeServicioCreacionDTO.ServiceCategorieId;
                }

                if (tipoDeServicioCreacionDTO.Price != 0 )
                {
                    tipoDeServicioDB.Price = tipoDeServicioCreacionDTO.Price;
                }

            }       
                await _context.SaveChangesAsync();
                var mensaje = "se modifico exitosamente";
                return Ok(mensaje);
            }

            [HttpDelete("{id:int}")]

            public async Task<ActionResult> Delete(int id)
            {
                //productoDB busca el primer valor Id con el ingresado

                var tipoDeServicioId = await _context.ServiceTypes.FirstOrDefaultAsync(g => g.Id == id);

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
