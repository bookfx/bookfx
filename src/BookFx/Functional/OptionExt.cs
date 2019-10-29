namespace BookFx.Functional
{
    using System;
    using static F;
    using Unit = System.ValueTuple;

    internal static class OptionExt
    {
        public static Option<TR> Map<T, TR>(this Option<T> option, Func<T, TR> f) =>
            option.Match(
                none: () => None,
                some: x => Some(f(x)));

        public static Option<TR> Bind<T, TR>(this Option<T> option, Func<T, Option<TR>> f) =>
            option.Match(
                none: () => None,
                some: f);

        public static Option<T> Where<T>(this Option<T> option, Func<T, bool> predicate) =>
            option.Match(
                none: () => None,
                some: x => predicate(x) ? option : None);

        public static Option<Unit> ForEach<T>(this Option<T> option, Action<T> action) => Map(option, action.ToFunc());

        public static T ValueUnsafe<T>(this Option<T> option) =>
            option.Match(
                none: () => throw new InvalidOperationException(),
                some: x => x);

        public static T GetOrElse<T>(this Option<T> option, T defaultValue) =>
            option.Match(
                none: () => defaultValue,
                some: value => value);

        public static T GetOrElse<T>(this Option<T> option, Func<T> fallback) =>
            option.Match(
                none: fallback,
                some: value => value);

        public static Option<T> OrElse<T>(this Option<T> option, Option<T> another) =>
            option.Match(
                none: () => another,
                some: _ => option);

        public static Option<T> OrElse<T>(this Option<T> option, Func<Option<T>> fallback) =>
            option.Match(
                none: fallback,
                some: _ => option);

        public static Unit Match<T>(this Option<T> option, Action none, Action<T> some) =>
            option.Match(none.ToFunc(), some.ToFunc());

        public static Result<T> ToResult<T>(this Option<T> option, Func<Error> error) =>
            option.Match(
                none: () => Invalid(error()),
                some: Valid);

        public static Result<T> ToResultUnsafe<T>(this Option<T> option) =>
            option.ToResult(() => throw new InvalidOperationException($"Cannot get result for {option}."));
    }
}