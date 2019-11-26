namespace BookFx
{
    using JetBrains.Annotations;

    /// <summary>
    /// Page margin size.
    /// </summary>
    [PublicAPI]
    public struct MarginSize
    {
        private const decimal CentimetresInInch = 2.54m;

        private MarginSize(decimal centimetres) => Centimetres = centimetres;

        /// <summary>
        /// Gets the page margin size in centimeters.
        /// </summary>
        public decimal Centimetres { get; }

        /// <summary>
        /// Gets the page margin size in inches.
        /// </summary>
        public decimal Inches => Centimetres / CentimetresInInch;

        /// <summary>
        /// Creates a page margin size from centimeters.
        /// </summary>
        public static MarginSize FromCentimetres(decimal centimeters) => new MarginSize(centimeters);

        /// <summary>
        /// Creates a page margin size from inches.
        /// </summary>
        public static MarginSize FromInches(decimal inches) => new MarginSize(inches * CentimetresInInch);
    }
}