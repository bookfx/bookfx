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

        internal const int MinScale = 10;

        internal const int MaxScale = 400;

        internal static readonly Regex SheetNameRegex = new Regex(@"^[^:\\/?*[\]]{1,31}$");
    }
}