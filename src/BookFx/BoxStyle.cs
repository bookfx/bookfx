namespace BookFx
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static Functional.F;
    using static Make;

    [PublicAPI]
    public sealed class BoxStyle
    {
        public static readonly BoxStyle Empty = BoxStyleCore.Empty;

        public BoxStyle(BoxStyleCore core) => Get = core;

        public BoxStyleCore Get { get; }

        public static implicit operator BoxStyle(BoxStyleCore core) => new BoxStyle(core);

        [Pure]
        public BoxStyle Borders(BoxBorder border, params BoxBorder[] borders) =>
            Borders(borders.Prepend(border));

        [Pure]
        public BoxStyle Borders(IEnumerable<BoxBorder> borders) =>
            Get.With(borders: borders.Map(x => x.Get));

        [Pure]
        public BoxStyle DefaultBorder() => Borders(Border());

        [Pure]
        public BoxStyle Font(float size) => Get.With(fontSize: size);

        [Pure]
        public BoxStyle Font(string name) => Get.With(fontName: Some(name));

        [Pure]
        public BoxStyle Font(string name, float size) => Get.With(fontName: Some(name), fontSize: size);

        [Pure]
        public BoxStyle Font(float size, Color color) => Get.With(fontSize: size, fontColor: color);

        [Pure]
        public BoxStyle Font(string name, float size, Color color) =>
            Get.With(fontName: Some(name), fontSize: size, fontColor: color);

        [Pure]
        public BoxStyle Font(Color color) => Get.With(fontColor: color);

        [Pure]
        public BoxStyle Back(Color color) => Get.With(backColor: color);

        [Pure]
        public BoxStyle Color(Color font) => Get.With(fontColor: font);

        [Pure]
        public BoxStyle Color(Color font, Color back) => Get.With(fontColor: font, backColor: back);

        [Pure]
        public BoxStyle Bold(bool bold = true) => Get.With(isBold: bold);

        [Pure]
        public BoxStyle Italic(bool italic = true) => Get.With(isItalic: italic);

        [Pure]
        public BoxStyle Underline(bool underline = true) => Get.With(isUnderline: underline);

        [Pure]
        public BoxStyle Strike(bool strike = true) => Get.With(isStrike: strike);

        [Pure]
        public BoxStyle Wrap(bool wrap = true) => Get.With(isWrap: wrap);

        [Pure]
        public BoxStyle Align(HAlign align) => Get.With(hAlign: align);

        [Pure]
        public BoxStyle Align(VAlign align) => Get.With(vAlign: align);

        [Pure]
        public BoxStyle Left() => Align(HAlign.Left);

        [Pure]
        public BoxStyle Center() => Align(HAlign.Center);

        [Pure]
        public BoxStyle Right() => Align(HAlign.Right);

        [Pure]
        public BoxStyle Top() => Align(VAlign.Top);

        [Pure]
        public BoxStyle Middle() => Align(VAlign.Middle);

        [Pure]
        public BoxStyle Bottom() => Align(VAlign.Bottom);

        [Pure]
        public BoxStyle Indent(int size) => Get.With(indent: size);

        /// <summary>
        /// Set number format.
        /// </summary>
        /// <param name="format">Number format.</param>
        [Pure]
        public BoxStyle Format(string format) => Get.With(format: Some(format));

        /// <summary>
        /// Equivalent to:
        /// <code>Format("General")</code>
        /// </summary>
        [Pure]
        public BoxStyle DefaultFormat() => Format("General");

        /// <summary>
        /// Equivalent to:
        /// <code>Format("@")</code>
        /// </summary>
        [Pure]
        public BoxStyle Text() => Format("@");

        /// <summary>
        /// Equivalent to:
        /// <code>Format("#,##0")</code>
        /// </summary>
        [Pure]
        public BoxStyle Integer() => Format("#,##0");

        /// <summary>
        /// Equivalent to:
        /// <code>Format("#,##0.00")</code>
        /// </summary>
        [Pure]
        public BoxStyle Money() => Format("#,##0.00");

        /// <summary>
        /// Equivalent to:
        /// <code>Format("0%")</code>
        /// </summary>
        [Pure]
        public BoxStyle Percent() => Format("0%");

        /// <summary>
        /// Equivalent to:
        /// <code>Format("dd.mm.yyyy")</code>
        /// </summary>
        [Pure]
        public BoxStyle DateShort() => Format("dd.mm.yyyy");
    }
}