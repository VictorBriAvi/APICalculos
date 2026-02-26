using APICalculos.Domain.Entidades;
using System.Linq.Expressions;

namespace APICalculos.Application.Interfaces
{
    public interface IServiceCategoriesRepository
    {
        Task<IEnumerable<ServiceCategorie>> GetAllAsync(int storeId, string? search);
        Task<ServiceCategorie?> GetByIdAsync(int id, int storeId);
        Task AddAsync(ServiceCategorie serviceCategorie);
        void Update(ServiceCategorie serviceCategorie);
        void Remove(ServiceCategorie serviceCategorie);
        Task<bool> ExistsByNameAsync(string name, int storeId);
    }
}
