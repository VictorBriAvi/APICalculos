using APICalculos.Application.DTOs.Employee;
using APICalculos.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAsync([FromQuery] string? search)
        {
            var storeId = GetStoreIdFromToken();
            var employees = await _employeeService.GetAllEmployeesAsync(storeId, search);
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetForIdAsync(int id)
        {
            var storeId = GetStoreIdFromToken();
            var employee = await _employeeService.GetEmployeeForIdAsync(id, storeId);

            if (employee == null)
                return NotFound("Colaborador no encontrado");

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> Create([FromBody] EmployeeCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                var employee = await _employeeService.AddEmployeeAsync(storeId, dto);
                return Ok(employee);
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
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] EmployeeCreationDTO dto)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _employeeService.UpdateEmployeeAsync(id, storeId, dto);
                return Ok("Se modificó exitosamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Colaborador no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var storeId = GetStoreIdFromToken();
            try
            {
                await _employeeService.DeleteEmployeeAsync(id, storeId);
                return Ok("Se eliminó correctamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Colaborador no encontrado");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<EmployeeSearchDTO>>> Search([FromQuery] string query)
        {
            var storeId = GetStoreIdFromToken();
            var result = await _employeeService.SearchEmployeeAsync(storeId, query);
            return Ok(result);
        }
    }
}
