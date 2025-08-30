using APICalculos.Application.DTOs;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APICalculos.API.Controllers
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
                NombreCompletoEmpleado = servicioId.Empleado.Name,
                NombreCompletoCliente = servicioId.Cliente.Name,
                NombreTipoDePago = servicioId.TipoDePago.Name,
                NombreServicio = servicioId.TipoDeServicio.Name,
                ValorServicio = servicioId.ValorServicio

            };

            return Ok(servicioDTO);

        }

        [HttpGet("buscarServicioPorFecha/{fecha}")]
        public async Task<ActionResult<IEnumerable<ServicioDTO>>>GetServicioByFecha(DateTime fecha)
        {
            var serviciosPorFecha = await _context.Servicios
                .Include(s => s.Empleado)
                .Include(s => s.Cliente)
                .Include(s => s.TipoDePago)
                .Include(s => s.TipoDeServicio)
                .Where(s => s.FechaIngresoServicio.Date == fecha.Date) // Filtra por la fecha recibida
                .ToListAsync();

            if (serviciosPorFecha.Count == 0)
            {
                var mensajeError = $"No se encontraron servicios para la fecha '{fecha:yyyy-MM-dd}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }

            var serviciosDTOs = serviciosPorFecha.Select(s => new ServicioDTO
            {
                ServicioId = s.ServicioId,
                NombreCompletoEmpleado = s.Empleado.Name,
                NombreCompletoCliente = s.Cliente.Name,
                NombreTipoDePago = s.TipoDePago.Name,
                NombreServicio = s.TipoDeServicio.Name,
                ValorServicio = s.ValorServicio,
                FechaIngresoServicio = s.FechaIngresoServicio
                // Agrega más propiedades según tus necesidades
            });

            return Ok(serviciosDTOs);
        }

        [HttpGet("buscarServicioPorRangoFecha/{fechaInicio}/{fechaFin}")]
        public async Task<ActionResult<IEnumerable<ServicioDTO>>> GetServicioByRangoFecha( DateTime fechaInicio,  DateTime fechaFin)
        {
            var serviciosPorRangoFecha = await _context.Servicios
                .Include(s => s.Empleado)
                .Include(s => s.Cliente)
                .Include(s => s.TipoDePago)
                .Include(s => s.TipoDeServicio)
                .Where(s => s.FechaIngresoServicio.Date >= fechaInicio.Date && s.FechaIngresoServicio.Date <= fechaFin.Date) // Filtra por el rango de fechas recibido
                .ToListAsync();

            if (serviciosPorRangoFecha.Count == 0)
            {
                var mensajeError = $"No se encontraron servicios para el rango de fechas entre '{fechaInicio:yyyy-MM-dd}' y '{fechaFin:yyyy-MM-dd}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }

            var serviciosDTOs = serviciosPorRangoFecha.Select(s => new ServicioDTO
            {
                ServicioId = s.ServicioId,
                NombreCompletoEmpleado = s.Empleado.Name,
                NombreCompletoCliente = s.Cliente.Name,
                NombreTipoDePago = s.TipoDePago.Name,
                NombreServicio = s.TipoDeServicio.Name,
                ValorServicio = s.ValorServicio,
                FechaIngresoServicio = s.FechaIngresoServicio
                // Agrega más propiedades según tus necesidades
            });

            return Ok(serviciosDTOs);
        }

        [HttpPost]
        public async Task<ActionResult<Servicio>> Post(ServicioCreacionDTO servicioCreacionDTO)
        {
            //var clienteExiste = await _context.Clientes.FindAsync(servicioCreacionDTO.ClienteId);
            var empleadoExiste = await _context.Employees.FindAsync(servicioCreacionDTO.EmpleadoId);
            var tipoDeServicioExiste = await _context.ServiceTypes.FindAsync(servicioCreacionDTO.TipoDeServicioId);
            var tipoDePagoExiste = await _context.PaymentTypes.FindAsync(servicioCreacionDTO.TipoDePagoId);

            if (tipoDePagoExiste == null)
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

                // Comprueba si el valor de TipoDeServicioId es diferente de 0 antes de asignarlo.
                if (servicioCreacionDTO.TipoDeServicioId != 0)
                {
                    servicioDB.TipoDeServicioId = servicioCreacionDTO.TipoDeServicioId;
                }

                // Comprueba si el valor de ClienteId es diferente de 0 antes de asignarlo.
                if (servicioCreacionDTO.ClienteId != 0)
                {
                    servicioDB.ClienteId = servicioCreacionDTO.ClienteId;
                }

                // Comprueba si el valor de EmpleadoId es diferente de 0 antes de asignarlo.
                if (servicioCreacionDTO.EmpleadoId != 0)
                {
                    servicioDB.EmpleadoId = servicioCreacionDTO.EmpleadoId;
                }

                // Comprueba si el valor de TipoDePagoId es diferente de 0 antes de asignarlo.
                if (servicioCreacionDTO.TipoDePagoId != 0)
                {
                    servicioDB.TipoDePagoId = servicioCreacionDTO.TipoDePagoId;
                }

                // Comprueba si el valor de ValorServicio es diferente de 0 antes de asignarlo.
                if (servicioCreacionDTO.ValorServicio != 0)
                {
                    servicioDB.ValorServicio = servicioCreacionDTO.ValorServicio;
                }
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
