using APICalculos.Domain.Entities;

namespace APICalculos.Domain.Entidades
{
    public class Expenses
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpenseDate { get; set; }

        public int ExpenseTypeId { get; set; }
        public ExpenseType ExpenseType { get; set; }


        public int PaymentTypeId { get; set; }
        public PaymentTypes PaymentType { get; set; }

        public int? StoreId { get; set; }
        public Store? Store { get; set; }

    }
}