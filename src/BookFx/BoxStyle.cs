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

    /// <summary>
    /// A style.
    /// </summary>
    [PublicAPI]
    public sealed class BoxStyle
    {
        /// <summary>
        /// The empty <see cref="BoxStyle"/>.
        /// </summary>
        public static readonly BoxStyle Empty = BoxStyleCore.Empty;

        public BoxStyle(BoxStyleCore core) => Get = core;

        /// <summary>
        /// Gets properties of the style.
        /// </summary>
        public BoxStyleCore Get { get; }

        public static implicit operator BoxStyle(BoxStyleCore core) => new BoxStyle(core);

        /// <summary>
        /// Define borders.
        /// </summary>
        /// <param name="border">The first border.</param>
        /// <param name="borders">Other borders.</param>
        [Pure]
        public BoxStyle Borders(BoxBorder border, params BoxBorder[] borders) =>
            Borders(borders.Prepend(border));

        /// <summary>
        /// Define borders.
        /// </summary>
        [Pure]
        public BoxStyle Borders(IEnumerable<BoxBorder> borders) =>
            Get.With(borders: borders.Map(x => x.Get));

        /// <summary>
        /// Define regular borders.
        /// </summary>
        [Pure]
        public BoxStyle DefaultBorder() => Borders(Border());

        /// <summary>
        /// Define a font size.
        /// </summary>
        /// <param name="size">A font size.</param>
        [Pure]
        public BoxStyle Font(float size) => Get.With(fontSize: size);

        /// <summary>
        /// Define a font.
        /// </summary>
        /// <param name="name">A font name.</param>
        [Pure]
        public BoxStyle Font(string name) => Get.With(fontName: Some(name));

        /// <summary>
        /// Define a font and its size.
        /// </summary>
        /// <param name="name">A font name.</param>
        /// <param name="size">A font size.</param>
        [Pure]
        public BoxStyle Font(string name, float size) => Get.With(fontName: Some(name), fontSize: size);

        /// <summary>
        /// Define a font size and a font color.
        /// </summary>
        /// <param name="size">A font size.</param>
        /// <param name="color">A font color.</param>
        [Pure]
        public BoxStyle Font(float size, Color color) => Get.With(fontSize: size, fontColor: color);

        /// <summary>
        /// Define a font, its size and color.
        /// </summary>
        /// <param name="name">A font name.</param>
        /// <param name="size">A font size.</param>
        /// <param name="color">A font color.</param>
        [Pure]
        public BoxStyle Font(string name, float size, Color color) =>
            Get.With(fontName: Some(name), fontSize: size, fontColor: color);

        /// <summary>
        /// Define a font color.
        /// </summary>
        /// <param name="color">A font color.</param>
        [Pure]
        public BoxStyle Font(Color color) => Get.With(fontColor: color);

        /// <summary>
        /// Define a background color.
        /// </summary>
        /// <param name="color">A background color.</param>
        [Pure]
        public BoxStyle Back(Color color) => Get.With(backColor: color);

        /// <summary>
        /// Define a font color.
        /// </summary>
        /// <param name="font">A font color.</param>
        [Pure]
        public BoxStyle Color(Color font) => Get.With(fontColor: font);

        /// <summary>
        /// Define a font color and a background color.
        /// </summary>
        /// <param name="font">A font color.</param>
        /// <param name="back">A background color.</param>
        [Pure]
        public BoxStyle Color(Color font, Color back) => Get.With(fontColor: font, backColor: back);

        /// <summary>
        /// In bold.
        /// </summary>
        /// <param name="bold">true - set bold; false - unset bold.</param>
        [Pure]
        public BoxStyle Bold(bool bold = true) => Get.With(isBold: bold);

        /// <summary>
        /// In italic.
        /// </summary>
        /// <param name="italic">true - set italic; false - unset italic.</param>
        [Pure]
        public BoxStyle Italic(bool italic = true) => Get.With(isItalic: italic);

        /// <summary>
        /// Underline.
        /// </summary>
        /// <param name="underline">true - set underline; false - unset underline.</param>
        [Pure]
        public BoxStyle Underline(bool underline = true) => Get.With(isUnderline: underline);

        /// <summary>
        /// Strike.
        /// </summary>
        /// <param name="strike">true - set strike; false - unset strike.</param>
        [Pure]
        public BoxStyle Strike(bool strike = true) => Get.With(isStrike: strike);

        /// <summary>
        /// Define a text wrap.
        /// </summary>
        /// <param name="wrap">true - set text wrap; false - unset text wrap.</param>
        [Pure]
        public BoxStyle Wrap(bool wrap = true) => Get.With(isWrap: wrap);

        /// <summary>
        /// Define a horizontal alignment.
        /// </summary>
        /// <param name="align">A horizontal alignment.</param>
        [Pure]
        public BoxStyle Align(HAlign align) => Get.With(hAlign: align);

        /// <summary>
        /// Define a vertical alignment.
        /// </summary>
        /// <param name="align">A vertical alignment.</param>
        [Pure]
        public BoxStyle Align(VAlign align) => Get.With(vAlign: align);

        /// <summary>
        /// Align to the left.
        /// </summary>
        [Pure]
        public BoxStyle Left() => Align(HAlign.Left);

        /// <summary>
        /// Align at the center horizontally.
        /// </summary>
        [Pure]
        public BoxStyle Center() => Align(HAlign.Center);

        /// <summary>
        /// Align to the right.
        /// </summary>
        [Pure]
        public BoxStyle Right() => Align(HAlign.Right);

        /// <summary>
        /// Align to the top.
        /// </summary>
        [Pure]
        public BoxStyle Top() => Align(VAlign.Top);

        /// <summary>
        /// Align at the middle vertically.
        /// </summary>
        [Pure]
        public BoxStyle Middle() => Align(VAlign.Middle);

        /// <summary>
        /// Align to the bottom.
        /// </summary>
        [Pure]
        public BoxStyle Bottom() => Align(VAlign.Bottom);

        /// <summary>
        /// Define an indent.
        /// </summary>
        /// <param name="size">A size of indent.</param>
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