using APICalculos.Domain.Entities;

namespace APICalculos.Domain.Entidades
{
    public class ExpenseType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Expenses> Expenses { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }



    }
}
