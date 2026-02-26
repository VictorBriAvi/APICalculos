using APICalculos.Domain.Entidades;
using System.Linq.Expressions;

namespace APICalculos.Application.Interfaces
{
    public interface IServiceTypeRepository
    {
        Task<IEnumerable<ServiceType>> GetAllAsync(
            int storeId,
            string? search,
            int? serviceCategorieId
        );

        Task<IEnumerable<ServiceType>> SearchAsync(
            int storeId,
            Expression<Func<ServiceType, bool>> predicate
        );

        Task<List<ServiceType>> SearchAsync(
            int storeId,
            string query,
            int limit
        );

        Task<ServiceType?> GetByIdAsync(int id, int storeId);

        Task AddAsync(ServiceType serviceType);
        void Update(ServiceType serviceType);
        void Remove(ServiceType serviceType);

        Task<bool> ExistsByNameAsync(string name, int storeId);
    }
}
