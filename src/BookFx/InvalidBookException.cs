namespace BookFx
{
    using System;
    using JetBrains.Annotations;

    [PublicAPI]
    public sealed class InvalidBookException : Exception
    {
        internal InvalidBookException(Errors.Book.AggregateError error)
            : base(error.ToString())
        {
        }
    }
}