using APICalculos.Application.DTOs.Client;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/client")]
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClientDTO>>> ObtenerClientes([FromQuery] string? search)
        {
            var storeId = GetStoreIdFromToken();
            var clientDto = await _clientService.GetAllClientsAsync(storeId, search);
            return Ok(clientDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> BuscarClienteId(int id)
        {
            var storeId = GetStoreIdFromToken();
            var clienteDto = await _clientService.GetClientForIdAsync(id, storeId);
            if (clienteDto == null) return NotFound($"No se encontró el cliente con ID {id}");
            return Ok(clienteDto);
        }

        [HttpPost]
        public async Task<ActionResult<ClientDTO>> Post(ClientCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            var clienteDTO = await _clientService.AddAsync(storeId, dto);
            return Ok(clienteDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ClientUpdateDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            await _clientService.UpdateAsync(id, storeId, dto);
            return Ok("Se modificó exitosamente");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var storeId = GetStoreIdFromToken();
            await _clientService.DeleteAsync(id, storeId);
            return Ok("Se ha eliminado el cliente");
        }
    }
}
