using APICalculos.Application.DTOs;
using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync(int storeId, string? search);
        Task<Employee?> GetByIdAsync(int id, int storeId);
        Task AddAsync(Employee employee);
        void Update(Employee employee);
        void Remove(Employee employee);
        Task<bool> ExistsByNameAsync(string name, int storeId);
        Task<List<Employee>> SearchAsync(int storeId, string query, int limit);

    }
}
