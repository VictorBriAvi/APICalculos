using APICalculos.Domain.Entidades;

namespace APICalculos.Application.Interfaces
{
    public interface IPaymentTypeRepository
    {
        Task<IEnumerable<PaymentType>> GetAllAsync();
        Task<PaymentType> GetByIdAsync(int id);
        Task AddAsync(PaymentType paymentType);
        void Remove(PaymentType paymentType);
        void Update(PaymentType paymentType);
        Task<bool> ExistsByNameAsync(string namne);
    }
}
