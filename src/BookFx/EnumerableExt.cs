namespace BookFx
{
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using static Make;

    [PublicAPI]
    public static class EnumerableExt
    {
        [Pure]
        public static Book ToBook(this IEnumerable<Sheet> sheets) => Book(sheets);

        [Pure]
        public static Box ToRow(this IEnumerable<Box> boxes) => Row(boxes);

        [Pure]
        public static Box ToCol(this IEnumerable<Box> boxes) => Col(boxes);

        [Pure]
        public static Box ToStack(this IEnumerable<Box> boxes) => Stack(boxes);

        [Pure]
        public static BoxStyle Mix(this IEnumerable<BoxStyle> styles) => Style(styles);
    }
}