using APICalculos.Application.DTOs.Client;
using APICalculos.Application.DTOs.Employee;
using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace APICalculos.API.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAsync()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetForIdAsync(int id)
        {
            var employee = await _employeeService.GetEmployeeForIdAsync(id);  
            if (employee == null)
                return NotFound($"No se encontró el colaborador con ID {id}");

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDTO>> Create(EmployeeCreationDTO employeeCreationDTO)
        {
            try
            {
                var employee = await _employeeService.AddEmployeeAsync(employeeCreationDTO);
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
        public async Task<IActionResult> UpdateAsync(int id, EmployeeCreationDTO employeeCreationDTO)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(id, employeeCreationDTO);
                return Ok("Se modificó exitosamente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Cliente no encontrado");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return Ok("Se ha eliminado el cliente");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Tipo de pago no encontrado");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ClientSearchDTO>>> SearchClients( [FromQuery] string query)
        {
            var result = await _employeeService.SearchEmployeeAsync(query);
            return Ok(result);
        }

    }
}
