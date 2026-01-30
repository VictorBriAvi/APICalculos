using APICalculos.Application.DTOs;
using APICalculos.Application.DTOs.Employee;
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

        public ExpenseService(IExpensesRepository expensesRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _expensesRepository = expensesRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ExpenseDTO>> GetAllExpenseAsync(
            string? search,
            int? expenseTypeId,
            DateTime? fromDate,
            DateTime? toDate
        )
        {
            if (fromDate.HasValue && toDate.HasValue && fromDate > toDate)
                throw new ArgumentException("La fecha desde no puede ser mayor a la fecha hasta.");

            var expenses = await _expensesRepository.GetAllAsync(
                search,
                expenseTypeId,
                fromDate,
                toDate
            );

            return _mapper.Map<List<ExpenseDTO>>(expenses);
        }


        public async Task<ExpenseDTO> GetExpenseForIdAsync (int id)
        {
            var expense = await _expensesRepository.GetByIdAsync(id);
            if (expense == null)
            {
                return null;
            }
            return _mapper.Map<ExpenseDTO>(expense);
        }

        public async Task<ExpenseDTO> AddExpensesAsync(ExpenseCreationDTO dto)
        {
            if (dto.ExpenseDate == default)
                throw new ArgumentException("La fecha del gasto es obligatoria.");

            if (dto.ExpenseDate > DateTime.UtcNow)
                throw new ArgumentException("La fecha del gasto no puede ser futura.");

            var expense = _mapper.Map<Expenses>(dto);

            await _unitOfWork.Expenses.AddAsync(expense);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ExpenseDTO>(expense);
        }


        public async Task UpdateExpenseAsync (int id, ExpenseCreationDTO expenseCreationDTO)
        {
            var expenseDB = await _expensesRepository.GetByIdAsync(id);
            if (expenseDB == null)
            {
                throw new KeyNotFoundException("Gasto no encontrado");
            }

            if (!string.IsNullOrWhiteSpace(expenseCreationDTO.Description))
                expenseDB.Description = expenseCreationDTO.Description;

            if (!string.IsNullOrWhiteSpace(expenseCreationDTO.Price.ToString()))
                expenseDB.Price = expenseCreationDTO.Price;

            if (expenseCreationDTO.ExpenseTypeId != 0)
            {
                expenseDB.ExpenseType= null;
                expenseDB.ExpenseTypeId = expenseCreationDTO.ExpenseTypeId;
            }

            if (expenseCreationDTO.PaymentTypeId != 0)
            {
                expenseDB.PaymentType= null;
                expenseDB.PaymentTypeId = expenseCreationDTO.PaymentTypeId;
            }

            if (expenseCreationDTO.ExpenseDate != default)
                expenseDB.ExpenseDate = expenseCreationDTO.ExpenseDate;


            _expensesRepository.Update(expenseDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var expenseDB = await _expensesRepository.GetByIdAsync(id);
            if (expenseDB == null)
                throw new KeyNotFoundException("Tipo de pago no encontrado");

            try
            {
                _expensesRepository.Remove(expenseDB);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                throw new InvalidOperationException("No se puede eliminar este gasto porque está asociado a una venta.");
            }
        }
    }
}
