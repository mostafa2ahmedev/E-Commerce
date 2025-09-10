namespace E_Commerce.Domain.Entities.Orders
{
    public class Order : BaseAuditableEntity<int>
    {
        public required string BuyerEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;


        // Navigational Property for owned entity (same table)(same request)
        public  required Address ShippingAddress  { get; set; }
        public int? DeliveryMethodId { get; set; }
        public virtual  DeliveryMethod? DeliveryMethod { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }

        //[NotMapped]
        //public decimal Total => SubTotal + DeliveryMethod?.Cost ?? 0;

        public decimal GetTotal()=> SubTotal + (DeliveryMethod?.Cost ?? 0);

        public string PaymentIntentId { get; set; } = "";
    }
}
