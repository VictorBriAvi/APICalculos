using APICalculos.Domain.Entidades;
using System.Linq.Expressions;

namespace APICalculos.Application.Interfaces
{
    public interface IServiceTypeRepository
    {
        Task<IEnumerable<ServiceType>> GetAllAsync();
        Task<IEnumerable<ServiceType>> SearchAsync(Expression<Func<ServiceType, bool>> predicate);
        Task<ServiceType> GetByIdAsync(int Id);
        Task AddAsync(ServiceType serviceType);
        void Update(ServiceType serviceType);
        void Remove(ServiceType serviceType);
        Task<bool> ExistsByNameAsync(string name);
    }
}
