#pragma warning disable 1591 // CS1591: Missing XML comment for publicly visible type or member

namespace BookFx
{
    using JetBrains.Annotations;

    /// <summary>
    /// Page view mode.
    /// </summary>
    [PublicAPI]
    public enum PageView
    {
        Normal,
        Layout,
        Break,
    }
}