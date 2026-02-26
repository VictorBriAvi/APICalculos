namespace APICalculos.Application.DTOs.Expense
{
    public class ExpenseCreationDTO
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ExpenseTypeId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int PaymentTypeId { get; set; }

    }
}
