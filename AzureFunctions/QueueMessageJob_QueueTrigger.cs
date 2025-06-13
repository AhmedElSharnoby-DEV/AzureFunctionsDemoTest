using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions;

public class QueueMessageJob_QueueTrigger
{
    private readonly ILogger<QueueMessageJob_QueueTrigger> _logger;

    public QueueMessageJob_QueueTrigger(ILogger<QueueMessageJob_QueueTrigger> logger)
    {
        _logger = logger;
    }

    [Function(nameof(QueueMessageJob_QueueTrigger))]
    public void Run([QueueTrigger("order-queue", Connection = "AzureWebJobsStorage")] int message)
    {
        Console.WriteLine($"azure storage queue triggered with docker container {message/*.Body.ToString()*/}.");
        _logger.LogInformation("C# Queue trigger function processed: {messageText}", message/*.MessageText*/);
    }
}