using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AzureFunctions;

public class GetSpecifiedBlob_HttpTrigger
{
    private readonly ILogger<GetSpecifiedBlob_HttpTrigger> _logger;

    public GetSpecifiedBlob_HttpTrigger(ILogger<GetSpecifiedBlob_HttpTrigger> logger)
    {
        _logger = logger;
    }

    [Function("GetSpecifiedBlob_HttpTrigger")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route ="get/{orderId:long}")] HttpRequestData req,
        [BlobInput("blob-order-created/{orderId}.json", Connection = "AzureWebJobsStorage")] BlobClient blobClient)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var blobResult = await blobClient.DownloadContentAsync();
        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        var blobData = JsonSerializer.Deserialize<OrderCreatedDto>(blobResult.Value.Content);
        await response.WriteAsJsonAsync(blobData);
        // return response of azure function
        return response;
    }
}