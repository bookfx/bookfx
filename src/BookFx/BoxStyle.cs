namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;
    using static Make;

    /// <summary>
    /// A box style.
    /// </summary>
    [PublicAPI]
    public sealed class BoxStyle
    {
        /// <summary>
        /// The empty <see cref="BoxStyle"/>.
        /// </summary>
        public static readonly BoxStyle Empty = BoxStyleCore.Empty;

        private BoxStyle(BoxStyleCore core) => Get = core;

        /// <summary>
        /// Gets properties of the style.
        /// </summary>
        public BoxStyleCore Get { get; }

        /// <summary>
        /// Implicit convert from <see cref="BoxStyleCore"/> to <see cref="BoxStyle"/>.
        /// </summary>
        public static implicit operator BoxStyle(BoxStyleCore core) => new BoxStyle(core);

        /// <summary>
        /// Define borders.
        /// </summary>
        /// <param name="first">The first border.</param>
        /// <param name="others">Other borders.</param>
        [Pure]
        public BoxStyle Borders(BoxBorder first, params BoxBorder[] others) =>
            Borders(others.Prepend(first));

        /// <summary>
        /// Define borders.
        /// </summary>
        [Pure]
        public BoxStyle Borders(IEnumerable<BoxBorder> boxBorders) =>
            Get.With(borders: boxBorders.Map(x => x.Get));

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
        public BoxStyle Font(double size) => Get.With(fontSize: size);

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
        public BoxStyle Font(string name, double size) => Get.With(fontName: Some(name), fontSize: size);

        /// <summary>
        /// Define a font size and a font color.
        /// </summary>
        /// <param name="size">A font size.</param>
        /// <param name="color">A font color.</param>
        [Pure]
        public BoxStyle Font(double size, Color color) => Get.With(fontSize: size, fontColor: color);

        /// <summary>
        /// Define a font, its size and color.
        /// </summary>
        /// <param name="name">A font name.</param>
        /// <param name="size">A font size.</param>
        /// <param name="color">A font color.</param>
        [Pure]
        public BoxStyle Font(string name, double size, Color color) =>
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
        [Pure]
        public BoxStyle Bold() => Get.With(isBold: true);

        /// <summary>
        /// Set or unset bold.
        /// </summary>
        /// <param name="isBold">true - set bold; false - unset bold.</param>
        [Pure]
        public BoxStyle Bold(bool isBold) => Get.With(isBold: isBold);

        /// <summary>
        /// In italic.
        /// </summary>
        [Pure]
        public BoxStyle Italic() => Get.With(isItalic: true);

        /// <summary>
        /// Set or unset italic.
        /// </summary>
        /// <param name="isItalic">true - set italic; false - unset italic.</param>
        [Pure]
        public BoxStyle Italic(bool isItalic) => Get.With(isItalic: isItalic);

        /// <summary>
        /// Underline.
        /// </summary>
        [Pure]
        public BoxStyle Underline() => Get.With(isUnderline: true);

        /// <summary>
        /// Set or unset underline.
        /// </summary>
        /// <param name="isUnderlined">true - set underline; false - unset underline.</param>
        [Pure]
        public BoxStyle Underline(bool isUnderlined) => Get.With(isUnderline: isUnderlined);

        /// <summary>
        /// Strike.
        /// </summary>
        [Pure]
        public BoxStyle Strike() => Get.With(isStrike: true);

        /// <summary>
        /// Set or unset strike.
        /// </summary>
        /// <param name="isStruck">true - set strike; false - unset strike.</param>
        [Pure]
        public BoxStyle Strike(bool isStruck) => Get.With(isStrike: isStruck);

        /// <summary>
        /// Wrap text.
        /// </summary>
        [Pure]
        public BoxStyle Wrap() => Get.With(isWrap: true);

        /// <summary>
        /// Set or unset the text wrap.
        /// </summary>
        /// <param name="isWrapped">true - set text wrap; false - unset text wrap.</param>
        [Pure]
        public BoxStyle Wrap(bool isWrapped) => Get.With(isWrap: isWrapped);

        /// <summary>
        /// Shrink text to fit.
        /// </summary>
        [Pure]
        public BoxStyle Shrink() => Get.With(isShrink: true);

        /// <summary>
        /// Set or unset the text shrinking to fit.
        /// </summary>
        /// <param name="isShrinked">true - set text shrinking; false - unset text shrinking.</param>
        [Pure]
        public BoxStyle Shrink(bool isShrinked) => Get.With(isShrink: isShrinked);

        /// <summary>
        /// Define a horizontal alignment.
        /// </summary>
        /// <param name="horizontalAlign">A horizontal alignment.</param>
        [Pure]
        public BoxStyle Align(HAlign horizontalAlign) => Get.With(hAlign: horizontalAlign);

        /// <summary>
        /// Define a vertical alignment.
        /// </summary>
        /// <param name="verticalAlign">A vertical alignment.</param>
        [Pure]
        public BoxStyle Align(VAlign verticalAlign) => Get.With(vAlign: verticalAlign);

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
        /// Align horizontally at the center of adjacent cells with the <see cref="HAlign.CenterContinuous"/> alignment.
        /// </summary>
        [Pure]
        public BoxStyle CenterContinuous() => Align(HAlign.CenterContinuous);

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
        /// Rotate the text counterclockwise.
        /// </summary>
        /// <param name="degrees">An angle of rotation in degrees from 0 to 90.</param>
        [Pure]
        public BoxStyle RotateCounterclockwise(int degrees) => Get.With(rotation: degrees);

        /// <summary>
        /// Rotate the text clockwise.
        /// </summary>
        /// <param name="degrees">An angle of rotation in degrees from 1 to 90.</param>
        [Pure]
        public BoxStyle RotateClockwise(int degrees) => Get.With(rotation: 90 + degrees);

        /// <summary>
        /// Rotate the text counterclockwise.
        /// </summary>
        /// <param name="degrees">An angle of rotation in degrees from 0 to 180.</param>
        [Obsolete("Use RotateCounterclockwise or RotateClockwise instead.")]
        [Pure]
        public BoxStyle Rotate(int degrees) => Get.With(rotation: degrees);

        /// <summary>
        /// Define an indent.
        /// </summary>
        /// <param name="size">A size of indent.</param>
        [Pure]
        public BoxStyle Indent(int size) => Get.With(indent: size);

        /// <summary>
        /// Set number format.
        /// </summary>
        /// <param name="numberFormat">Number format.</param>
        [Pure]
        public BoxStyle Format(string numberFormat) => Get.With(format: Some(numberFormat));

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