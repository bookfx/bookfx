#pragma warning disable 1591 // CS1591: Missing XML comment for publicly visible type or member

namespace BookFx
{
    using JetBrains.Annotations;

    /// <summary>
    /// Vertical alignment.
    /// </summary>
    [PublicAPI]
    public enum VAlign
    {
        Top,
        Middle,
        Bottom,
        Distributed,
        Justify,
    }
}