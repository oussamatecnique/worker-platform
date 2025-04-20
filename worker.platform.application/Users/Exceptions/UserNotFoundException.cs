using System.Runtime.Serialization;
using worker.platform.application.Common.Exceptions;

namespace worker.platform.application.Users.Exceptions;

public class UserNotFoundException: NotFoundException
{
    public UserNotFoundException()
    {
    }

    protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
