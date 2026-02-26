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
        public ICustomerHistoryRepository CustomerHistory { get; }
        public IExpensesRepository Expenses { get; }
        public ISaleDetailRepository SaleDetail { get; }
        public ISaleRepository Sale { get; }
        public IStoreRepository Stores { get; }
        public IUserRepository Users { get; }


        public UnitOfWork(MyDbContext context, IClientRepository clientRepository, IEmployeeRepository employees, IPaymentTypeRepository paymentType, IServiceCategoriesRepository serviceCategories, IExpenseTypeRepository expenseType, IServiceTypeRepository serviceType, ICustomerHistoryRepository customerHistory, IExpensesRepository expenses, ISaleDetailRepository saleDetail, ISaleRepository sale, IStoreRepository storeRepository, IUserRepository userRepository)
        {
            _context = context;
            Clients = clientRepository;
            Employees = employees;
            PaymentType = paymentType;
            ServiceCategories = serviceCategories;
            ExpenseType = expenseType;
            ServiceTypes = serviceType;
            CustomerHistory = customerHistory;
            Expenses = expenses;
            SaleDetail = saleDetail;
            Sale = sale;
            Stores = storeRepository;
            Users = userRepository;

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
