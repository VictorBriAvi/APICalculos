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
    [Route("api/empleados")]
    public class EmpleadoController : ControllerBase
    {

        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public EmpleadoController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<List<EmpleadoDTO>> GetProjectTo()
        {
            return await _context.Empleados
                .ProjectTo<EmpleadoDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpGet("buscarEmpleadoPorId/{id:int}")]
        public async Task<ActionResult<Cliente>> BuscarEmpleadoPorId(int id)
        {
            var empleadoId = await _context.Empleados.FirstOrDefaultAsync(g => g.EmpleadoId == id);

            if (empleadoId is null)
            {
                var mensajeError = $"No se encontró ningún producto con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

            }

            var empleadoDTO = new EmpleadoDTO
            {
                EmpleadoId = empleadoId.EmpleadoId,
                NombreCompletoEmpleado = empleadoId.NombreCompletoEmpleado,
                DocumentoNacional = empleadoId.DocumentoNacional,
                FechaNacimiento = empleadoId.FechaNacimiento
            };

            return Ok(empleadoDTO);

        }

        [HttpPost]
        public async Task<ActionResult<Empleado>> Post(EmpleadoCreacionDTO empleadoCreacionDTO)
        {
            var existeNombreEmpleado = await _context.Empleados.AnyAsync(g => g.NombreCompletoEmpleado.Replace(" ", "").Trim() == empleadoCreacionDTO.NombreCompletoEmpleado.Replace(" ", "").Trim());
            var existeDocumentoEmpleado = await _context.Empleados.AnyAsync(g => g.DocumentoNacional.Replace(" ", "").Trim() == empleadoCreacionDTO.DocumentoNacional.Replace(" ", "").Trim());

            if (string.IsNullOrWhiteSpace(empleadoCreacionDTO.NombreCompletoEmpleado))
            {
                return BadRequest("El nombre no puede estar vacío");
            }
            if (existeNombreEmpleado)
            {
                var texto = $"Este nombre  ya esta existente";
                return BadRequest(texto);

            };
            if (string.IsNullOrWhiteSpace(empleadoCreacionDTO.DocumentoNacional))
            {
                return BadRequest("El documento no puede estar vacío");
            }
            if (existeDocumentoEmpleado)
            {
                var texto = $"Este documento  ya esta existente";
                return BadRequest(texto);

            };

            var empleado = _mapper.Map<Empleado>(empleadoCreacionDTO);
            _context.Add(empleado);
            await _context.SaveChangesAsync();
            var mensaje = "Se agrego correctamente :D";
            return Ok(mensaje);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(EmpleadoCreacionDTO empleadoCreacionDTO, int id)
        {

            //var existeNombreEmpleado = await _context.Empleados.AnyAsync(g => g.NombreCompletoEmpleado.Replace(" ", "").Trim() == empleadoCreacionDTO.NombreCompletoEmpleado.Replace(" ", "").Trim());

            //var existeDocumentoEmpleado = await _context.Empleados.AnyAsync(g => g.DocumentoNacional.Replace(" ", "").Trim() == empleadoCreacionDTO.DocumentoNacional.Replace(" ", "").Trim());

            var empleadoDB = await _context.Empleados.AsTracking().FirstOrDefaultAsync(a => a.EmpleadoId == id);


            //if (existeNombreEmpleado)
            //{
            //    var texto = $"Este codigo ya esta existente";
            //    return BadRequest(texto);
            //}
            //if (existeDocumentoEmpleado)
            //{
            //    var texto = $"Este documento ya esta existente";
            //    return BadRequest(texto);
            //}

            if (empleadoDB is null)
            {
                return NotFound();

            }

            if (!string.IsNullOrWhiteSpace(empleadoCreacionDTO.NombreCompletoEmpleado))
            {
                empleadoDB.NombreCompletoEmpleado = empleadoCreacionDTO.NombreCompletoEmpleado;
            }
            if (!string.IsNullOrWhiteSpace(empleadoCreacionDTO.DocumentoNacional))
            {
                empleadoDB.DocumentoNacional = empleadoCreacionDTO.DocumentoNacional;
            }
            if (empleadoCreacionDTO.FechaNacimiento != DateTime.MinValue)
            {
                empleadoDB.FechaNacimiento = empleadoCreacionDTO.FechaNacimiento;
                
            }

           
            await _context.SaveChangesAsync();
            var mensaje = "se modifico exitosamente";
            return Ok(mensaje);


        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            //productoDB busca el primer valor Id con el ingresado

            var empleadoId = await _context.Empleados.FirstOrDefaultAsync(g => g.EmpleadoId == id);

            if (empleadoId is null)
            {
                var textoNoEncontrado = "Este producto no existe";
                return StatusCode((int)HttpStatusCode.NotFound, textoNoEncontrado);

            }

            _context.Remove(empleadoId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se ha Eliminado el Producto";
            return Ok(mensaje);
        }
    }
}
