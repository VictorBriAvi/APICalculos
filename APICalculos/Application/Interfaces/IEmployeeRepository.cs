using APICalculos.Application.DTOs;
using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeModel>> GetAllAsync();
        Task<EmployeeModel> GetByIdAsync(int id);
        Task AddAsync(EmployeeModel employee);
        void Remove(EmployeeModel employee);
        void Update(EmployeeModel employee);
        Task<bool> ExistsByNameAsync(string name);


    }
}
