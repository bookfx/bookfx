namespace BookFx.Functional
{
    using System.Collections.Generic;

    internal static class Result
    {
        public struct Invalid
        {
            internal readonly IEnumerable<Error> Errors;

            public Invalid(IEnumerable<Error> errors) => Errors = errors;
        }
    }
}