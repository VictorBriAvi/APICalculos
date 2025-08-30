using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IServiceCategoriesRepository
    {
        Task<IEnumerable<ServiceCategorie>> GetAllAsync();
        Task<ServiceCategorie> GetByIdAsync(int id);
        Task AddAsync(ServiceCategorie serviceCategorie);
        void Update(ServiceCategorie serviceCategorie);
        void Remove(ServiceCategorie serviceCategorie);
        Task<bool> ExistsByNameAsync(string name);
    }
}
