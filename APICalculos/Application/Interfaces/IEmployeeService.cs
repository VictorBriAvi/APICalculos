using APICalculos.Application.DTOs.Employee;

namespace APICalculos.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDTO>> GetAllEmployeesAsync(int storeId, string? search);
        Task<EmployeeDTO?> GetEmployeeForIdAsync(int id, int storeId);
        Task<EmployeeDTO> AddEmployeeAsync(int storeId, EmployeeCreationDTO dto);
        Task UpdateEmployeeAsync(int id, int storeId, EmployeeCreationDTO dto);
        Task DeleteEmployeeAsync(int id, int storeId);
        Task<List<EmployeeSearchDTO>> SearchEmployeeAsync(int storeId, string query);
    }
}
