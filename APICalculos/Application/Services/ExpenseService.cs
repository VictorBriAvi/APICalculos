using APICalculos.Application.DTOs.Employee;
using APICalculos.Application.DTOs.Expense;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Application.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ExpenseService(
            IExpensesRepository expensesRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _expensesRepository = expensesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ExpenseDTO>> GetAllExpenseAsync(
            int storeId,
            string? search,
            int? expenseTypeId,
            int? paymentTypeId,
            DateTime? fromDate,
            DateTime? toDate
        )
        {
            if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
                throw new ArgumentException("La fecha desde no puede ser mayor a la fecha hasta.");

            var expenses = await _expensesRepository.GetAllAsync(
                storeId,
                search,
                expenseTypeId,
                paymentTypeId,
                fromDate,
                toDate
            );

            return _mapper.Map<List<ExpenseDTO>>(expenses);
        }

        public async Task<ExpenseDTO?> GetExpenseForIdAsync(int id, int storeId)
        {
            var expense =
                await _expensesRepository.GetByIdAsync(id, storeId);

            if (expense == null)
                return null;

            return _mapper.Map<ExpenseDTO>(expense);
        }

        public async Task<ExpenseDTO> AddExpensesAsync(
            int storeId,
            ExpenseCreationDTO dto)
        {
            if (dto.ExpenseDate == default)
                throw new ArgumentException("La fecha del gasto es obligatoria.");

            if (dto.ExpenseDate > DateTime.UtcNow)
                throw new ArgumentException("La fecha no puede ser futura.");

            var expense = _mapper.Map<Expenses>(dto);

            expense.StoreId = storeId;

            await _unitOfWork.Expenses.AddAsync(expense);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ExpenseDTO>(expense);
        }

        public async Task UpdateExpenseAsync(
            int id,
            int storeId,
            ExpenseCreationDTO dto)
        {
            var expenseDB =
                await _expensesRepository.GetByIdAsync(id, storeId);

            if (expenseDB == null)
                throw new KeyNotFoundException("Gasto no encontrado");

            if (!string.IsNullOrWhiteSpace(dto.Description))
                expenseDB.Description = dto.Description;

            if (dto.Price > 0)
                expenseDB.Price = dto.Price;

            if (dto.ExpenseTypeId != 0)
            {
                expenseDB.ExpenseType = null;
                expenseDB.ExpenseTypeId = dto.ExpenseTypeId;
            }

            if (dto.PaymentTypeId != 0)
            {
                expenseDB.PaymentType = null;
                expenseDB.PaymentTypeId = dto.PaymentTypeId;
            }

            if (dto.ExpenseDate != default)
                expenseDB.ExpenseDate = dto.ExpenseDate;

            _expensesRepository.Update(expenseDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(int id, int storeId)
        {
            var expenseDB =
                await _expensesRepository.GetByIdAsync(id, storeId);

            if (expenseDB == null)
                throw new KeyNotFoundException("Gasto no encontrado");

            _expensesRepository.Remove(expenseDB);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
