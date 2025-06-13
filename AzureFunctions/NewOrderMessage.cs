using Newtonsoft.Json;

namespace AzureFunctions
{
    public class NewOrderMessage
    {
        public string id { get; set; }
        public int OrderId { get; set; }
        public string OrderName { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
    }
}
