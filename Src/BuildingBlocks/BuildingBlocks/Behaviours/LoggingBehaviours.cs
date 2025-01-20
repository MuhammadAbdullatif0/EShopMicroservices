using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace BuildingBlocks.Behaviours;

public class LoggingBehaviours<TRequest, TResponse>(ILogger<LoggingBehaviours<TRequest,TResponse>> logger) :
    IPipelineBehavior<TRequest, TRequest>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TRequest> Handle(TRequest request, RequestHandlerDelegate<TRequest> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handel Request={Request} - Ressponse={Response} - RequestData={RequestData}", 
            typeof(TRequest).Name , typeof(TResponse).Name , request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;
        if(timeTaken.Seconds > 3)
        {
            logger.LogWarning("[PERFORMANCE] the request {Request} took {timeTaken}", typeof(TRequest).Name, timeTaken.Seconds);
        }

        logger.LogInformation("[END] Handel Request={Request} - Ressponse={Response} - RequestData={RequestData}",
            typeof(TRequest).Name, typeof(TResponse).Name, request);

        return response;
    }
}

