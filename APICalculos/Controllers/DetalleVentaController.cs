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
    [Route("api/detalleVenta")]
    public class DetalleVentaController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public DetalleVentaController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IEnumerable<DetalleVentaDTO>> DetalleVentaGet()
        {
            return await _context.DetalleVentas
                .Include(d => d.TipoDeServicio)
                .ProjectTo<DetalleVentaDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpGet("buscarDetalleVentaId/{id:int}")]
        public async Task<ActionResult<DetalleVenta>> BuscarDetalleVentaId(int id)
        {
            var detalleVentaId = await _context.DetalleVentas
                .Include(s => s.Venta.Cliente)
                .Include(s => s.TipoDeServicio)
                .Include(s => s.Empleado)
                .FirstOrDefaultAsync(g => g.DetalleVentaId == id);

            if (detalleVentaId is null)
            {
                var mensajeError = $"No se encontró ningún producto con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }

            var detalleVentaDTO = new DetalleVentaDTO
            {
                DetalleVentaId = detalleVentaId.DetalleVentaId,
                NombreClienteVenta = detalleVentaId.Venta.Cliente.NombreCompletoCliente ,
                NombreTipoDeServicioVenta = detalleVentaId.TipoDeServicio.NombreServicio,
                PrecioTipoDeServicio = detalleVentaId.TipoDeServicio.PrecioServicio,
                NombreEmpleadoVenta = detalleVentaId.Empleado.NombreCompletoEmpleado,
            };

            return Ok(detalleVentaDTO);
        }


        [HttpGet("buscarDetallesPorVentaId/{ventaId:int}")]
        public async Task<ActionResult<IEnumerable<DetalleVentaDTO>>> BuscarDetallesPorVentaId(int ventaId)
        {
            var detallesVenta = await _context.DetalleVentas
                .Include(s => s.Venta.Cliente)
                .Include(s => s.TipoDeServicio)
                .Include(s => s.Empleado)
                .Where(g => g.VentaId == ventaId)
                .ToListAsync();

            if (!detallesVenta.Any())
            {
                var mensajeError = $"No se encontraron detalles de venta para la VentaId '{ventaId}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }

            var detallesVentaDTO = detallesVenta.Select(detalle => new DetalleVentaDTO
            {
                DetalleVentaId = detalle.DetalleVentaId,
                NombreClienteVenta = detalle.Venta.Cliente.NombreCompletoCliente,
                NombreTipoDeServicioVenta = detalle.TipoDeServicio.NombreServicio,
                PrecioTipoDeServicio = detalle.TipoDeServicio.PrecioServicio,
                NombreEmpleadoVenta = detalle.Empleado.NombreCompletoEmpleado,
            }).ToList();

            return Ok(detallesVentaDTO);
        }



        [HttpPost]
        public async Task<ActionResult<DetalleVenta>> AgregarDetalleVenta (DetalleVentaCreacionDTO detalleVentaCreacionDTO)
        {
            var venta = await _context.Ventas.FindAsync(detalleVentaCreacionDTO.VentaId);
            var tipoDeServicio = await _context.TipoDeServicios.FindAsync(detalleVentaCreacionDTO.TipoDeServicioId);
            var empleado = await _context.Empleados.FindAsync(detalleVentaCreacionDTO.EmpleadoId);


            if (tipoDeServicio == null)
            {
                return BadRequest("El tipo de servicio no existe");
            }
            if (empleado == null)
            {
                return BadRequest("El empleado no existe");
            }
            if (venta == null)
            {
                return BadRequest("La venta no existe");
            }

            var detalleVenta = _mapper.Map<DetalleVenta>(detalleVentaCreacionDTO);
            _context.Add(detalleVenta);
            await _context.SaveChangesAsync();
            var mensaje = "Se agrego correctamente :D";
            return Ok(mensaje);


        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> EditarDetalleVenta(DetalleVentaCreacionDTO detalleVentaCreacionDTO, int id)
        {
            var detalleVentaDB = await _context.DetalleVentas.AsTracking().FirstOrDefaultAsync(a => a.DetalleVentaId == id);

            if (detalleVentaDB != null)
            {
                if (detalleVentaCreacionDTO.VentaId != 0 )
                {
                    detalleVentaDB.VentaId = detalleVentaCreacionDTO.VentaId;
                }
                if (detalleVentaCreacionDTO.TipoDeServicioId != 0)
                {
                    detalleVentaDB.TipoDeServicioId = detalleVentaCreacionDTO.TipoDeServicioId;

                }
                if (detalleVentaCreacionDTO.EmpleadoId != 0)
                {
                    detalleVentaDB.EmpleadoId = detalleVentaCreacionDTO.EmpleadoId;
                }

                await _context.SaveChangesAsync();
                var mensaje = "Se modificó correctamente.";
                return Ok(mensaje);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> EliminarDetalleVenta (int id)
        {
            var detalleVentaId = await _context.DetalleVentas.FirstOrDefaultAsync(g => g.DetalleVentaId == id);

            _context.Remove(detalleVentaId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se ha Eliminado el Producto";
            return Ok(mensaje);
        }


    }
}
