using APICalculos.Application.DTOs.Reports;
using APICalculos.Application.Interfaces;
using APICalculos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace APICalculos.Infrastructure.Repositories.Reports
{
    public class FinancialReportRepository : IFinancialReportRepository
    {
        private readonly MyDbContext _dbContext;

        public FinancialReportRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<FinancialSummaryDTO> GetFinancialSummaryAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            // 🔹 Query base de ventas
            var salesQuery = _dbContext.SaleDetails
                .Include(sd => sd.Sale)
                .Include(sd => sd.Employee)
                .Where(sd => !sd.IsDeleted && !sd.Sale.IsDeleted);

            if (fromDate.HasValue && toDate.HasValue)
            {
                salesQuery = salesQuery.Where(sd => sd.Sale.DateSale >= fromDate && sd.Sale.DateSale <= toDate);
            }

            // 🔹 Total de ventas (suma bruta)
            var totalVentas = await salesQuery.SumAsync(sd =>
                (sd.UnitPrice + sd.AdditionalCharge) * (1 - (sd.DiscountPercent / 100m))
            );

            // 🔹 Total a pagar a empleados
            var totalPagosColaboradores = await salesQuery.SumAsync(sd =>
                ((sd.UnitPrice + sd.AdditionalCharge) * (1 - (sd.DiscountPercent / 100m)))
                * (sd.Employee.PaymentPercentage / 100m)
            );

            // 🔹 Total de gastos
            var expensesQuery = _dbContext.Expenses.AsQueryable();
            if (fromDate.HasValue && toDate.HasValue)
            {
                expensesQuery = expensesQuery.Where(e => e.ExpenseDate >= fromDate && e.ExpenseDate <= toDate);
            }

            var totalGastos = await expensesQuery.SumAsync(e => e.Price);

            // 🔹 DTO Final
            return new FinancialSummaryDTO
            {
                TotalVentas = totalVentas,
                TotalPagosColaboradores = totalPagosColaboradores,
                TotalGastos = totalGastos
            };
        }

        public async Task<IEnumerable<DailyFinancialDTO>> GetDailyFinancialSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            // 🔹 Query de ventas agrupadas por fecha
            var salesByDate = await _dbContext.SaleDetails
                .Include(sd => sd.Sale)
                .Where(sd => !sd.IsDeleted
                          && !sd.Sale.IsDeleted
                          && sd.Sale.DateSale >= fromDate
                          && sd.Sale.DateSale <= toDate)
                .GroupBy(sd => sd.Sale.DateSale.Date)
                .Select(g => new
                {
                    Fecha = g.Key,
                    TotalVentas = g.Sum(sd =>
                        (sd.UnitPrice + sd.AdditionalCharge) * (1 - (sd.DiscountPercent / 100m))
                    )
                })
                .ToListAsync();

            // 🔹 Query de gastos agrupados por fecha
            var expensesByDate = await _dbContext.Expenses
                .Where(e => e.ExpenseDate >= fromDate && e.ExpenseDate <= toDate)
                .GroupBy(e => e.ExpenseDate.Date)
                .Select(g => new
                {
                    Fecha = g.Key,
                    TotalGastos = g.Sum(e => e.Price)
                })
                .ToListAsync();

            // 🔹 Combinar ambas listas por fecha (tipo "full outer join" manual)
            var allDates = salesByDate.Select(s => s.Fecha)
                .Union(expensesByDate.Select(e => e.Fecha))
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            var result = allDates.Select(date => new DailyFinancialDTO
            {
                Fecha = date,
                TotalVentas = salesByDate.FirstOrDefault(s => s.Fecha == date)?.TotalVentas ?? 0,
                TotalGastos = expensesByDate.FirstOrDefault(e => e.Fecha == date)?.TotalGastos ?? 0,
            });

            return result;
        }

        public async Task<IEnumerable<EmployeeSalesSummaryDTO>> GetEmployeeSalesSummaryAsync(DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.SaleDetails
                .Where(sd => !sd.IsDeleted
                    && !sd.Sale.IsDeleted
                    && sd.Sale.DateSale >= fromDate
                    && sd.Sale.DateSale <= toDate)
                .GroupBy(sd => new
                {
                    sd.Employee.Name,
                    sd.Employee.PaymentPercentage
                })
                .Select(g => new EmployeeSalesSummaryDTO
                {
                    Empleado = g.Key.Name,
                    TotalVentas = g.Sum(sd =>
                        (sd.UnitPrice + sd.AdditionalCharge)
                        * (1 - (sd.DiscountPercent / 100m))),
                    PorcentajePago = g.Key.PaymentPercentage,
                    TotalAPagar = g.Sum(sd =>
                        ((sd.UnitPrice + sd.AdditionalCharge)
                        * (1 - (sd.DiscountPercent / 100m)))
                        * (g.Key.PaymentPercentage / 100m))
                })
                .OrderBy(x => x.Empleado)
                .ToListAsync();
        }

        public async Task<List<SalesByPaymentReportDTO>> GetSalesReportByPaymentTypeAsync(DateTime startDate, DateTime endDate)
        {
            var result = await (from sp in _dbContext.SalePayments
                                join s in _dbContext.Sales on sp.SaleId equals s.Id
                                join pt in _dbContext.PaymentTypes on sp.PaymentTypeId equals pt.Id
                                where s.DateSale >= startDate && s.DateSale <= endDate
                                group new { sp, s, pt } by new { Fecha = s.DateSale.Date, pt.Name } into g
                                orderby g.Key.Fecha, g.Key.Name
                                select new SalesByPaymentReportDTO
                                {
                                    Fecha = g.Key.Fecha,
                                    MedioDePago = g.Key.Name,
                                    TotalRecaudado = g.Sum(x => x.sp.AmountPaid)
                                }).ToListAsync();

            return result;
        }


        public async Task<List<SalesByPaymentSummaryDTO>> GetSalesSummaryByPaymentTypeAsync(DateTime startDate, DateTime endDate)
        {
            return await (from sp in _dbContext.SalePayments
                          join s in _dbContext.Sales on sp.SaleId equals s.Id
                          join pt in _dbContext.PaymentTypes on sp.PaymentTypeId equals pt.Id
                          where s.DateSale >= startDate && s.DateSale <= endDate
                          group sp by pt.Name into g
                          orderby g.Key
                          select new SalesByPaymentSummaryDTO
                          {
                              MedioDePago = g.Key,
                              TotalRecaudado = g.Sum(x => x.AmountPaid),
                              CantidadOperaciones = g.Count()
                          }).ToListAsync();
        }


    }
}
