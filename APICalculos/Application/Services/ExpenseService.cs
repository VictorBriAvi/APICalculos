using APICalculos.Application.DTOs;
using APICalculos.Application.Interfaces;
using APICalculos.Domain.Entidades;
using APICalculos.Infrastructure.UnitOfWork;
using AutoMapper;

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

        public async Task<List<ExpenseDTO>> GetAllExpenseAsync()
        {
            var expenses = await _expensesRepository.GetAllAsync();
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

        public async Task<ExpenseDTO> AddExpensesAsync(ExpenseCreationDTO expenseCreationDTO)
        {
           var expense = _mapper.Map<Expense>(expenseCreationDTO);

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

            if (expenseCreationDTO.ExpenseTypeId > 0)
                expenseDB.ExpenseTypeId = expenseCreationDTO.ExpenseTypeId;


            _expensesRepository.Update(expenseDB);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var expenseDB = await _expensesRepository.GetByIdAsync(id);

            if (expenseDB == null)
            {
                throw new KeyNotFoundException("Gasto no encontrado");
            }

            _expensesRepository.Remove(expenseDB);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
