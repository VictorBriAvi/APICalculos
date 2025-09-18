namespace APICalculos.Domain.Entidades
{
    public class ServiceType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServiceCategorieId { get; set; }
        public decimal Price { get; set; }
        public ServiceCategorie ServiceCategorie { get; set; }
        public ICollection<SaleDetail> SaleDetail { get; set; }

    
    }
}
                                                                             