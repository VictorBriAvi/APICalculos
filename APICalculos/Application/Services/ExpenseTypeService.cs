using APICalculos.Application.DTOs;
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

        public ExpenseTypeService(IExpenseTypeRepository expenseTypeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ExpenseTypeDTO>> GetAllExpensesTypesAsync()
        {
            var expensesTypes = await _expenseTypeRepository.GetAllAsync();
            return _mapper.Map<List<ExpenseTypeDTO>>(expensesTypes);
        }

        public async Task<ExpenseTypeDTO> GetExpenseTypeForId(int id)
        {
            var expenseType = await _expenseTypeRepository.GetByIdAsync(id);
            if (expenseType == null)
            {
                return null;
            }
            return _mapper.Map<ExpenseTypeDTO>(expenseType);
        }

        public async Task<ExpenseTypeDTO> AddExpenseTypeAsync(ExpenseTypeCreationDTO expenseTypeCreationDTO)
        {
            if (string.IsNullOrWhiteSpace(expenseTypeCreationDTO.Name))
            {
                throw new ArgumentException("El nommbre del tipo de gasto no puede estar vacio");
            }

            var existsName = await _expenseTypeRepository.ExistsByNameAsync(expenseTypeCreationDTO.Name);
            if (existsName)
            {
                throw new InvalidOperationException("El nombre del tipo de gasto ya existe");
            }

            var expenseType = _mapper.Map<ExpenseType>(expenseTypeCreationDTO);

            await _unitOfWork.ExpenseType.AddAsync(expenseType);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ExpenseTypeDTO>(expenseType);
        }

        public async Task UpdateExpenseTypeAsync (int id, ExpenseTypeCreationDTO expenseTypeCreationDTO)
        {
            var expensesTypeDB = await _expenseTypeRepository.GetByIdAsync(id);

            if (expensesTypeDB == null)
            {
                throw new KeyNotFoundException("Tipo de gasto no encontrado");
            }

            if (!string.IsNullOrWhiteSpace(expenseTypeCreationDTO.Name))
            {
                expensesTypeDB.Name = expenseTypeCreationDTO.Name;
            }

            _expenseTypeRepository.Update(expensesTypeDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteExpenseTypeAsync(int id)
        {
            var expenseTypeDB = await _expenseTypeRepository.GetByIdAsync(id);
            if (expenseTypeDB == null)
                throw new KeyNotFoundException("Tipo de pago no encontrado");

            try
            {
                _expenseTypeRepository.Remove(expenseTypeDB);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                throw new InvalidOperationException("No se puede eliminar esta categoria de gasto porque está asociado a un gasto.");
            }
        }

    }
}
