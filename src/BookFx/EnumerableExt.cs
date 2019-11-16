namespace BookFx
{
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using static Make;

    /// <summary>
    /// IEnumerable extensions for BookFx types.
    /// </summary>
    [PublicAPI]
    public static class EnumerableExt
    {
        /// <summary>
        /// Make a <see cref="Book"/> from sheets.
        /// </summary>
        /// <param name="sheets">Sheets.</param>
        [Pure]
        public static Book ToBook(this IEnumerable<Sheet> sheets) => Book(sheets);

        /// <summary>
        /// Make a <see cref="RowBox"/> from other boxes.
        /// </summary>
        /// <param name="boxes">Boxes.</param>
        [Pure]
        public static Box ToRow(this IEnumerable<Box> boxes) => Row(boxes);

        /// <summary>
        /// Make a <see cref="ColBox"/> from other boxes.
        /// </summary>
        /// <param name="boxes">Boxes.</param>
        [Pure]
        public static Box ToCol(this IEnumerable<Box> boxes) => Col(boxes);

        /// <summary>
        /// Make a <see cref="StackBox"/> from other boxes.
        /// </summary>
        /// <param name="boxes">Boxes.</param>
        [Pure]
        public static Box ToStack(this IEnumerable<Box> boxes) => Stack(boxes);

        /// <summary>
        /// Mix styles into a new style.
        /// </summary>
        /// <param name="styles">Styles to mixing.</param>
        [Pure]
        public static BoxStyle Mix(this IEnumerable<BoxStyle> styles) => Style(styles);
    }
}