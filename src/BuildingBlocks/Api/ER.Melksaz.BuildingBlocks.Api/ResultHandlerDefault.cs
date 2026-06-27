using ER.Melksaz.BuildingBlocks.Domain;
using ER.Melksaz.PrimitiveResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ER.Melksaz.BuildingBlocks.Api;

public sealed class ResultHandlerDefault : IResultHandler
{
    private readonly ILogger<ResultHandlerDefault> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ResultHandlerDefault(
        ILogger<ResultHandlerDefault> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        this._logger = logger;
        this._httpContextAccessor = httpContextAccessor;
    }
    public IResult Handle<T>(PrimitiveResult<T> result, Func<T, IResult> func)
    {
        if (result.IsSuccess)
            return func(result.Value);

        var httpContext = this._httpContextAccessor.HttpContext!;
        string traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        string path = httpContext.Request.Path;

        // Log all errors for support/dev team
        this._logger.LogError("Request failed at {Path}, TraceId: {TraceId}, Errors: {@Errors}", path, traceId, result.Errors);

        var problemDetails = new ProblemDetails
        {
            Status = GetStatus(result.Error),
            Title = "Request failed",
            Detail = GetMessage(result.Errors),
            Instance = path
        };

        problemDetails.Extensions["traceId"] = traceId;
        problemDetails.Extensions["isSuccess"] = result.IsSuccess;
        problemDetails.Extensions["isFailure"] = result.IsFailure;
        problemDetails.Extensions["errors"] = result.Errors
            .Where(e => !e.Internal)
            .Select(e => new
            {
                e.Code,
                e.Message
            });

        return Results.Problem(
            title: problemDetails.Title,
            detail: problemDetails.Detail,
            statusCode: problemDetails.Status,
            instance: problemDetails.Instance,
            extensions: problemDetails.Extensions
        );
    }
    public IResult Handle<T>(PrimitiveResult<T> result) => this.Handle(result, TypedResults.Ok);

    private static string GetMessage(IReadOnlyList<PrimitiveError> errors)
    {
        var visibleErrors = errors.Where(e => !e.Internal).Select(e => e.Message).ToList();
        return
            visibleErrors.Any()
            ? string.Join(Environment.NewLine, visibleErrors)
            : "An error occurred";
    }
    private static int GetStatus(PrimitiveError error)
    {
        var result = StatusCodes.Status500InternalServerError;

        if (error.Code.Equals(DomainErrorCodes.AccessDenied_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status401Unauthorized; //401

        if (error.Code.Equals(DomainErrorCodes.UnhandledException_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status500InternalServerError; //500

        if (error.Code.Equals(DomainErrorCodes.RpcException_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status503ServiceUnavailable; //503

        if (error.Code.Equals(DomainErrorCodes.InvalidCaptcha_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status412PreconditionFailed; //412

        if (error.Code.Contains(DomainErrorCodes.BadRequest_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status400BadRequest; //400

        if (error.Code.Contains(DomainErrorCodes.Validation_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status400BadRequest; //400

        if (error.Code.Equals(DomainErrorCodes.Concurrency_ErrorCode, StringComparison.InvariantCultureIgnoreCase))
            return StatusCodes.Status409Conflict; //409

        return result;
    }
}