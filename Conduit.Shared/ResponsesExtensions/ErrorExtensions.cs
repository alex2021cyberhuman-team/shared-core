using Conduit.Shared.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Conduit.Shared.ResponsesExtensions;

public static class ErrorExtensions
{
    public static IActionResult GetAndLogActionResult(
        this Error error,
        object? output,
        Validations.Validation? validation,
        ILogger logger)
    {
        switch (error)
        {
            case Error.BadRequest:
                logger.LogWarning("BadRequest error");
                break;
            case Error.NotFound:
                logger.LogWarning("NotFound error");
                break;
            case Error.Forbidden:
                logger.LogWarning("Forbidden error");
                break;
            default: throw new ArgumentOutOfRangeException(nameof(error));
        }

        return error switch
        {
            Error.None => output != null ? new OkObjectResult(output) : new NoContentResult(),
            Error.BadRequest => validation.ToBadRequest(),
            Error.NotFound => new NotFoundResult(),
            Error.Forbidden => new ForbidResult(),
            _ => throw new ArgumentOutOfRangeException(nameof(error))
        };
    }

    public static IActionResult GetActionResult(
        this Error error,
        object? output,
        Validations.Validation? validation)
    {
        return error switch
        {
            Error.None => output != null ? new OkObjectResult(output) : new NoContentResult(),
            Error.BadRequest => validation.ToBadRequest(),
            Error.NotFound => new NotFoundResult(),
            Error.Forbidden => new ForbidResult(),
            _ => throw new ArgumentOutOfRangeException(nameof(error))
        };
    }
}
