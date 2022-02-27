using Microsoft.Extensions.DependencyInjection;

namespace Conduit.Shared.Validation;

public static class ValidateModelAttributeExtensions
{
    public static IMvcBuilder RegisterValidateModelAttribute(
        this IMvcBuilder builder)
    {
        return builder.AddMvcOptions(x =>
            x.Filters.Add(ValidateModelAttribute.Instance));
    }
}
