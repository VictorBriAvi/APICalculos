using APICalculos.Domain.Entities;

namespace APICalculos.Domain.Entidades
{
    public class Sale
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public DateTime DateSale { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int StoreId { get; set; }

        // Recargo a nivel venta — el mayor % entre todos los medios de pago usados
        public decimal SurchargePercent { get; set; }
        public decimal SurchargeAmount { get; set; }

        // BaseAmount = suma de servicios (sin recargo)
        // TotalAmount = BaseAmount + SurchargeAmount → lo que el cliente pagó
        public decimal BaseAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public Client Client { get; set; }
        public Store Store { get; set; }
        public ICollection<SaleDetail> SaleDetail { get; set; }
        public ICollection<SalePayment> Payments { get; set; }

        public void CalculateTotal()
        {
            BaseAmount = SaleDetail.Sum(d =>
                (d.UnitPrice + d.AdditionalCharge) * (1 - d.DiscountPercent / 100m));

            SurchargeAmount = BaseAmount * (SurchargePercent / 100m);
            TotalAmount = BaseAmount + SurchargeAmount;
        }
    }
}