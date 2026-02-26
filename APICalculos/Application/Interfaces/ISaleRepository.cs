using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface ISaleRepository
    {
        Task<IEnumerable<Sale>> GetAllAsync(int storeId);

        Task<List<Sale>> GetFilteredAsync(
                    int storeId,
                    DateTime? fromDate = null,
                    DateTime? toDate = null,
                    int? clientId = null,
                    int? paymentTypeId = null,
                    int? employeeId = null,
                    int? serviceTypeId = null);

        Task<Sale?> GetByIdAsync(int id, int storeId);

        Task AddAsync(Sale sale);

        void Update(Sale sale);

        void Remove(Sale sale);
    }
}
