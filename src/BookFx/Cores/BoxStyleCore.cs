namespace BookFx.Cores
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Drawing;
    using System.Linq;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    /// <summary>
    /// Gets a box style properties.
    /// </summary>
    [PublicAPI]
    public sealed class BoxStyleCore
    {
        internal static readonly BoxStyleCore Empty = new BoxStyleCore(
            borders: Enumerable.Empty<BoxBorderCore>(),
            fontSize: None,
            fontName: None,
            fontColor: None,
            backColor: None,
            isBold: None,
            isItalic: None,
            isUnderline: None,
            isStrike: None,
            isWrap: None,
            isShrink: None,
            hAlign: None,
            vAlign: None,
            rotation: None,
            indent: None,
            format: None);

        private BoxStyleCore(
            IEnumerable<BoxBorderCore> borders,
            Option<double> fontSize,
            Option<string> fontName,
            Option<Color> fontColor,
            Option<Color> backColor,
            Option<bool> isBold,
            Option<bool> isItalic,
            Option<bool> isUnderline,
            Option<bool> isStrike,
            Option<bool> isWrap,
            Option<bool> isShrink,
            Option<HAlign> hAlign,
            Option<VAlign> vAlign,
            Option<int> rotation,
            Option<int> indent,
            Option<string> format)
        {
            Borders = borders.ToImmutableList();
            FontSize = fontSize;
            FontName = fontName;
            FontColor = fontColor;
            BackColor = backColor;
            IsBold = isBold;
            IsItalic = isItalic;
            IsUnderline = isUnderline;
            IsStrike = isStrike;
            IsWrap = isWrap;
            IsShrink = isShrink;
            HAlign = hAlign;
            VAlign = vAlign;
            Rotation = rotation;
            Indent = indent;
            Format = format;
        }

        /// <summary>
        /// Gets borders.
        /// </summary>
        public ImmutableList<BoxBorderCore> Borders { get; }

        /// <summary>
        /// Gets the font size.
        /// </summary>
        public Option<double> FontSize { get; }

        /// <summary>
        /// Gets the font name.
        /// </summary>
        public Option<string> FontName { get; }

        /// <summary>
        /// Gets the font color.
        /// </summary>
        public Option<Color> FontColor { get; }

        /// <summary>
        /// Gets the background color.
        /// </summary>
        public Option<Color> BackColor { get; }

        /// <summary>
        /// Gets a value indicating whether is text bold.
        /// </summary>
        public Option<bool> IsBold { get; }

        /// <summary>
        /// Gets a value indicating whether is text italic.
        /// </summary>
        public Option<bool> IsItalic { get; }

        /// <summary>
        /// Gets a value indicating whether is text underlined.
        /// </summary>
        public Option<bool> IsUnderline { get; }

        /// <summary>
        /// Gets a value indicating whether is text struck.
        /// </summary>
        public Option<bool> IsStrike { get; }

        /// <summary>
        /// Gets a value indicating whether is text wrapped.
        /// </summary>
        public Option<bool> IsWrap { get; }

        /// <summary>
        /// Gets a value indicating whether is text shrinked to fit.
        /// </summary>
        public Option<bool> IsShrink { get; }

        /// <summary>
        /// Gets the horizontal alignment.
        /// </summary>
        public Option<HAlign> HAlign { get; }

        /// <summary>
        /// Gets the vertical alignment.
        /// </summary>
        public Option<VAlign> VAlign { get; }

        /// <summary>
        /// Gets the text rotation in degrees.
        /// Values are in the range 0 to 180. The first letter of the text is considered the center-point of the arc.
        /// For 0 - 90, the value represents degrees above horizon.
        /// For 91 - 180 the degrees below the horizon is calculated as:
        /// <code>DegreesBelowHorizon = 90 - Rotation</code>
        /// See also ECMA-376 - Office Open XML Part 1 - Fundamentals And Markup Language Reference - 18.8 Styles.
        /// </summary>
        public Option<int> Rotation { get; }

        /// <summary>
        /// Gets the indent size.
        /// </summary>
        public Option<int> Indent { get; }

        /// <summary>
        /// Gets the format.
        /// </summary>
        public Option<string> Format { get; }

        [Pure]
        internal static BoxStyleCore Mix(BoxStyleCore a, BoxStyleCore b) =>
            new BoxStyleCore(
                borders: a.Borders.Concat(b.Borders),
                fontSize: b.FontSize.OrElse(a.FontSize),
                fontName: b.FontName.OrElse(a.FontName),
                fontColor: b.FontColor.OrElse(a.FontColor),
                backColor: b.BackColor.OrElse(a.BackColor),
                isBold: b.IsBold.OrElse(a.IsBold),
                isItalic: b.IsItalic.OrElse(a.IsItalic),
                isUnderline: b.IsUnderline.OrElse(a.IsUnderline),
                isStrike: b.IsStrike.OrElse(a.IsStrike),
                isWrap: b.IsWrap.OrElse(a.IsWrap),
                isShrink: b.IsShrink.OrElse(a.IsShrink),
                hAlign: b.HAlign.OrElse(a.HAlign),
                vAlign: b.VAlign.OrElse(a.VAlign),
                rotation: b.Rotation.OrElse(a.Rotation),
                indent: b.Indent.OrElse(a.Indent),
                format: b.Format.OrElse(a.Format));

        [Pure]
        internal static BoxStyleCore Mix(IEnumerable<BoxStyleCore> styles) =>
            styles.Aggregate(Empty, Mix);

        [Pure]
        internal BoxStyleCore With(
            IEnumerable<BoxBorderCore>? borders = null,
            Option<double>? fontSize = null,
            Option<string>? fontName = null,
            Option<Color>? fontColor = null,
            Option<Color>? backColor = null,
            Option<bool>? isBold = null,
            Option<bool>? isItalic = null,
            Option<bool>? isUnderline = null,
            Option<bool>? isStrike = null,
            Option<bool>? isWrap = null,
            Option<bool>? isShrink = null,
            Option<HAlign>? hAlign = null,
            Option<VAlign>? vAlign = null,
            Option<int>? rotation = null,
            Option<int>? indent = null,
            Option<string>? format = null
        ) =>
            new BoxStyleCore(
                borders ?? Borders,
                fontSize ?? FontSize,
                fontName ?? FontName,
                fontColor ?? FontColor,
                backColor ?? BackColor,
                isBold ?? IsBold,
                isItalic ?? IsItalic,
                isUnderline ?? IsUnderline,
                isStrike ?? IsStrike,
                isWrap ?? IsWrap,
                isShrink ?? IsShrink,
                hAlign ?? HAlign,
                vAlign ?? VAlign,
                rotation ?? Rotation,
                indent ?? Indent,
                format ?? Format);
    }
}