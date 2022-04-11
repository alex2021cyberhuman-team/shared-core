namespace Conduit.Shared.ResponsesExtensions;

public abstract class BaseResponse
{
    public Error Error { get; set; }

    public bool IsSuccess => Error.None == Error;
}
