using System.Collections;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

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
