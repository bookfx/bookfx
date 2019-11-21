namespace BookFx
{
    using JetBrains.Annotations;

    /// <summary>
    /// Page view mode.
    /// </summary>
    [PublicAPI]
    public enum PageView
    {
        /// <summary>
        /// Normal page view mode.
        /// </summary>
        Normal,

        /// <summary>
        /// Layout page view mode.
        /// </summary>
        Layout,

        /// <summary>
        /// Break page view mode.
        /// </summary>
        Break,
    }
}