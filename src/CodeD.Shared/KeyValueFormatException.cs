using System.Runtime.Serialization;

namespace CodeD.Domain.Abstractions;

[Serializable]
public class KeyValueFormatException : Exception
{
    public KeyValueFormatException()
    {
    }

    public KeyValueFormatException(string? message) : base(message)
    {
    }

    public KeyValueFormatException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

#pragma warning disable SYSLIB0051 // Type or member is obsolete

    protected KeyValueFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
#pragma warning restore SYSLIB0051 // Type or member is obsolete
    {
    }
}