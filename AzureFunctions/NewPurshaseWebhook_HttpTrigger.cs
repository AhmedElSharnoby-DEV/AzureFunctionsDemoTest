using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions;

public class NewPurshaseWebhook_HttpTrigger
{
    private readonly ILogger<NewPurshaseWebhook_HttpTrigger> _logger;

    public NewPurshaseWebhook_HttpTrigger(ILogger<NewPurshaseWebhook_HttpTrigger> logger)
    {
        _logger = logger;
    }

    [Function("NewPurshaseWebhook")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        return new OkObjectResult("Welcome to Azure Functions!");
    }
}