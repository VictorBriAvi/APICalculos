using APICalculos.Application.DTOs.CustomerHistory;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/customerHistory")]
    public class CustomerHistoryController : BaseController
    {
        private readonly ICustomerHistoryService _service;

        public CustomerHistoryController(ICustomerHistoryService service)
        {
            _service = service;
        }

        // ✅ GET ALL
        [HttpGet]
        public async Task<ActionResult<List<CustomerHistoryDTO>>> GetAll()
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetAllCustomerHistoriesAsync(storeId);
            return Ok(result);
        }

        // ✅ GET BY ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetCustomerHistoryForId(id, storeId);

            if (result == null)
                return NotFound($"No se encontró el historial con ID {id}");

            return Ok(result);
        }

        // ✅ CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerHistoryCreationDTO dto)
        {
            try
            {
                var storeId = GetStoreIdFromToken();
                var result = await _service.AddCustomerHistoryAsync(storeId, dto);
                return Ok(result);
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

        // ✅ UPDATE
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerHistoryUpdateDTO dto)
        {
            try
            {
                var storeId = GetStoreIdFromToken();
                await _service.UpdateCustomerHistoryAsync(id, storeId, dto);
                return Ok("Historial actualizado correctamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Historial no encontrado");
            }
        }

        // ✅ DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var storeId = GetStoreIdFromToken();
                await _service.DeleteCustomerHistoriesAsync(id, storeId);
                return Ok("Historial eliminado correctamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Historial no encontrado");
            }
        }
    }
}
