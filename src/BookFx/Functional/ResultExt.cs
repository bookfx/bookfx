namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static F;
    using Unit = System.ValueTuple;

    internal static class ResultExt
    {
        public static T GetOrElse<T>(this Result<T> result, T defaultValue) =>
            result.Match(
                errors => defaultValue,
                x => x);

        public static T GetOrElse<T>(this Result<T> result, Func<T> fallback) =>
            result.Match(
                errors => fallback(),
                x => x);

        public static T GetOrElse<T>(this Result<T> result, Func<IEnumerable<Error>, T> fallback) =>
            result.Match(
                fallback,
                x => x);

        public static T ValueUnsafe<T>(this Result<T> result) =>
            result.Match(
                invalid: errors => throw new InvalidOperationException($"Cannot get value for {result}."),
                valid: value => value);

        public static IEnumerable<Error> ErrorsUnsafe<T>(this Result<T> result) =>
            result.Match(
                invalid: errors => errors,
                valid: value => throw new InvalidOperationException($"Cannot get errors for {result}."));

        public static Result<TR> Apply<T, TR>(this Result<Func<T, TR>> f, Result<T> x) =>
            f.Match(
                valid: fv => x.Match(
                    valid: xv => Valid(fv(xv)),
                    invalid: xe => (Result<TR>)Invalid(xe)),
                invalid: fe => x.Match(
                    valid: _ => Invalid(fe),
                    invalid: xe => Invalid(fe.Concat(xe))));

        public static Result<Func<T2, TR>> Apply<T1, T2, TR>(
            this Result<Func<T1, T2, TR>> f,
            Result<T1> x) =>
            Apply(f.Map(FuncExt.Curry), x);

        public static Result<Func<T2, T3, TR>> Apply<T1, T2, T3, TR>(
            this Result<Func<T1, T2, T3, TR>> f,
            Result<T1> x) =>
            Apply(f.Map(FuncExt.CurryFirst), x);

        public static Result<TR> Map<T, TR>(this Result<T> result, Func<T, TR> f) =>
            result.Match(
                invalid: Invalid<TR>,
                valid: value => Valid(f(value)));

        public static Result<Func<T2, TR>> Map<T1, T2, TR>(this Result<T1> result, Func<T1, T2, TR> f) =>
            result.Map(f.Curry());

        public static Result<Unit> ForEach<TR>(this Result<TR> result, Action<TR> action) =>
            Map(result, action.ToFunc());

        public static Result<T> Do<T>(this Result<T> result, Action<T> action)
        {
            result.ForEach(action);
            return result;
        }

        public static Result<TR> Bind<T, TR>(this Result<T> result, Func<T, Result<TR>> f) =>
            result.Match(
                invalid: errors => (Result<TR>)Invalid(errors),
                valid: f);

        public static Result<TR> Select<T, TR>(this Result<T> result, Func<T, TR> f) => result.Map(f);

        public static Result<TR> SelectMany<T1, T2, TR>(
            this Result<T1> result,
            Func<T1, Result<T2>> bind,
            Func<T1, T2, TR> project) =>
            result.Match(
                invalid: errors => (Result<TR>)Invalid(errors),
                valid: x1 => bind(x1)
                    .Match(
                        invalid: errors => (Result<TR>)Invalid(errors),
                        valid: x2 => Valid(project(x1, x2))));

        public static Option<T> ToOption<T>(this Result<T> result) =>
            result.Match(
                invalid: _ => None,
                valid: Some);
    }
}