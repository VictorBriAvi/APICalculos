using APICalculos.Domain.Entidades;
using System.Text.Json.Serialization;

namespace APICalculos.Application.DTOs
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string NameExpenseType { get; set; }
        [JsonIgnore]
        public DateTime ExpenseDate { get; set; }
        public string ExpensesDateStr => ExpenseDate.ToString("dd-MM-yyyy");
        public int ExpenseTypeId { get; set; }

    }
}
