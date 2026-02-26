using APICalculos.Application.DTOs.Store;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _storeService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _storeService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreCreateDto dto)
        {
            var result = await _storeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StoreUpdateDTO dto)
        {
            var updated = await _storeService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _storeService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }

}
