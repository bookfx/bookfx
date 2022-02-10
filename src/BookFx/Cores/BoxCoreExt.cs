namespace BookFx.Cores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    internal static class BoxCoreExt
    {
        [Pure]
        public static T Match<T>(
            this BoxCore box,
            Func<BoxCore, T> row,
            Func<BoxCore, T> col,
            Func<BoxCore, T> stack,
            Func<BoxCore, T> value,
            Func<BoxCore, T> proto) =>
            box.Type switch
            {
                BoxType.Value => value(box),
                BoxType.Row => row(box),
                BoxType.Col => col(box),
                BoxType.Stack => stack(box),
                BoxType.Proto => proto(box),
                _ => throw new InvalidOperationException(),
            };

        [Pure]
        public static Option<T> Map<T>(
            this Option<BoxCore> option,
            Func<BoxCore, T> row,
            Func<BoxCore, T> col,
            Func<BoxCore, T> stack,
            Func<BoxCore, T> value,
            Func<BoxCore, T> proto) =>
            option.Match(
                none: () => None,
                some: box => Some(box.Match(row, col, stack, value, proto)));

        [Pure]
        public static IEnumerable<BoxCore> SelfAndDescendants(this BoxCore box) =>
            box.Descendants().Prepend(box);

        [Pure]
        public static IEnumerable<BoxCore> Descendants(this BoxCore box) =>
            box.ImmediateDescendants().Bind(SelfAndDescendants);

        [Pure]
        public static IEnumerable<BoxCore> ImmediateDescendants(this BoxCore box) =>
            box.Children.Concat(box.Slots.Map(x => x.Box));

        [Pure]
        public static IEnumerable<ProtoCore> Protos(this BoxCore box) =>
            box.SelfAndDescendants().Bind(x => x.Proto);
    }
}