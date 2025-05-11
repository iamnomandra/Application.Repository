
namespace Chat.Application.Entities 
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public string? CostumerName { get; set; } = "Amit Kumar";
        public string? Email { get; set; } = "iamnomandra@gmail.com";
        public string? Mobile { get; set; } = "+910000000000";
        public double? TotalAmount { get; set; } = 0;
        public string OrderId { get; set; }
        public string TransactionId { get; set; }
    }
}
