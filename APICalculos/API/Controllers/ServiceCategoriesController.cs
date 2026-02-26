using APICalculos.Application.DTOs.ServiceCategories;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/serviceCategorie")]
    [Authorize]
    public class ServiceCategoriesController : BaseController
    {
        private readonly IServiceCategoriesService _service;

        public ServiceCategoriesController(IServiceCategoriesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceCategoriesDTO>>> Get(string? search)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetAllServiceCategoriesAsync(storeId, search);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetServiceCategorieForId(id, storeId);

            if (result == null)
                return NotFound("Categoría de servicio no encontrada");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServiceCategoriesCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                var result = await _service.AddServiceCategorieAsync(storeId, dto);
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ServiceCategoriesCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _service.UpdateServiceCategorieAsync(id, storeId, dto);
                return Ok("Se modificó exitosamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Categoría de servicio no encontrada");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _service.DeleteServiceCategorieAsync(id, storeId);
                return Ok("Se ha eliminado la categoría de servicio");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Categoría de servicio no encontrada");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
