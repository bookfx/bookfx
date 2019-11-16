#pragma warning disable 1591 // CS1591: Missing XML comment for publicly visible type or member

namespace BookFx
{
    using JetBrains.Annotations;

    /// <summary>
    /// Box type.
    /// </summary>
    [PublicAPI]
    public enum BoxType : byte
    {
        Value,
        Row,
        Col,
        Stack,
        Proto,
    }
}