using System.Runtime.InteropServices.WindowsRuntime;

namespace Conduit.Shared.ResponsesExtensions;

public enum Error
{
    None,
    BadRequest,
    NotFound,
    Forbidden
}
