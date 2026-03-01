using APICalculos.Application.DTOs.Sale;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/sale")]
    [Authorize]
    public class SaleController : BaseController
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDTO>>> GetAllAsync()
        {
            var storeId = GetStoreIdFromToken();
            var sales = await _saleService.GetAllSaleAsync(storeId);
            return Ok(sales);
        }

        [HttpGet("by-date-range")]
        public async Task<ActionResult<List<SaleDTO>>> GetSalesByDateRangeAsync([FromQuery] DateTime? fromDate,[FromQuery] DateTime? toDate,[FromQuery] int? clientId,[FromQuery] int? paymentTypeId,[FromQuery] int? employeeId,[FromQuery] int? serviceTypeId)
        {
            if (fromDate == default || toDate == default)
                return BadRequest("Las fechas son obligatorias.");

            var storeId = GetStoreIdFromToken();

            var result = await _saleService.GetFilteredSalesAsync(storeId,fromDate,toDate,clientId,paymentTypeId,employeeId,serviceTypeId);

            return Ok(result);
        }

        [HttpGet("filtered")]
        public async Task<ActionResult<List<SaleDTO>>> GetFiltered([FromQuery] DateTime? fromDate,[FromQuery] DateTime? toDate, [FromQuery] int? clientId,[FromQuery] int? paymentTypeId,[FromQuery] int? employeeId,[FromQuery] int? serviceTypeId)
        {
            var storeId = GetStoreIdFromToken();

            var result = await _saleService.GetFilteredSalesAsync(storeId,fromDate,toDate,clientId,paymentTypeId,employeeId,serviceTypeId);

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var storeId = GetStoreIdFromToken();
            var sale = await _saleService.GetSaleForId(id, storeId);

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

            var storeId = GetStoreIdFromToken();
            try
            {
                var createdSale = await _saleService.AddSaleWithDetailsAsync(storeId, saleCreationDTO);
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

            var storeId = GetStoreIdFromToken();
            try
            {
                await _saleService.UpdateSaleAsync(id, storeId, saleCreationDTO);
                return Ok($"La venta con ID {id} fue actualizada correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"No se encontró la venta con ID {id}");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _saleService.DeleteSaleAsync(id, storeId);
                return Ok($"La venta con ID {id} fue eliminada correctamente.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"No se encontró la venta con ID {id}");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
