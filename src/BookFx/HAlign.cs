namespace BookFx
{
    using JetBrains.Annotations;

    /// <summary>
    /// Horizontal alignment.
    /// </summary>
    [PublicAPI]
    public enum HAlign
    {
        /// <summary>
        /// General horizontal alignment.
        /// </summary>
        General,

        /// <summary>
        /// To the left alignment.
        /// </summary>
        Left,

        /// <summary>
        /// At the center horizontal alignment.
        /// </summary>
        Center,

        /// <summary>
        /// At the selection center alignment.
        /// </summary>
        CenterContinuous,

        /// <summary>
        /// At the right alignment.
        /// </summary>
        Right,

        /// <summary>
        /// Fill horizontal alignment.
        /// </summary>
        Fill,

        /// <summary>
        /// Distributed horizontal alignment.
        /// </summary>
        Distributed,

        /// <summary>
        /// Justify horizontal alignment.
        /// </summary>
        Justify,
    }
}