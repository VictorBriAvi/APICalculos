using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/Expense")]
    public class ExpenseController :  ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExpenseDTO>>> GetAll()
        {
            var expenseDto = await _expenseService.GetAllExpenseAsync();
            return Ok(expenseDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetForId(int id)
        {
            var expenseDto = await _expenseService.GetExpenseForIdAsync(id);

            if (expenseDto == null)
                return NotFound($"No se encontro el tipo de servicio con el ID {id}");

            return Ok(expenseDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ExpenseCreationDTO expenseCreationDTO)
        {
            try
            {
                var expenseDto = await _expenseService.AddExpensesAsync(expenseCreationDTO);
                return Ok(expenseDto);
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
        public async Task<IActionResult> Put(int id, ExpenseCreationDTO expenseCreationDTO)
        {
            try
            {
                await _expenseService.UpdateExpenseAsync(id, expenseCreationDTO);
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
                await _expenseService.DeleteExpenseAsync(id);
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
    }
}
