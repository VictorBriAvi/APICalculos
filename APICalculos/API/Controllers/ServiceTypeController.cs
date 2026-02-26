using APICalculos.Application.DTOs.Services;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/serviceType")]
    [Authorize]
    public class ServiceTypeController : BaseController
    {
        private readonly IServiceTypeService _service;

        public ServiceTypeController(IServiceTypeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ServiceTypeDTO>>> Get(string? search, int? serviceCategorieId)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetAllServiceTypesAsync(storeId, search, serviceCategorieId);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.GetServiceTypeForId(id, storeId);

            if (result == null)
                return NotFound("Tipo de servicio no encontrado");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServiceTypeCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                var result = await _service.AddServiceTypeAsync(storeId, dto);
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
        public async Task<IActionResult> Put(int id, [FromBody] ServiceTypeCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _service.UpdateServiceTypeAsync(id, storeId, dto);
                return Ok("Se modificó exitosamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de servicio no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _service.DeleteServiceTypeAsync(id, storeId);
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
        public async Task<ActionResult<List<ServicesSearchDTO>>> Search(string query)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _service.SearchServiceAsync(storeId, query);
            return Ok(result);
        }
    }
}
