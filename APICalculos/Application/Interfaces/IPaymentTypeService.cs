using APICalculos.Application.DTOs;

namespace APICalculos.Application.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<List<PaymentTypeDTO>> GetAllPaymenteTypesAsync();
        Task<PaymentTypeDTO> GetPaymentTypeForId(int id);
        Task<PaymentTypeDTO> AddPaymenteTypeAsync(PaymentTypeCreationDTO paymentTypeCreationDTO);
        Task UpdatePaymentTypeAsync(int id, PaymentTypeCreationDTO paymentTypeCreationDTO);
        Task DeletePaymentTypeAsync(int id);
    }
}
