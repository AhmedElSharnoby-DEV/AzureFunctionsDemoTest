using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AzureFunctions;

public class PlaceNewOrder_HttpTrigger
{
    private readonly ILogger<PlaceNewOrder_HttpTrigger> _logger;
    private readonly IConfiguration _config;

    public PlaceNewOrder_HttpTrigger(ILogger<PlaceNewOrder_HttpTrigger> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
    }

    [Function("PlaceNewOrder_HttpTrigger")]
    public async Task<PlaceNewOrder_HttpTriggerDto> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route ="create-order")] 
    HttpRequestData req)
    {
        try
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var order = await req.ReadFromJsonAsync<OrderCreatedDto>();
            order ??= new OrderCreatedDto();

            //response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            //response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            var responseData = new
            {
                Message = $"The order {order.Name} has created successfully, and due date {order.DueDate}",
                OrderId = order.Id,
                BlobPath = $"blob-order-created/{order.Id}.json",
                BlobName = $"{order.Id}.json"
            };
            NewOrderMessage orderMessage = new NewOrderMessage
            {
                id = Guid.NewGuid().ToString(),
                OrderId = order.Id,
                OrderName = order.Name,
                CreatedDate = DateTime.Now
            };


            //await response.WriteStringAsync($"The order {order.Name} has created successfully, and due date {order.DueDate}");
            //await response.WriteAsJsonAsync(responseData);

            var content = JsonSerializer.Serialize(order);

            #region Create Blob With OrderId
            var storageConnectionString = _config["AzureWebJobsStorage"];
            var blobFileName = $"{order.Id}.json";
            // Upload to Blob Storage
            var blobServiceClient = new BlobServiceClient(storageConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient("blob-order-created");
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(blobFileName);
            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            await blobClient.UploadAsync(stream, overwrite: true);

            #endregion

            HttpResponseData response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            await response.WriteAsJsonAsync(responseData);

            return new PlaceNewOrder_HttpTriggerDto
            {
                BlobName = $"{order.Id.ToString()}.json",
                Order = orderMessage,
                Content = content,
                NewOrderMessage = orderMessage
            };

        }
        catch (Exception ex)
        {
            var exception = ex;
            Console.WriteLine(ex);
            return new PlaceNewOrder_HttpTriggerDto();
        }
    }
}