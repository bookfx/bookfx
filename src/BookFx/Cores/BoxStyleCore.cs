namespace BookFx.Cores
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Drawing;
    using System.Linq;
    using BookFx.Functional;
    using JetBrains.Annotations;

    [PublicAPI]
    public sealed class BoxStyleCore
    {
        internal static readonly BoxStyleCore Empty = new BoxStyleCore(
            borders: Enumerable.Empty<BoxBorderCore>(),
            fontSize: F.None,
            fontName: F.None,
            fontColor: F.None,
            backColor: F.None,
            isBold: F.None,
            isItalic: F.None,
            isUnderline: F.None,
            isStrike: F.None,
            isWrap: F.None,
            hAlign: F.None,
            vAlign: F.None,
            indent: F.None,
            format: F.None);

        private BoxStyleCore(
            IEnumerable<BoxBorderCore> borders,
            Option<float> fontSize,
            Option<string> fontName,
            Option<Color> fontColor,
            Option<Color> backColor,
            Option<bool> isBold,
            Option<bool> isItalic,
            Option<bool> isUnderline,
            Option<bool> isStrike,
            Option<bool> isWrap,
            Option<HAlign> hAlign,
            Option<VAlign> vAlign,
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
            HAlign = hAlign;
            VAlign = vAlign;
            Indent = indent;
            Format = format;
        }

        public ImmutableList<BoxBorderCore> Borders { get; }

        public Option<float> FontSize { get; }

        public Option<string> FontName { get; }

        public Option<Color> FontColor { get; }

        public Option<Color> BackColor { get; }

        public Option<bool> IsBold { get; }

        public Option<bool> IsItalic { get; }

        public Option<bool> IsUnderline { get; }

        public Option<bool> IsStrike { get; }

        public Option<bool> IsWrap { get; }

        public Option<HAlign> HAlign { get; }

        public Option<VAlign> VAlign { get; }

        public Option<int> Indent { get; }

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
                hAlign: b.HAlign.OrElse(a.HAlign),
                vAlign: b.VAlign.OrElse(a.VAlign),
                indent: b.Indent.OrElse(a.Indent),
                format: b.Format.OrElse(a.Format));

        [Pure]
        internal static BoxStyleCore Mix(IEnumerable<BoxStyleCore> styles) =>
            styles.Aggregate(Empty, Mix);

        [Pure]
        internal BoxStyleCore With(
            IEnumerable<BoxBorderCore>? borders = null,
            Option<float>? fontSize = null,
            Option<string>? fontName = null,
            Option<Color>? fontColor = null,
            Option<Color>? backColor = null,
            Option<bool>? isBold = null,
            Option<bool>? isItalic = null,
            Option<bool>? isUnderline = null,
            Option<bool>? isStrike = null,
            Option<bool>? isWrap = null,
            Option<HAlign>? hAlign = null,
            Option<VAlign>? vAlign = null,
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
                hAlign ?? HAlign,
                vAlign ?? VAlign,
                indent ?? Indent,
                format ?? Format);
    }
}