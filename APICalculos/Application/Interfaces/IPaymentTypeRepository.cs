using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IPaymentTypeRepository
    {
        Task<IEnumerable<PaymentTypes>> GetAllAsync(int storeId, string? search);
        Task<PaymentTypes?> GetByIdAsync(int id, int storeId);
        Task AddAsync(PaymentTypes paymentType);
        void Update(PaymentTypes paymentType);
        void Remove(PaymentTypes paymentType);
        Task<bool> ExistsByNameAsync(string name, int storeId);
    }
}
