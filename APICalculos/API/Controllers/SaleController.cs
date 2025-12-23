using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/sale")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetAllAsync()
        {
            var sales = await _saleService.GetAllSaleAsync();
            return Ok(sales);
        }

        [HttpGet("by-date-range")]
        public async Task<List<SaleDTO>> GetSalesByDateRangeAsync(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate)
        {
            return await _saleService.GetSalesByDateRangeAsync(fromDate, toDate);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var sale = await _saleService.GetSaleForId(id);
            if (sale == null)
                return NotFound($"No se encontró la venta con ID {id}");

            return Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] SaleCreationDTO saleCreationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (saleCreationDTO.ClientId <= 0)
                return BadRequest("Debe especificar un cliente válido.");

            try
            {
                var createdSale = await _saleService.AddSaleWithDetailsAsync(saleCreationDTO);

                return Ok(createdSale);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] SaleCreationDTO saleCreationDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _saleService.UpdateSaleAsync(id, saleCreationDTO);
                return Ok($"La venta con ID {id} fue actualizada correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"No se encontró la venta con ID {id}");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _saleService.DeleteSaleAsync(id);
                return Ok($"La venta con ID {id} fue eliminada correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"No se encontró la venta con ID {id}");
            }
        }
    }
}
