using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/expenseType")]
    public class ExpenseTypeController : ControllerBase
    {
        private readonly IExpenseTypeService _expenseTypeService;
    
        public ExpenseTypeController(IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExpenseTypeDTO>>> GetExpenseTypeAsync()
        {
            var expenseTypeDto = await _expenseTypeService.GetAllExpensesTypesAsync();
            return Ok(expenseTypeDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetExpenseTypeForId(int id)
        {
            var expenseTypeDto = await _expenseTypeService.GetExpenseTypeForId(id);

            if (expenseTypeDto == null)
                return NotFound($"No se encontro el tipo de pago con el ID {id}");

            return Ok(expenseTypeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExpenseTypeCreationDTO expenseTypeCreationDTO)
        {
            try
            {
                var expenseTypeDto = await _expenseTypeService.AddExpenseTypeAsync(expenseTypeCreationDTO);
                return Ok(expenseTypeDto);
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
        public async Task<IActionResult> Put(int id, ExpenseTypeCreationDTO expenseTypeCreationDTO)
        {
            try
            {
                await _expenseTypeService.UpdateExpenseTypeAsync(id, expenseTypeCreationDTO);
                return Ok("Se modifico exitosamente");
            }
            catch (KeyNotFoundException)
            {

                return NotFound("Tipo de gasto no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _expenseTypeService.DeleteExpenseTypeAsync(id);
                return Ok("Se ha eliminado el tipo de gasto");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de gasto no encontrado");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
