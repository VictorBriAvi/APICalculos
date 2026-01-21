using APICalculos.Application.DTOs.Client;
using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }


        [HttpGet]
        public async Task<ActionResult<List<ClientDTO>>> ObtenerClientes()
        {
            var clientesDto = await _clientService.GetAllClientsAsync();
            return Ok(clientesDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> BuscarClienteId(int id)
        {
            var clienteDto = await _clientService.GetClientForIdAsync(id);
            if (clienteDto == null)
                return NotFound($"No se encontró el cliente con ID {id}");

            return Ok(clienteDto);
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> Post(ClientCreationDTO clienteCreacionDTO)
        {
            try
            {
                var clienteDTO = await _clientService.AddAsync(clienteCreacionDTO);
                return Ok(clienteDTO);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ClientCreationDTO clienteCreacionDTO)
        {
            try
            {
                await _clientService.UpdateAsync(id, clienteCreacionDTO);
                return Ok("Se modificó exitosamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cliente no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _clientService.DeleteAsync(id);
                return Ok("Se ha eliminado el cliente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de pago no encontrado");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // 409
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ClientSearchDTO>>> SearchClients(
    [FromQuery] string query)
        {
            var result = await _clientService.SearchClientsAsync(query);
            return Ok(result);
        }

    }
}
