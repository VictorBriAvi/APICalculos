using APICalculos.Application.DTOs.ExpenseType;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.Repositories;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Application.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseTypeService(
            IExpenseTypeRepository expenseTypeRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ExpenseTypeDTO>> GetAllExpensesTypesAsync(
            int storeId,
            string? search)
        {
            var types = await _expenseTypeRepository
                .GetAllAsync(storeId, search);

            return _mapper.Map<List<ExpenseTypeDTO>>(types);
        }

        public async Task<ExpenseTypeDTO?> GetExpenseTypeForId(int id, int storeId)
        {
            var expenseType =
                await _expenseTypeRepository.GetByIdAsync(id, storeId);

            if (expenseType == null)
                return null;

            return _mapper.Map<ExpenseTypeDTO>(expenseType);
        }

        public async Task<ExpenseTypeDTO> AddExpenseTypeAsync(
            int storeId,
            ExpenseTypeCreationDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("El nombre no puede estar vacío");

            var exists = await _expenseTypeRepository
                .ExistsByNameAsync(dto.Name, storeId);

            if (exists)
                throw new InvalidOperationException("El tipo de gasto ya existe");

            var expenseType = _mapper.Map<ExpenseType>(dto);

            expenseType.StoreId = storeId;

            await _unitOfWork.ExpenseType.AddAsync(expenseType);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ExpenseTypeDTO>(expenseType);
        }

        public async Task UpdateExpenseTypeAsync(
            int id,
            int storeId,
            ExpenseTypeCreationDTO dto)
        {
            var expenseTypeDB =
                await _expenseTypeRepository.GetByIdAsync(id, storeId);

            if (expenseTypeDB == null)
                throw new KeyNotFoundException("Tipo de gasto no encontrado");

            if (!string.IsNullOrWhiteSpace(dto.Name))
                expenseTypeDB.Name = dto.Name;

            _expenseTypeRepository.Update(expenseTypeDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteExpenseTypeAsync(int id, int storeId)
        {
            var expenseTypeDB =
                await _expenseTypeRepository.GetByIdAsync(id, storeId);

            if (expenseTypeDB == null)
                throw new KeyNotFoundException("Tipo de gasto no encontrado");

            _expenseTypeRepository.Remove(expenseTypeDB);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
