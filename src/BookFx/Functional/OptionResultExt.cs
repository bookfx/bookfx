namespace BookFx.Functional
{
    using System;
    using static F;

    internal static class OptionResultExt
    {
        public static Result<Option<TR>> Traverse<T, TR>(this Option<T> option, Func<T, Result<TR>> f) =>
            option.Match(
                none: () => Valid((Option<TR>)None),
                some: x => f(x).Map(Some));
    }
}