namespace BookFx
{
    using JetBrains.Annotations;

    /// <summary>
    /// Box type.
    /// </summary>
    [PublicAPI]
    public enum BoxType
    {
        /// <summary>
        /// A box with a value, with a formula or an empty box.
        /// </summary>
        Value,

        /// <summary>
        /// One of the composite <see cref="Box"/> types. Inner boxes are placed in row from left to right.
        /// </summary>
        Row,

        /// <summary>
        /// One of the composite <see cref="Box"/> types. Inner boxes are placed in column from top to bottom.
        /// </summary>
        Col,

        /// <summary>
        /// One of the composite <see cref="Box"/> types. Inner boxes are placed in stack one above the other.
        /// </summary>
        Stack,

        /// <summary>
        /// A prototype. Inner boxes are placed in slots.
        /// </summary>
        Proto,
    }
}