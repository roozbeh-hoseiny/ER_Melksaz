using ER.Melksaz.BuildingBlocks.Domain;
using ER.Melksaz.PrimitiveResults;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ER.Melksaz.BuildingBlocks.Api;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private static readonly PrimitiveError BadRequestError = PrimitiveError.Create(DomainErrorCodes.BadRequest_ErrorCode, "Bad request");

    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IResultHandler _resultHandler;

    public GlobalExceptionHandler(
        IServiceScopeFactory serviceScopeFactory,
        IResultHandler resultHandler,
        ILogger<GlobalExceptionHandler> logger)
    {
        this._serviceScopeFactory = serviceScopeFactory;
        this._resultHandler = resultHandler;
        this._logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        using var scope = this._serviceScopeFactory.CreateScope();
        var resultHandler = scope.ServiceProvider.GetRequiredService<IResultHandler>();

        PrimitiveError error;
        PrimitiveResult<bool> result;

        if (exception is Microsoft.AspNetCore.Http.BadHttpRequestException)
        {
            var badRequestMessage = exception.InnerException?.Message ?? "Bad request";
            error = PrimitiveError.Create(DomainErrorCodes.BadRequest_ErrorCode, badRequestMessage);
            result = PrimitiveResult.Failure<bool>(error);
        }
        else
        {
            error = PrimitiveError.Create(DomainErrorCodes.UnhandledException_ErrorCode, exception.Message);
            result = PrimitiveResult.InternalFailure<bool>(error);
        }

        var handlerResult = this._resultHandler.Handle(result);

        await handlerResult.ExecuteAsync(httpContext).ConfigureAwait(false);

        return true;
    }
}