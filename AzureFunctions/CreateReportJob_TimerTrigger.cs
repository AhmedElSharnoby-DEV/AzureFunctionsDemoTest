using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctions;

public class CreateReportJob_TimerTrigger
{
    private readonly ILogger _logger;

    public CreateReportJob_TimerTrigger(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<CreateReportJob_TimerTrigger>();
    }

    [Function("DailyReportJob")]
    public void Run([TimerTrigger("0 */40 * * * *", RunOnStartup = true)] TimerInfo myTimer)
    {
        _logger.LogInformation("C# Timer trigger function executed at: {executionTime}", DateTime.Now);
        
        if (myTimer.ScheduleStatus is not null)
        {
            _logger.LogInformation("Next timer schedule at: {nextSchedule}", myTimer.ScheduleStatus.Next);
        }
    }
}