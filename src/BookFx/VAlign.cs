namespace BookFx
{
    using JetBrains.Annotations;

    /// <summary>
    /// Vertical alignment.
    /// </summary>
    [PublicAPI]
    public enum VAlign
    {
        /// <summary>
        /// To the top alignment.
        /// </summary>
        Top,

        /// <summary>
        /// At the middle vertical alignment.
        /// </summary>
        Middle,

        /// <summary>
        /// To the bottom alignment.
        /// </summary>
        Bottom,

        /// <summary>
        /// Distributed vertical alignment.
        /// </summary>
        Distributed,

        /// <summary>
        /// Justify vertical alignment.
        /// </summary>
        Justify,
    }
}