namespace BookFx.Cores
{
    using System.Drawing;
    using BookFx.Functional;
    using JetBrains.Annotations;

    public sealed class BoxBorderCore
    {
        public static readonly BoxBorderCore Empty = new BoxBorderCore(
            part: F.None,
            style: F.None,
            color: F.None);

        private BoxBorderCore(
            Option<BorderPart> part,
            Option<BorderStyle> style,
            Option<Color> color)
        {
            Part = part;
            Style = style;
            Color = color;
        }

        public Option<BorderPart> Part { get; }

        public Option<BorderStyle> Style { get; }

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