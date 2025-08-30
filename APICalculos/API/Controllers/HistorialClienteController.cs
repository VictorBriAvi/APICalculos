using APICalculos.Application.DTOs;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.API.Controllers
{
    [Route("api/HistorialCliente")]
    [ApiController]
    public class HistorialClienteController : ControllerBase
    {

        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public HistorialClienteController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<HistorialClienteDTO>> GetAllHistorialCliente()
        {
            return await _context.HistorialClientes
                .ProjectTo<HistorialClienteDTO>(_mapper.ConfigurationProvider).ToListAsync();   
        }

        [HttpGet("buscarHistorialClienteId/{id:int}")]
        public async Task<ActionResult<HistorialClientes>> GetHistorialClienteForId (int id)
        {
            var historialClienteId = await _context.HistorialClientes
                .FirstOrDefaultAsync(g => g.HistorialClientesId == id);



            return historialClienteId;
        }

        [HttpPost]
        public async Task<ActionResult<HistorialClientes>> IngresarNuevoHistorialCliente(HistorialClienteCreacionDTO historialClienteCreacionDTO)
        {
            var historialCliente = _mapper.Map<HistorialClientes>(historialClienteCreacionDTO);
            _context.Add(historialCliente);
            await _context.SaveChangesAsync();
            var mensaje = "Se agrego un nuevo historial de un cliente";
            return Ok(mensaje);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> ModificarHistorialCliente(HistorialClienteCreacionDTO historialClienteCreacionDTO , int id)
        {
            var historialClienteDB = await _context.HistorialClientes.AsTracking().FirstOrDefaultAsync(a => a.HistorialClientesId == id);


            if (historialClienteDB != null)
            {
                if (historialClienteCreacionDTO.ClienteId != 0)
                {
                    historialClienteDB.ClienteId = historialClienteCreacionDTO.ClienteId;

                }

                if (!string.IsNullOrWhiteSpace(historialClienteCreacionDTO.NombreDeHistorialCliente))
                {
                    historialClienteDB.NombreDeHistorialCliente = historialClienteCreacionDTO.NombreDeHistorialCliente;
                }

                if (!string.IsNullOrWhiteSpace(historialClienteCreacionDTO.DescripcionHistorialCliente))
                {
                    historialClienteDB.DescripcionHistorialCliente = historialClienteCreacionDTO.DescripcionHistorialCliente;
                }

                if (historialClienteCreacionDTO.FechaHistorial != default)
                {
                    historialClienteDB.FechaHistorial = historialClienteCreacionDTO.FechaHistorial;
                }



                await _context.SaveChangesAsync();
                var mensaje = "Se modificó correctamente.";
                return Ok(mensaje);
            } else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteHistorialCliente(int id)
        {
                var historialClienteId = await  _context.HistorialClientes.FirstOrDefaultAsync(g => g.HistorialClientesId == id);

            _context.Remove(historialClienteId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se ha eliminado un gasto";
            return Ok(mensaje);
        }
    }

}
