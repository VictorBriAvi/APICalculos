using APICalculos.Application.DTOs;
using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        void Remove(Employee employee);
        void Update(Employee employee);
        Task<bool> ExistsByNameAsync(string name);


    }
}
