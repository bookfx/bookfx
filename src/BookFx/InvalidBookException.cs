namespace BookFx
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;

    /// <summary>
    /// The only exception of BookFx, which is throws then the model has validation errors.
    /// </summary>
    [PublicAPI]
    [Serializable]
    public sealed class InvalidBookException : Exception
    {
        internal InvalidBookException(Errors.Book.AggregateError error)
            : base(error.ToString())
        {
        }

        private InvalidBookException()
        {
        }

        private InvalidBookException(string message)
            : base(message)
        {
        }

        private InvalidBookException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        private InvalidBookException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}