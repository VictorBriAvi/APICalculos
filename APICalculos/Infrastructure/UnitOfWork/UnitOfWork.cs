using APICalculos.Application.Interfaces;
using APICalculos.Infrastructure.Data;

namespace APICalculos.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;
        public IClientRepository Clients { get; }
        public IEmployeeRepository Employees { get; }
        public IPaymentTypeRepository PaymentType { get; }
        public IServiceCategoriesRepository ServiceCategories { get; }
        public IExpenseTypeRepository ExpenseType { get; }
        public IServiceTypeRepository ServiceTypes { get; }
        public UnitOfWork(MyDbContext context, IClientRepository clientRepository, IEmployeeRepository employees, IPaymentTypeRepository paymentType, IServiceCategoriesRepository serviceCategories, IExpenseTypeRepository expenseType, IServiceTypeRepository serviceType)
        {
            _context = context;
            Clients = clientRepository;
            Employees = employees;
            PaymentType = paymentType;
            ServiceCategories = serviceCategories;
            ExpenseType = expenseType;
            ServiceTypes = serviceType;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
