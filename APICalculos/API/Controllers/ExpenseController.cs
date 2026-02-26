using APICalculos.Application.DTOs.Expense;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/expense")]
    public class ExpenseController : BaseController
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExpenseDTO>>> GetAll(
            [FromQuery] string? search,
            [FromQuery] int? expenseTypeId,
            [FromQuery] int? paymentTypeId,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate
        )
        {
            var storeId = GetStoreIdFromToken();
            var result = await _expenseService.GetAllExpenseAsync(
                storeId, search, expenseTypeId, paymentTypeId, fromDate, toDate
            );

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetForId(int id)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _expenseService.GetExpenseForIdAsync(id, storeId);

            if (result == null)
                return NotFound("Gasto no encontrado");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExpenseCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                var result = await _expenseService.AddExpensesAsync(storeId, dto);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ExpenseCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _expenseService.UpdateExpenseAsync(id, storeId, dto);
                return Ok("Se modificó exitosamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Gasto no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _expenseService.DeleteExpenseAsync(id, storeId);
                return Ok("Se eliminó correctamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Gasto no encontrado");
            }
        }
    }
}
