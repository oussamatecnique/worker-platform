using System.Runtime.Serialization;
using worker.platform.application.Common.Exceptions;

namespace worker.platform.application.JobsDeals.Exceptions;

[Serializable]
public class InvalidJobResponseParamsException: ValidationException
{
    public InvalidJobResponseParamsException()
    {
    }

    protected InvalidJobResponseParamsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public InvalidJobResponseParamsException(string message) : base(message)
    {
    }

    public InvalidJobResponseParamsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
