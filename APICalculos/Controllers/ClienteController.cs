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
    [Route("api/clientes")]

    public class ClienteController : ControllerBase
    {

        private readonly MyDbContext _context;
        private readonly IMapper _mapper;

        public ClienteController(MyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<List<ClienteDTO>> GetProjectTo()
        {
            return await _context.Clientes
                .ProjectTo<ClienteDTO>(_mapper.ConfigurationProvider).ToListAsync();

        }

        [HttpGet("buscarClienteId/{id:int}")]
        public async Task<ActionResult<Cliente>> BuscarClienteId(int id)
        {
            var clienteId = await _context.Clientes.FirstOrDefaultAsync(g => g.ClienteId == id);

            if (clienteId is null)
            {
                var mensajeError = $"No se encontró ningún producto con el Id '{id}'.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

            }

            var clienteDTO = new ClienteDTO
            {   
                ClienteId = clienteId.ClienteId,
                NombreCompletoCliente = clienteId.NombreCompletoCliente,
                NumeroDocumento = clienteId.NumeroDocumento,
                FechaNacimiento = clienteId.FechaNacimiento,
            };

            return Ok(clienteDTO);
        
        }

        [HttpGet("buscarClienteDocumento/{documento}")]
        public async Task<ActionResult<Cliente>> BuscarClienteDocumento(string documento)
        {
            var clienteCodigo = await _context.Clientes.FirstOrDefaultAsync(g => g.NumeroDocumento == documento);

            if (clienteCodigo is null)
            {
                var mensajeError = $"Este documento'{documento}' no existe.";
                return StatusCode((int)HttpStatusCode.NotFound, mensajeError);

            }

            var clienteDTO = new ClienteDTO
            {
                ClienteId = clienteCodigo.ClienteId,
                NombreCompletoCliente = clienteCodigo.NombreCompletoCliente,
    
            };

            return Ok(clienteDTO);
        }



        [HttpPost]
        public async Task<ActionResult<Cliente>> Post(ClienteCreacionDTO clienteCreacionDTO)
        {
            var existeNombreCliente = await _context.Clientes.AnyAsync(g => g.NombreCompletoCliente.Replace(" ", "").Trim() == clienteCreacionDTO.NombreCompletoCliente.Replace(" ", "").Trim());
            var existeDocumentoCliente = await _context.Clientes.AnyAsync(g => g.NumeroDocumento.Replace(" ", "").Trim() == clienteCreacionDTO.NumeroDocumento.Replace(" ", "").Trim());

            if (string.IsNullOrWhiteSpace(clienteCreacionDTO.NombreCompletoCliente ) )
            {
                return BadRequest("no puede estar vacío");
            }

            if (existeNombreCliente)
            {
                var texto = $"Este codigo ya esta existente";
                return BadRequest(texto);

            };

            if (!string.IsNullOrWhiteSpace(clienteCreacionDTO.NumeroDocumento))
            {
                if (existeDocumentoCliente)
                {
                    var texto = $"El documento del cliente ya esta existente";
                    return BadRequest(texto);

                };
            }



            var cliente = _mapper.Map<Cliente>(clienteCreacionDTO);
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            var mensaje = "Se agrego correctamente :D";
            return Ok(mensaje);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ClienteCreacionDTO clienteCreacionDTO, int id)
        {

            var existeNombreCliente = await _context.Clientes.AnyAsync(g => g.NombreCompletoCliente.Replace(" ", "").Trim() == clienteCreacionDTO.NombreCompletoCliente.Replace(" ", "").Trim());
            var existeDocumentoCliente = await _context.Clientes.AnyAsync(g => g.NumeroDocumento.Replace(" ", "").Trim() == clienteCreacionDTO.NumeroDocumento.Replace(" ", "").Trim());


            var clienteDB = await _context.Clientes.AsTracking().FirstOrDefaultAsync(a => a.ClienteId == id);


            if (existeNombreCliente)
            {
                var texto = $"El nombre del cliente ya existe";
                return BadRequest(texto);
            }


            if (!string.IsNullOrWhiteSpace(clienteCreacionDTO.NombreCompletoCliente))
            {
                clienteDB.NombreCompletoCliente = clienteCreacionDTO.NombreCompletoCliente;
            }
            if (!string.IsNullOrWhiteSpace(clienteCreacionDTO.NumeroDocumento))
            {
                clienteDB.NumeroDocumento = clienteCreacionDTO.NumeroDocumento;
            }


            if (clienteCreacionDTO.FechaNacimiento != DateTime.MinValue)
            {
                clienteDB.FechaNacimiento = clienteCreacionDTO.FechaNacimiento;
            }

            if (clienteDB is null)
            {
                return NotFound();

            }

            await _context.SaveChangesAsync();
            var mensaje = "se modifico exitosamente";
            return Ok(mensaje);
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            //productoDB busca el primer valor Id con el ingresado

            var clienteId = await _context.Clientes.FirstOrDefaultAsync(g => g.ClienteId == id);

            if (clienteId is null)
            {
                var textoNoEncontrado = "Este producto no existe";
                return StatusCode((int)HttpStatusCode.NotFound, textoNoEncontrado);

            }

            _context.Remove(clienteId);
            await _context.SaveChangesAsync();
            var mensaje = $"Se ha Eliminado el Producto";
            return Ok(mensaje);
        }



    }
}
