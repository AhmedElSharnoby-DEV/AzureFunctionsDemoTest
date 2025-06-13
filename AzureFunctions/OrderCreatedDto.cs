namespace AzureFunctions
{
    public class OrderCreatedDto : IOrderCreatedDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ShippingLocation { get; set; } = null!;
        public DateTime DueDate { get; set; }
    }
}
