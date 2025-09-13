using APICalculos.Domain.Entidades;

namespace APICalculos.Application.DTOs
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string NameExpenseType { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ExpensesDateStr => ExpenseDate.ToString("dd-MM-yyyy");
        public int ExpenseTypeId { get; set; }

    }
}
