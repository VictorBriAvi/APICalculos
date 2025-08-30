using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IServiceTypeRepository
    {
        Task<IEnumerable<ServiceType>> GetAllAsync();
        Task<ServiceType> GetByIdAsync(int Id);
        Task AddAsync(ServiceType serviceType);
        void Update(ServiceType serviceType);
        void Remove(ServiceType serviceType);
        Task<bool> ExistsByNameAsync(string name);
    }
}
