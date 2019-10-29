namespace BookFx
{
    using System.Drawing;
    using BookFx.Cores;
    using JetBrains.Annotations;

    [PublicAPI]
    public sealed class BoxBorder
    {
        public static readonly BoxBorder Empty = BoxBorderCore.Empty;

        private BoxBorder(BoxBorderCore core) => Get = core;

        public BoxBorderCore Get { get; }

        [Pure]
        public static implicit operator BoxBorder(BoxBorderCore core) => new BoxBorder(core);

        [Pure]
        public BoxBorder Restrict(BorderPart part) => Get.With(part: part);

        [Pure]
        public BoxBorder Style(BorderStyle style) => Get.With(style: style);

        [Pure]
        public BoxBorder Color(Color color) => Get.With(color: color);
    }
}