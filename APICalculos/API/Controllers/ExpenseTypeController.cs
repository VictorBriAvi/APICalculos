using APICalculos.Application.DTOs.ExpenseType;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/expenseType")]
    public class ExpenseTypeController : BaseController
    {
        private readonly IExpenseTypeService _expenseTypeService;

        public ExpenseTypeController(IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExpenseTypeDTO>>> Get([FromQuery] string? search)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _expenseTypeService.GetAllExpensesTypesAsync(storeId, search);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetForId(int id)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _expenseTypeService.GetExpenseTypeForId(id, storeId);

            if (result == null)
                return NotFound("Tipo de gasto no encontrado");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExpenseTypeCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                var result = await _expenseTypeService.AddExpenseTypeAsync(storeId, dto);
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
        public async Task<IActionResult> Put(int id, [FromBody] ExpenseTypeCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _expenseTypeService.UpdateExpenseTypeAsync(id, storeId, dto);
                return Ok("Se modificó correctamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de gasto no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _expenseTypeService.DeleteExpenseTypeAsync(id, storeId);
                return Ok("Se eliminó correctamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de gasto no encontrado");
            }
        }
    }
}
