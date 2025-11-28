using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllAsync();
        Task<IEnumerable<Sale>> GetByTodayAsync();
        Task<Sale> GetByIdAsync(int id);
        Task AddAsync(Sale sale);
        void Update(Sale sale); 
        void Remove(Sale sale);
    }
}
