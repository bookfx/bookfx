namespace BookFx
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// The only exception of BookFx, which is throws then the model has validation errors.
    /// </summary>
    [PublicAPI]
    public sealed class InvalidBookException : Exception
    {
        internal InvalidBookException(Errors.Book.AggregateError error)
            : base(error.ToString())
        {
        }
    }
}