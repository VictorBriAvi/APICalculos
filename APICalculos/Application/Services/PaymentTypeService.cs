using APICalculos.Application.DTOs.PaymentType;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Application.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentTypeService(
            IPaymentTypeRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<PaymentTypeDTO>> GetAllPaymentTypeAsync(int storeId, string? search)
        {
            var entities = await _repository.GetAllAsync(storeId, search);
            return _mapper.Map<List<PaymentTypeDTO>>(entities);
        }

        public async Task<PaymentTypeDTO?> GetPaymentTypeForId(int id, int storeId)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);
            return entity == null ? null : _mapper.Map<PaymentTypeDTO>(entity);
        }

        public async Task<PaymentTypeDTO> AddPaymenteTypeAsync(int storeId, PaymentTypeCreationDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("El nombre no puede estar vacío");

            var exists = await _repository.ExistsByNameAsync(dto.Name, storeId);
            if (exists)
                throw new InvalidOperationException("Ya existe un tipo de pago con ese nombre");

            var entity = _mapper.Map<PaymentTypes>(dto);
            entity.StoreId = storeId;

            await _repository.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PaymentTypeDTO>(entity);
        }

        public async Task UpdatePaymentTypeAsync(int id, int storeId, PaymentTypeCreationDTO dto)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);

            if (entity == null)
                throw new KeyNotFoundException("Tipo de pago no encontrado");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                entity.Name = dto.Name;

            _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePaymentTypeAsync(int id, int storeId)
        {
            var entity = await _repository.GetByIdAsync(id, storeId);

            if (entity == null)
                throw new KeyNotFoundException("Tipo de pago no encontrado");

            try
            {
                _repository.Remove(entity);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                throw new InvalidOperationException(
                    "No se puede eliminar este tipo de pago porque está asociado a una venta.");
            }
        }
    }
}
