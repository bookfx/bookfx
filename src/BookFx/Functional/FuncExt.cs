namespace BookFx.Functional
{
    using System;

    internal static class FuncExt
    {
        public static Func<T1, Func<T2, TR>> Curry<T1, T2, TR>(this Func<T1, T2, TR> f) => x1 => x2 => f(x1, x2);

        public static Func<T1, Func<T2, Func<T3, TR>>> Curry<T1, T2, T3, TR>(this Func<T1, T2, T3, TR> f) =>
            x1 => x2 => x3 => f(x1, x2, x3);

        public static Func<T1, Func<T2, T3, TR>> CurryFirst<T1, T2, T3, TR>(this Func<T1, T2, T3, TR> f) =>
            x1 => (x2, x3) => f(x1, x2, x3);

        public static Func<T2, TR> Apply<T1, T2, TR>(this Func<T1, T2, TR> f, T1 x1) => x2 => f(x1, x2);

        public static Func<T2, T3, TR> Apply<T1, T2, T3, TR>(this Func<T1, T2, T3, TR> f, T1 x1) =>
            (x2, x3) => f(x1, x2, x3);
    }
}