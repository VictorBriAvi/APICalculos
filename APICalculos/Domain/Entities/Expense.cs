namespace APICalculos.Domain.Entidades
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpenseDate { get; set; }

        public int ExpenseTypeId { get; set; }
        public ExpenseType ExpenseType { get; set; }


        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}