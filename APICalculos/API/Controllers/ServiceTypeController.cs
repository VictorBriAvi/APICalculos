using APICalculos.Application.DTOs;
using APICalculos.Application.DTOs.Client;
using APICalculos.Application.DTOs.Services;
using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/serviceType")]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceTypeDTO>>> GetAllServiceTypeAsync(
            [FromQuery] string? search,
            [FromQuery] int? serviceCategorieId
        )
        {
            var serviceTypeDto =
                await _serviceTypeService.GetAllServiceTypesAsync(search, serviceCategorieId);

            return Ok(serviceTypeDto);
        }


        [HttpGet("search-Id")]
        public async Task<ActionResult<IEnumerable<ServiceTypeDTO>>> SearchServices([FromQuery] int? categoryId)
        {
            var result = await _serviceTypeService.SearchServicesAsync(categoryId);
            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetServiceTypeForId(int id) 
        {
            var serviceTypeDto = await _serviceTypeService.GetServiceTypeForId(id);

            if (serviceTypeDto == null)
                return NotFound($"No se encontro el tipo de servicio con el ID {id}");

            return Ok(serviceTypeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServiceTypeCreationDTO serviceTypeCreationDTO)
        {
            try
            {
                var serviceTypeDto = await _serviceTypeService.AddServiceTypeAsync(serviceTypeCreationDTO);
                return Ok(serviceTypeDto);
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
        public async Task<IActionResult> Put(int id, ServiceTypeCreationDTO serviceTypeCreationDTO)
        {
            try
            {
                await _serviceTypeService.UpdateServiceTypeAsync(id, serviceTypeCreationDTO);
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
                await _serviceTypeService.DeleteServiceTypeAsync(id);
                return Ok("Se ha eliminado el tipo de servicio");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de servicio no encontrado");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ServicesSearchDTO>>> SearchServices([FromQuery] string query)
        {
            var result = await _serviceTypeService.SearchServiceAsync(query);
            return Ok(result);
        }

    }
}
