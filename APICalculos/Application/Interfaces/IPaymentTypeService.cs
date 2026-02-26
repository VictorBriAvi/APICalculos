using APICalculos.Application.DTOs.PaymentType;

namespace APICalculos.Application.Interfaces
{
    public interface IPaymentTypeService
    {
        Task<List<PaymentTypeDTO>> GetAllPaymentTypeAsync(int storeId, string? search);
        Task<PaymentTypeDTO?> GetPaymentTypeForId(int id, int storeId);
        Task<PaymentTypeDTO> AddPaymenteTypeAsync(int storeId, PaymentTypeCreationDTO dto);
        Task UpdatePaymentTypeAsync(int id, int storeId, PaymentTypeCreationDTO dto);
        Task DeletePaymentTypeAsync(int id, int storeId);
    }
}
