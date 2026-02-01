using System.Runtime.Serialization;

namespace worker.platform.application.Common.Exceptions;

[Serializable]
public class NotFoundArgumentsException: NotFoundException
{
    public NotFoundArgumentsException()
    {
    }

    protected NotFoundArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public NotFoundArgumentsException(string message) : base(message)
    {
    }

    public NotFoundArgumentsException(string message, Exception innerException) : base(message, innerException)
    {
    }

    public NotFoundArgumentsException(params string[] arguments) : base($"The following arguments match not found: {string.Join(", ", arguments)}")
    {
    }
}
