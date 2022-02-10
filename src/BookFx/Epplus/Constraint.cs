namespace BookFx.Epplus
{
    using System.Text.RegularExpressions;

    internal static class Constraint
    {
        internal const int MaxColumn = 16_384;

        internal const int MaxRow = 1_048_576;

        internal const double MinFontSize = 1d;

        internal const double MaxFontSize = 409d;

        internal const int MinRotation = 0;

        internal const int MaxRotation = 180;

        internal const int MinIndentSize = 0;

        internal const int MaxIndentSize = 250;

        internal const double MinColSize = 0d;

        internal const double MaxColSize = 255d;

        internal const double MinRowSize = 0d;

        internal const double MaxRowSize = 409d;

        internal const double MinMargin = 0;

        /// <summary>
        /// The max page margin in centimetres. It is the longest size of a A0 paper format.
        /// </summary>
        internal const double MaxMargin = 119;

        internal const int MinFit = 0;

        internal const int MaxFit = 32767;

        internal const int MinScale = 10;

        internal const int MaxScale = 400;

        internal static readonly Regex SheetNameRegex = new(@"^[^:\\/?*[\]]{1,31}$");

        internal static readonly Regex RangeNameRegex = new(@"(?i)^(?=\D)\w+$");

        internal static readonly Regex A1RangeNameRegex = new(@"(?i)^[a-z]{1,3}\d{1,7}$");

        internal static readonly Regex R1C1RangeNameRegex = new(@"(?i)^(?:(?:r|c)(?:[-\[\]\d]*)){1,2}$");
    }
}