using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/customerHistory")]
    public class CustomerHistoryController : ControllerBase
    {
        private readonly ICustomerHistoryService _customerHistoryService;

        public CustomerHistoryController(ICustomerHistoryService customerHistoryService)
        {
            _customerHistoryService = customerHistoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerHistoryDTO>>> GetAllCustomerHistoryAsync()
        {
            var customerHistoriesDto = await _customerHistoryService.GetAllCustomerHistoriesAsync();
            return Ok(customerHistoriesDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCustomerHistoryForId(int id)
        {
            var customerHistoryDto = await _customerHistoryService.GetCustomerHistoryForId(id);

            if (customerHistoryDto == null)
                return NotFound($"No se encontro el tipo de servicio con el ID {id}");

            return Ok(customerHistoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CustomerHistoryCreationDTO customerHistoryCreationDTO)
        {
            try
            {
                var customerHistoryDto = await _customerHistoryService.AddCustomerHistoryAsync(customerHistoryCreationDTO);
                return Ok(customerHistoryDto);
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
        public async Task<IActionResult> Put(int id, CustomerHistoryUpdateDTO customerHistoryCreationDTO)
        {
            try
            {
                await _customerHistoryService.UpdateCustomerHistoryAsync(id, customerHistoryCreationDTO);
                return Ok("Se modifico exitosamente");
            }
            catch (KeyNotFoundException)
            {

                return NotFound("Tipo de servicio no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _customerHistoryService.DeleteCustomerHistoriesAsync(id);
                return Ok("Se ha eliminado el tipo de servicio");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de servicio no encontrado");
            }
        }

    }
}
