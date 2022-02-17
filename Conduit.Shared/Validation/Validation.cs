using System.ComponentModel.DataAnnotations;

namespace Conduit.Shared.Validation;

public class Validation
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
}
