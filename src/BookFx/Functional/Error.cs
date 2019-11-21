namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    internal class Error
    {
        internal Error(string message)
        {
            Message = message;
            Inners = ImmutableList<Error>.Empty;
        }

        internal Error(string message, IEnumerable<Error> inners)
        {
            Message = message;
            Inners = inners.ToImmutableList();
        }

        public string Message { get; }

        public ImmutableList<Error> Inners { get; }

        public static implicit operator Error(string message) => new Error(message);

        public override string ToString() =>
            Inners
                .Map(x => x.ToString().Indent())
                .Prepend(Message)
                .Join(Environment.NewLine);

        public override bool Equals(object obj) => obj is Error other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return (Message.GetHashCode() * 397) ^ Inners.GetHashCode();
            }
        }

        private bool Equals(Error other) =>
            string.Equals(Message, other.Message, StringComparison.Ordinal) &&
            Inners.SequenceEqual(other.Inners);
    }
}