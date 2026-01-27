using APICalculos.Application.DTOs.Employee;

namespace APICalculos.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployeesAsync(string? search);
        Task<EmployeeDTO> GetEmployeeForIdAsync(int id);
        Task<EmployeeDTO> AddEmployeeAsync(EmployeeCreationDTO clienteCreacionDTO);
        Task UpdateEmployeeAsync(int id, EmployeeCreationDTO clienteCreacionDTO);
        Task DeleteEmployeeAsync(int id);
        Task<List<EmployeeSearchDTO>> SearchEmployeeAsync(string query);
    }
}
