using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IPaymentTypeRepository
    {
        Task<IEnumerable<PaymentTypes>> GetAllAsync(string? search);
        Task<PaymentTypes> GetByIdAsync(int id);
        Task AddAsync(PaymentTypes paymentType);
        void Remove(PaymentTypes paymentType);
        void Update(PaymentTypes paymentType);
        Task<bool> ExistsByNameAsync(string namne);
    }
}
