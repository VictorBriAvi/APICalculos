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
        public async Task<ActionResult<List<SaleDTO>>> GetAllSaleAsync()
        {
            var saleDto = await _saleService.GetAllSaleAsync();
            return Ok(saleDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSaleForId(int id)
        {
            var saleDto = await _saleService.GetSaleForId(id);

            if (saleDto == null)
                return NotFound($"No se encontro la venta con el ID {id}");

            return Ok(saleDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SaleCreationDTO saleCreationDTO)
        {
            try
            {   //REVISAR
                // Llamamos al nuevo método que guarda venta + detalles
                var saleDTO = await _saleService.AddSaleWithDetailsAsync(saleCreationDTO);
                return Ok(saleDTO);
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
        public async Task<IActionResult> Put(int id, SaleCreationDTO saleCreationDTO)
        {
            try
            {
                await _saleService.UpdateSaleAsync(id, saleCreationDTO);
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
                await _saleService.DeleteSaleAsync(id);
                return Ok("Se ha eliminado la venta");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("venta no encontrado");
            }
        }
    }
}
