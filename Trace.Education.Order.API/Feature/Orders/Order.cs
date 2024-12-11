namespace Trace.Education.Order.API.Feature.Orders {
    public class Order {
        public int Id { get; set; }
        public string OrderCode { get; set; } = default!;
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> Items { get; set; } =default!;
    }

    public enum OrderStatus : byte {
        Success =1,
        Failed =0
    }

    public class OrderItem {
        public int Id { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = default!;
    }
}
