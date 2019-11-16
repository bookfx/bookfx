#pragma warning disable 1591 // CS1591: Missing XML comment for publicly visible type or member

namespace BookFx
{
    using JetBrains.Annotations;

    /// <summary>
    /// Horizontal alignment.
    /// </summary>
    [PublicAPI]
    public enum HAlign
    {
        General,
        Left,
        Center,
        CenterContinuous,
        Right,
        Fill,
        Distributed,
        Justify,
    }
}