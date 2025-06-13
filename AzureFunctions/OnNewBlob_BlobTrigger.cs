using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions;

public class OnNewBlob_BlobTrigger
{
    private readonly ILogger<OnNewBlob_BlobTrigger> _logger;

    public OnNewBlob_BlobTrigger(ILogger<OnNewBlob_BlobTrigger> logger)
    {
        _logger = logger;
    }

    [Function(nameof(OnNewBlob_BlobTrigger))]
    public async Task Run([BlobTrigger("samples-workitems/{name}", Connection = "")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        _logger.LogInformation("C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}", name, content);
    }
}