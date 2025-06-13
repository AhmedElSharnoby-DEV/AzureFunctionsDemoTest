namespace AzureFunctions
{
    public interface IOrderCreatedDto
    {
        public int Id { get; }
        public string Name { get;}
        public string ShippingLocation { get;}
        public DateTime DueDate { get;}
    }
}
