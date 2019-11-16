namespace BookFx.Cores
{
    using System.Drawing;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    /// <summary>
    /// Gets a border properties.
    /// </summary>
    [PublicAPI]
    public sealed class BoxBorderCore
    {
        internal static readonly BoxBorderCore Empty = new BoxBorderCore(
            part: None,
            style: None,
            color: None);

        private BoxBorderCore(
            Option<BorderPart> part,
            Option<BorderStyle> style,
            Option<Color> color)
        {
            Part = part;
            Style = style;
            Color = color;
        }

        /// <summary>
        /// Gets the part part of the box to which the border applied.
        /// </summary>
        public Option<BorderPart> Part { get; }

        /// <summary>
        /// Gets the style of the border.
        /// </summary>
        public Option<BorderStyle> Style { get; }

        /// <summary>
        /// Gets the color of the border.
        /// </summary>
        public Option<Color> Color { get; }

        [Pure]
        internal BoxBorderCore With(
            Option<BorderPart>? part = null,
            Option<BorderStyle>? style = null,
            Option<Color>? color = null) =>
            new BoxBorderCore(
                part ?? Part,
                style ?? Style,
                color ?? Color);
    }
}