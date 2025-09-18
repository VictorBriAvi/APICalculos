using APICalculos.Application.Interfaces;

namespace APICalculos.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepository Clients { get; }
        IEmployeeRepository Employees { get; }
        IPaymentTypeRepository PaymentType { get; } 
        IServiceCategoriesRepository ServiceCategories { get; }
        IExpenseTypeRepository ExpenseType { get; }
        IServiceTypeRepository ServiceTypes { get; }
        ICustomerHistoryRepository CustomerHistory { get; }
        IExpensesRepository Expenses { get; }
        ISaleDetailRepository SaleDetail { get; }
        ISaleRepository Sale { get; }
        Task<int> SaveChangesAsync();
    }
}
