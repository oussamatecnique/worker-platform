﻿using System.Runtime.Serialization;

namespace worker.platform.application.Common.Exceptions;

public class UnauthorizedException: Exception
{
    public UnauthorizedException()
    {
    }

    protected UnauthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UnauthorizedException(string? message) : base(message)
    {
    }

    public UnauthorizedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
