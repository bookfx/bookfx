namespace BookFx.Epplus
{
    using System.Text.RegularExpressions;

    internal static class Constraint
    {
        internal const int MaxColumn = 16_384;

        internal const int MaxRow = 1_048_576;

        internal const float MinFontSize = 1f;

        internal const float MaxFontSize = 409f;

        internal const int MinIndentSize = 0;

        internal const int MaxIndentSize = 250;

        internal const float MinColSize = 0f;

        internal const float MaxColSize = 255f;

        internal const float MinRowSize = 0f;

        internal const float MaxRowSize = 409f;

        internal const double MinMargin = 0;

        /// <summary>
        /// The max page margin in centimetres. It is the longest size of a A0 paper format.
        /// </summary>
        internal const double MaxMargin = 119;

        internal const int MinFit = 0;

        internal const int MaxFit = 32767;

        internal const int MinScale = 10;

        internal const int MaxScale = 400;

        internal static readonly Regex SheetNameRegex = new Regex(@"^[^:\\/?*[\]]{1,31}$");

        internal static readonly Regex RangeNameRegex = new Regex(@"(?i)^[a-z_]\w*$");

        internal static readonly Regex A1RangeNameRegex = new Regex(@"(?i)^[a-z]{1,3}\d{1,7}$");

        internal static readonly Regex R1C1RangeNameRegex = new Regex(@"(?i)^(?:(?:r|c)(?:[-\[\]\d]*)){1,2}$");
    }
}