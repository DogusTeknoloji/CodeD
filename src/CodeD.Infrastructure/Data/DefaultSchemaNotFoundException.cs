using System.Runtime.Serialization;

namespace CodeD.Infrastructure.Data
{
    [Serializable]
    public class DefaultSchemaNotFoundException : Exception
    {
        public DefaultSchemaNotFoundException()
        {
        }

        public DefaultSchemaNotFoundException(string? message) : base(message)
        {
        }

        public DefaultSchemaNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="Exception" /> class with serialized data.</summary>
        /// <param name="info">The <see cref="SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="ArgumentNullException">
        ///   <paramref name="info" /> is <see langword="null" />.</exception>
        /// <exception cref="SerializationException">The class name is <see langword="null" /> or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
#pragma warning disable SYSLIB0051 // Type or member is obsolete

        protected DefaultSchemaNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
#pragma warning restore SYSLIB0051 // Type or member is obsolete
        {
        }
    }
}