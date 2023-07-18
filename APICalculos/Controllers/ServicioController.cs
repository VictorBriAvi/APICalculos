using APICalculos.DTOs;
using APICalculos.Entidades;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APICalculos.Controllers
{
    [ApiController]
    [Route("api/servicio")]
    public class ServicioController : ControllerBase
    {

        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public ServicioController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpGet]


        public async Task<IEnumerable<ServicioDTO>> Get()
        {
            return await _context.Servicios
                .ProjectTo<ServicioDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpGet("buscarServicioId/{id:int}")]
        public async Task<ActionResult<Servicio>> BuscarServicioId(int id)
        {
            var servicioId = await _context.Servicios
                .Include(s => s.Empleado)
                .Include(s => s.Cliente)
                .Include(s => s.TipoDePago)
                .Include(s => s.TipoDeServicio)
                
                .FirstOrDefaultAsync(g => g.ServicioId == id);

            if (servicioId is null)
            {
                var mensajeError = $"No se encontró ningún producto con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

            }

            var servicioDTO = new ServicioDTO
            {
                ServicioId = servicioId.ServicioId,
                NombreCompletoEmpleado = servicioId.Empleado.NombreCompletoEmpleado,
                NombreCompletoCliente = servicioId.Cliente.NombreCompletoCliente,
                NombreTipoDePago = servicioId.TipoDePago.NombreTipoDePago,
                NombreServicio = servicioId.TipoDeServicio.NombreServicio,
                ValorServicio = servicioId.ValorServicio

            };

            return Ok(servicioDTO);

        }

        [HttpPost]
        public async Task<ActionResult<Servicio>> Post(ServicioCreacionDTO servicioCreacionDTO)
        {
            //var clienteExiste = await _context.Clientes.FindAsync(servicioCreacionDTO.ClienteId);
            var empleadoExiste = await _context.Empleados.FindAsync(servicioCreacionDTO.EmpleadoId);
            var tipoDeServicioExiste = await _context.TipoDeServicios.FindAsync(servicioCreacionDTO.TipoDeServicioId);
            var tipoDePago = await _context.TipoDePagos.FindAsync(servicioCreacionDTO.TipoDePagoId);

            if (tipoDePago == null)
            {
                return BadRequest("El tipo de pago no existe");
            }
            if (empleadoExiste == null)
            {
                return BadRequest("El empleado no existe");
            }
            if (tipoDeServicioExiste == null)
            {
                return BadRequest("el tipo de servicio no existe");
            }
            var servicio = _mapper.Map<Servicio>(servicioCreacionDTO);
            _context.Add(servicio);
            await _context.SaveChangesAsync();
            var mensaje = "Se agrego correctamente :D";
            return Ok(mensaje);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ServicioCreacionDTO servicioCreacionDTO, int id)
        {


            var servicioDB = await _context.Servicios.AsTracking().FirstOrDefaultAsync(a => a.ServicioId == id);

            if (servicioDB != null)
            {


                servicioDB.TipoDeServicioId = servicioCreacionDTO.TipoDeServicioId;
                servicioDB.ClienteId = servicioCreacionDTO.ClienteId;
                servicioDB.EmpleadoId = servicioCreacionDTO.EmpleadoId;
                servicioDB.TipoDePagoId = servicioCreacionDTO.TipoDePagoId;


                await _context.SaveChangesAsync();
                var mensaje = "Se modificó correctamente.";
                return Ok(mensaje);
            }
            else
            {
                return NotFound(); // El registro no fue encontrado
            }



        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            //productoDB busca el primer valor Id con el ingresado

            var servicioId = await _context.Servicios.FirstOrDefaultAsync(g => g.ServicioId == id);



            _context.Remove(servicioId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se ha Eliminado el Producto";
            return Ok(mensaje);
        }
    }
}
