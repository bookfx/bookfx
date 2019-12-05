namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;
    using Unit = System.ValueTuple;

    internal static class F
    {
        public static Option.None None => Option.None.Default;

        public static Option<T> Some<T>(T value) => new Option.Some<T>(value);

        public static Unit Unit() => default;

        public static Result<T> Valid<T>(T value) => new Result<T>(value);

        public static Result.Invalid Invalid(params Error[] errors) => new Result.Invalid(errors);

        public static Result<T> Invalid<T>(params Error[] errors) => new Result.Invalid(errors);

        public static Result.Invalid Invalid(IEnumerable<Error> errors) => new Result.Invalid(errors);

        public static Result<T> Invalid<T>(IEnumerable<Error> errors) => new Result.Invalid(errors);

        public static Tee<object> NoAct() => _ => Unit();

        public static IEnumerable<T> List<T>(T item, params T[] others) => others.Prepend(item).ToImmutableList();

        public static Func<TR> Fun<TR>(Func<TR> f) => f;

        public static Func<T1, TR> Fun<T1, TR>(Func<T1, TR> f) => f;

        public static Func<T1, T2, TR> Fun<T1, T2, TR>(Func<T1, T2, TR> f) => f;

        public static TR Using<T, TR>(T disposable, Func<T, TR> f)
            where T : IDisposable =>
            Using(() => disposable, f);

        public static TR Using<T, TR>(Func<T> create, Func<T, TR> f)
            where T : IDisposable
        {
            using var disposable = create();
            return f(disposable);
        }
    }
}