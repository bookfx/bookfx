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
        internal static readonly BoxBorderCore Default = new(
            parts: None,
            style: None,
            color: None);

        private BoxBorderCore(
            Option<BorderParts> parts,
            Option<BorderStyle> style,
            Option<Color> color)
        {
            Parts = parts;
            Style = style;
            Color = color;
        }

        /// <summary>
        /// Gets the parts of the box to whom the border applied.
        /// </summary>
        public Option<BorderParts> Parts { get; }

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
            Option<BorderParts>? parts = null,
            Option<BorderStyle>? style = null,
            Option<Color>? color = null) =>
            new(
                parts ?? Parts,
                style ?? Style,
                color ?? Color);
    }
}