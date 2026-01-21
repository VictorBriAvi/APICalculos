using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/serviceCategorie")]
    public class ServiceCategoriesController : ControllerBase
    {
        private readonly IServiceCategoriesService _serviceCategoriesService;

        public ServiceCategoriesController(IServiceCategoriesService serviceCategoriesService)
        {
            _serviceCategoriesService = serviceCategoriesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceCategoriesDTO>>> GetServiceCategorieAsync([FromQuery] string? search)
        {
            var serviceCategorieDto = await _serviceCategoriesService.GetAllServiceCategoriesAsync(search);

            return Ok(serviceCategorieDto);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetServiceCategorieForId(int id)
        {
            var serviceCategorieDto = await _serviceCategoriesService.GetServiceCategorieForId(id);

            if (serviceCategorieDto == null)
                return NotFound($"No se encontro el cliente con el ID {id}");

            return Ok(serviceCategorieDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ServiceCategoriesCreationDTO serviceCategoriesCreationDTO)
        {
            try
            {
                var serviceCategorieDto = await _serviceCategoriesService.AddServiceCategorieAsync(serviceCategoriesCreationDTO);
                return Ok(serviceCategorieDto);
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
        public async Task<IActionResult> Put(int id, ServiceCategoriesCreationDTO serviceCategoriesCreationDTO)
        {
            try
            {
                await _serviceCategoriesService.UpdateServiceCategorieAsync(id, serviceCategoriesCreationDTO);
                return Ok("Se modifico exitosamente");
            }
            catch (KeyNotFoundException)
            {

                return NotFound("Categoria servicio no encontrada");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _serviceCategoriesService.DeleteServiceCategorieAsync(id);
                return Ok("Se ha eliminado la categoria servicio");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Categoria servicio no encontrada");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
