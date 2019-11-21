namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Result
    {
        public struct Invalid : IEquatable<Invalid>
        {
            internal readonly IEnumerable<Error> Errors;

            public Invalid(IEnumerable<Error> errors) => Errors = errors;

            public static bool operator ==(Invalid left, Invalid right) => left.Equals(right);

            public static bool operator !=(Invalid left, Invalid right) => !(left == right);

            public bool Equals(Invalid other) => Errors.SequenceEqual(other.Errors);

            public override bool Equals(object obj) => obj is Invalid other && Equals(other);

            public override int GetHashCode() => Errors.GetHashCode();
        }
    }
}