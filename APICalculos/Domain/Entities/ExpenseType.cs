namespace APICalculos.Domain.Entidades
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Expense> Expense { get; set; }



    }
}
