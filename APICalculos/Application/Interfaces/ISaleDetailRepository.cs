using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface ISaleDetailRepository
    {
        Task<IEnumerable<SaleDetail>> GetAllAsync(int storeId);
        Task<SaleDetail> GetByIdAsync(int id, int storeId);
        Task<List<SaleDetail>> GetBySaleIdAsync(int saleId, int storeId);

        Task AddAsync(SaleDetail saleDetail);
        void Update(SaleDetail saleDetail);
        void Remove(SaleDetail saleDetail);
    }
}
