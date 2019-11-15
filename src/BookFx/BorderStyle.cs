#pragma warning disable 1591 // CS1591: Missing XML comment for publicly visible type or member

namespace BookFx
{
    using JetBrains.Annotations;

    /// <summary>
    /// A border style.
    /// </summary>
    [PublicAPI]
    public enum BorderStyle
    {
        None,
        Hair,
        Dotted,
        DashDot,
        Thin,
        DashDotDot,
        Dashed,
        MediumDashDotDot,
        MediumDashed,
        MediumDashDot,
        Thick,
        Medium,
        Double,
    }
}