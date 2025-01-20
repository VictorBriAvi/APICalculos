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
    [Route("api/venta")]
    public class VentaController : ControllerBase
    {
        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public VentaController(MyDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]

        public async Task<IEnumerable<VentaDTO>> ObtenerVenta()
        {
            return await _context.Ventas
                .Include(x => x.Cliente)
                .Include(x => x.TipoDePago)
                .Include(v => v.Detalle)
                    .ThenInclude( d => d.TipoDeServicio)
   
 
                .ProjectTo<VentaDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }

        [HttpGet("buscarVentaId/{id:int}")]

        public async Task<ActionResult<Venta>> BuscarVentaPorId(int id)
        {
            var ventaId = await _context.Ventas
                .Include(s => s.Cliente)
                .Include(s => s.TipoDePago)
                .FirstOrDefaultAsync(g => g.VentaId == id);

            if  (ventaId is null) 
            {
                var mensajeError = $"No se encontro ninguna Venta con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }

            var ventaDTO = new VentaDTO
            {
                VentaId = ventaId.VentaId,
                NombreCliente = ventaId.Cliente.NombreCompletoCliente,
                NombreTipoDePago = ventaId.TipoDePago.NombreTipoDePago,

                
                
    
                FechaVenta = ventaId.FechaVenta
            };

            return Ok(ventaDTO);

        }
                

        [HttpGet("VentaPorClienteYTipoDePago/{id:int}")]
        public async Task<ActionResult<Venta>> ObtenerVentaPorIdClienteYTipoDePago(int id)
        {
            var ventaId = await _context.Ventas
                .Include(s => s.Cliente)
                .Include(s => s.TipoDePago)
                .FirstOrDefaultAsync(g => g.VentaId == id);

            if (ventaId is null)
            {
                var mensajeError = $"No se encontro ninguna Venta con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);
            }

            var ventaDTO = new ClienteYTipoDePagoDTO
            {
                VentaId = ventaId.VentaId,
                ClienteId = ventaId.ClienteId,
                TipoDePagoId = ventaId.TipoDePagoId,




            };

            return Ok(ventaDTO);

        }

        [HttpPost]
        public async Task<ActionResult<Venta>> AgregarVenta(VentaCreacionDTO ventaCreacionDTO)
        {
            var clienteExiste = await _context.Clientes.FindAsync(ventaCreacionDTO.ClienteId);
            var tipoDePagoExiste = await _context.TipoDePagos.FindAsync(ventaCreacionDTO.TipoDePagoId);

            if (clienteExiste == null)
            {
                return BadRequest("El cliente no existe");
            }
            if (tipoDePagoExiste == null)
            {
                return BadRequest("El tipo de pago no existe");
            }

            var venta = _mapper.Map<Venta>(ventaCreacionDTO);
            _context.Add(venta);
            await _context.SaveChangesAsync();
            var mensaje = "Se Agrego con exito";
            return Ok(mensaje);
        }


        [HttpPost("agregarVentaConId")]
        public async Task<ActionResult<Venta>> AgregarVentaConId(VentaCreacionDTO ventaCreacionDTO)
        {
            var clienteExiste = await _context.Clientes.FindAsync(ventaCreacionDTO.ClienteId);
            var tipoDePagoExiste = await _context.TipoDePagos.FindAsync(ventaCreacionDTO.TipoDePagoId);

            if (clienteExiste == null)
            {
                return BadRequest("El cliente no existe");
            }
            if (tipoDePagoExiste == null)
            {
                return BadRequest("El tipo de pago no existe");
            }

            var venta = _mapper.Map<Venta>(ventaCreacionDTO);
            _context.Add(venta);
            await _context.SaveChangesAsync();
            var mensaje = "Se Agrego con exito";
            return Ok(venta.VentaId);
        }

        [HttpPut("{id:int}")]

        public async Task<ActionResult> EditarVenta(VentaCreacionDTO ventaCreacionDTO, int id) 
        {
            var ventaDB = await _context.Ventas.AsTracking().FirstOrDefaultAsync(a => a.VentaId == id);

            if (ventaDB != null)
            {
                if (ventaCreacionDTO.ClienteId != 0)
                {
                    ventaDB.ClienteId = ventaCreacionDTO.ClienteId;

                }

                if (ventaCreacionDTO.TipoDePagoId != 0)
                {

                    ventaDB.TipoDePagoId = ventaCreacionDTO.TipoDePagoId;
                }

                await _context.SaveChangesAsync();
                var mensaje = "se a editado con exito";
                return Ok(mensaje); 

            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> DeleteVenta (int id )
        {
            var ventaId = await _context.Ventas.FirstOrDefaultAsync(g => g.VentaId == id);

            _context.Remove(ventaId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se a eliminado la venta";
            return Ok(mensaje);
        }





    }
}
