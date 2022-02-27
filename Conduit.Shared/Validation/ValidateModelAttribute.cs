using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Conduit.Shared.Validation;

public class ValidateModelAttribute : ActionFilterAttribute
{
    static ValidateModelAttribute()
    {
        Instance = new();
    }

    public static ValidateModelAttribute Instance { get; }

    public override void OnActionExecuting(
        ActionExecutingContext context)
    {
        if (context.ModelState.IsValid == false)
        {
            var lowerCaseSerializableError =
                new LowerCaseSerializableError(context.ModelState);
            var actionResult =
                new ObjectResult(new { errors = lowerCaseSerializableError })
                {
                    StatusCode = (int)HttpStatusCode.UnprocessableEntity
                };
            actionResult.ExecuteResultAsync(context);
        }
    }
}
