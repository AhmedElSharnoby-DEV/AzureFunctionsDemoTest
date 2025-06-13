namespace AzureFunctions
{
    public class NullOrderCreatedDto : IOrderCreatedDto
    {
        public int Id => 0;
        public string Name => "No Order Created";
        public string ShippingLocation => "No Location Selected";
        public DateTime DueDate => DateTime.MinValue;
    }
}
