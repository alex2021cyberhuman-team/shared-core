using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Conduit.Shared.Validation;

public class Validation : IEnumerable<ValidationResult>
{
    public Validation(
        List<ValidationResult>? results = null)
    {
        Results = results ?? new();
    }

    public List<ValidationResult> Results { get; set; }

    public static implicit operator bool(
        Validation validation)
    {
        return validation.Results.Any() == false;
    }

    public IEnumerator<ValidationResult> GetEnumerator()
    {
        return Results.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public static class ValidationExtensions
{
    public static ModelStateDictionary ToModelStateDictionary(
        this Validation validation)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var result in validation)
        {
            if (result.ErrorMessage != null)
            {
                modelStateDictionary.AddModelError(
                    result.MemberNames.FirstOrDefault() ?? string.Empty,
                    result.ErrorMessage);
            }
        }

        return modelStateDictionary;
    }

    public static Validation ToValidation(
        this FluentValidation.Results.ValidationResult fluentValidationResult)
    {
        var validationResults = fluentValidationResult.Errors.Select(x =>
                new ValidationResult(x.ErrorMessage, new[] { x.PropertyName }))
            .ToList();
        var validation = new Validation(validationResults);
        return validation;
    }

    public static IActionResult ToBadRequest(
        this Validation? validation)
    {
        return validation is null
            ? new BadRequestResult()
            : new BadRequestObjectResult(validation.ToModelStateDictionary());
    }
}
