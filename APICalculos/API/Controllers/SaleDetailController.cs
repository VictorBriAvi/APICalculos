using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/saleDetail")]
    public class SaleDetailController : ControllerBase
    {
        private readonly ISaleDetailService _saleDetailService;

        public SaleDetailController(ISaleDetailService saleDetailService)
        {
            _saleDetailService = saleDetailService;
        }


        [HttpGet]
        public async Task<ActionResult<List<SaleDetailDTO>>> GetAllSaleDetailAsync()
        {
            var saleDetailDto = await _saleDetailService.GetAllSaleDetailAsync();
            return Ok(saleDetailDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSaleDetailForId(int id)
        {
            var saleDetailDto = await _saleDetailService.GetSaleDetailForId(id);

            if (saleDetailDto == null)
                return NotFound($"No se encontro el tipo de servicio con el ID {id}");

            return Ok(saleDetailDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(SaleDetailCreationDTO saleDetailCreationDTO)
        {
            try
            {
                var saleDetailDTO = await _saleDetailService.AddSaleDetailAsync(saleDetailCreationDTO);
                return Ok(saleDetailDTO);
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
        public async Task<IActionResult> Put(int id, SaleDetailCreationDTO saleDetailCreationDTO)
        {
            try
            {
                await _saleDetailService.UpdateSaleDetailAsync(id, saleDetailCreationDTO);
                return Ok("Se modifico exitosamente");
            }
            catch (KeyNotFoundException)
            {

                return NotFound("detalle servicio no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _saleDetailService.DeleteSaleDetailAsync(id);
                return Ok("Se ha eliminado el tipo de servicio");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de servicio no encontrado");
            }
        }


    }
}
