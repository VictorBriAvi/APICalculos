using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface ISaleDetailRepository
    {
        Task<IEnumerable<SaleDetail>> GetAllAsync();
        Task<SaleDetail> GetByIdAsync(int id);
        Task<List<SaleDetail>> GetBySaleIdAsync(int saleId);

        Task AddAsync(SaleDetail saleDetail);
        void Update(SaleDetail saleDetail);
        void Remove(SaleDetail saleDetail);
    }
}
