using System.Runtime.Serialization;

namespace worker.platform.application.Users.Exceptions;

public class WrongPasswordException: UnauthorizedAccessException
{
    public WrongPasswordException()
    {
    }

    protected WrongPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public WrongPasswordException(string message) : base(message)
    {
    }

    public WrongPasswordException(string message, Exception inner) : base(message, inner)
    {
    }
}
