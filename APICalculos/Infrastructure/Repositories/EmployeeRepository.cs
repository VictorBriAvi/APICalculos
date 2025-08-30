using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyDbContext _dbContext;

        public EmployeeRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllAsync()
        {
            return await _dbContext.Employees.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }
        
        public async Task<EmployeeModel> GetByIdAsync(int id)
        {
            return await _dbContext.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(EmployeeModel employee)
        {
            await _dbContext.Employees.AddAsync(employee);
        }

        public void Remove(EmployeeModel employee)
        {
            _dbContext.Employees.Remove(employee);
        }

        public void Update(EmployeeModel employee)
        {
            _dbContext.Employees.Update(employee);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            var nameEmployee = name.Replace(" ", "").Trim();
            return await _dbContext.Employees.AnyAsync(c => c.Name.Replace(" ", "").Trim() == nameEmployee);        
        }
    }
}
