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

        public async Task<IEnumerable<DailyFinancialDTO>> GetDailyFinancialSummaryAsync(
            DateTime fromDate,
            DateTime toDate)
        {
            // 🔹 Ventas + pagos a colaboradores por día
            var salesByDate = await _dbContext.SaleDetails
                .Include(sd => sd.Sale)
                .Include(sd => sd.Employee)
                .Where(sd => !sd.IsDeleted
                          && !sd.Sale.IsDeleted
                          && sd.Sale.DateSale >= fromDate
                          && sd.Sale.DateSale <= toDate)
                .GroupBy(sd => sd.Sale.DateSale.Date)
                .Select(g => new
                {
                    Fecha = g.Key,

                    TotalVentas = g.Sum(sd =>
                        (sd.UnitPrice + sd.AdditionalCharge) *
                        (1 - (sd.DiscountPercent / 100m))
                    ),

                    TotalPagosColaboradores = g.Sum(sd =>
                        ((sd.UnitPrice + sd.AdditionalCharge) *
                        (1 - (sd.DiscountPercent / 100m))) *
                        (sd.Employee.PaymentPercentage / 100m)
                    )
                })
                .ToListAsync();

            // 🔹 Gastos por día
            var expensesByDate = await _dbContext.Expenses
                .Where(e => e.ExpenseDate >= fromDate && e.ExpenseDate <= toDate)
                .GroupBy(e => e.ExpenseDate.Date)
                .Select(g => new
                {
                    Fecha = g.Key,
                    TotalGastos = g.Sum(e => e.Price)
                })
                .ToListAsync();

            // 🔹 Todas las fechas involucradas
            var allDates = salesByDate.Select(s => s.Fecha)
                .Union(expensesByDate.Select(e => e.Fecha))
                .Distinct()
                .OrderBy(d => d)
                .ToList();

            // 🔹 DTO final
            var result = allDates.Select(date =>
            {
                var sales = salesByDate.FirstOrDefault(s => s.Fecha == date);
                var expenses = expensesByDate.FirstOrDefault(e => e.Fecha == date);

                var totalVentas = sales?.TotalVentas ?? 0;
                var totalPagosColaboradores = sales?.TotalPagosColaboradores ?? 0;
                var totalGastos = expenses?.TotalGastos ?? 0;

                return new DailyFinancialDTO
                {
                    Fecha = date,
                    TotalVentas = totalVentas,
                    TotalGastos = totalGastos,
                    TotalGanancia = totalVentas
                                     - totalPagosColaboradores
                                     - totalGastos,
                    DiaSemana = date.DayOfWeek
                };
            });

            return result;
        }

        public async Task<IEnumerable<EmployeeSalesSummaryDTO>> GetEmployeeSalesSummaryAsync(
            DateTime fromDate, DateTime toDate)
        {
            return await _dbContext.SaleDetails
                .Where(sd => !sd.IsDeleted
                    && !sd.Sale.IsDeleted
                    && sd.Sale.DateSale >= fromDate
                    && sd.Sale.DateSale <= toDate)
                .GroupBy(sd => new
                {
                    sd.Employee.Id,
                    sd.Employee.Name,
                    sd.Employee.PaymentPercentage
                })
                .Select(g => new EmployeeSalesSummaryDTO
                {
                    EmpleadoId = g.Key.Id,
                    Empleado = g.Key.Name,
                    TotalVentas = g.Sum(sd =>
                        (sd.UnitPrice + sd.AdditionalCharge)
                        * (1 - (sd.DiscountPercent / 100m))),
                    PorcentajePago = g.Key.PaymentPercentage,
                    TotalAPagar = g.Sum(sd =>
                        ((sd.UnitPrice + sd.AdditionalCharge)
                        * (1 - (sd.DiscountPercent / 100m)))
                        * (g.Key.PaymentPercentage / 100m)),

                    TotalServicios = g.Count(),

                    ServiciosRealizados = g
                        .GroupBy(sd => sd.ServiceType.Name)
                        .Select(sg => new ServiceCountDTO
                        {
                            Servicio = sg.Key,
                            Cantidad = sg.Count()
                        })
                        .ToList()
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


        public async Task<List<PaymentTypeBalanceDTO>> GetPaymentTypeBalanceAsync(
            DateTime startDate,
            DateTime endDate)
        {
            // 🔹 Normalización de fechas (incluye el día completo)
            var fromDate = startDate.Date;
            var toDate = endDate.Date.AddDays(1);

            // 🔹 Ventas por medio de pago
            var ventas =
                from sp in _dbContext.SalePayments
                join s in _dbContext.Sales on sp.SaleId equals s.Id
                where !s.IsDeleted
                      && s.DateSale >= fromDate
                      && s.DateSale < toDate
                group sp by sp.PaymentTypeId into g
                select new
                {
                    PaymentTypeId = g.Key,
                    TotalVentas = (decimal?)g.Sum(x => x.AmountPaid)
                };

            // 🔹 Gastos por medio de pago
            var gastos =
                from e in _dbContext.Expenses
                where e.ExpenseDate >= fromDate
                      && e.ExpenseDate < toDate
                group e by e.PaymentTypeId into g
                select new
                {
                    PaymentTypeId = g.Key,
                    TotalGastos = (decimal?)g.Sum(x => x.Price)
                };

            // 🔹 Balance final
            var query =
                from pt in _dbContext.PaymentTypes
                join v in ventas on pt.Id equals v.PaymentTypeId into ventasJoin
                from v in ventasJoin.DefaultIfEmpty()
                join g in gastos on pt.Id equals g.PaymentTypeId into gastosJoin
                from g in gastosJoin.DefaultIfEmpty()
                orderby pt.Name
                select new PaymentTypeBalanceDTO
                {
                    PaymentTypeId = pt.Id,
                    MedioDePago = pt.Name,
                    TotalVentas = v.TotalVentas ?? 0,
                    TotalGastos = g.TotalGastos ?? 0,
                    TotalDisponible =
                        (v.TotalVentas ?? 0) - (g.TotalGastos ?? 0)
                };

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<ExpensesByCategoryDTO>> GetExpensesByCategoryAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {

            var query = _dbContext.Expenses
                .Include(e => e.ExpenseType)
                .AsQueryable();

            if (fromDate.HasValue && toDate.HasValue)
            {
                query = query.Where(e => e.ExpenseDate >= fromDate && e.ExpenseDate <= toDate);
            }

            var result = await query
                .GroupBy(e => e.ExpenseType.Name)
                .Select(g => new ExpensesByCategoryDTO
                {
                    Categoria = g.Key,
                    TotalGasto = g.Sum(e => e.Price)
                })
                .OrderBy(x => x.Categoria)
                .ToListAsync();

            return result;
        }



    }
}
