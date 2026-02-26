using APICalculos.Application.DTOs.SaleDetail;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/saleDetail")]
    [Authorize]
    public class SaleDetailController : BaseController
    {
        private readonly ISaleDetailService _saleDetailService;

        public SaleDetailController(ISaleDetailService saleDetailService)
        {
            _saleDetailService = saleDetailService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SaleDetailDTO>>> GetAll()
        {
            var storeId = GetStoreIdFromToken();
            var result = await _saleDetailService.GetAllSaleDetailAsync(storeId);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _saleDetailService.GetSaleDetailForId(id, storeId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaleDetailCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _saleDetailService.AddSaleDetailAsync(dto, storeId);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] SaleDetailCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            await _saleDetailService.UpdateSaleDetailAsync(id, dto, storeId);
            return Ok("Se modificó exitosamente");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var storeId = GetStoreIdFromToken();
            await _saleDetailService.DeleteSaleDetailAsync(id, storeId);
            return Ok("Eliminado correctamente");
        }
    }
}
