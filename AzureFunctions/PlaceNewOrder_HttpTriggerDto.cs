using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace AzureFunctions
{
    public class PlaceNewOrder_HttpTriggerDto
    {
        [QueueOutput("order-created-queue", Connection = "AzureWebJobsStorage")]
        public NewOrderMessage Order { get; set; } = null!;
        //public HttpResponseData Response { get; set; } = null!;
        public string BlobName { get; set; } = null!;

        //[BlobOutput("blob-order-created/{rand-guid}.json", Connection = "AzureWebJobsStorage")]
        public string Content { get; set; } = null!;

        [CosmosDBOutput("azurefuncs", "orders", Connection = "CosmosDBConnection")]
        public object NewOrderMessage { get; set; } = null!;
    }

}
