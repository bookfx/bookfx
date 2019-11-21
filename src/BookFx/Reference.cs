namespace BookFx
{
    using System;
    using JetBrains.Annotations;

    /// <summary>
    /// Represents a sheet reference.
    /// </summary>
    [PublicAPI]
    public struct Reference : IEquatable<Reference>
    {
        private readonly string _value;

        private Reference(string value) => _value = value;

        /// <summary>
        /// Implicit convert from <see cref="string"/> to <see cref="Reference"/>.
        /// </summary>
        public static implicit operator Reference(string reference) => new Reference(reference);

        /// <summary>
        /// Equality operator.
        /// </summary>
        public static bool operator ==(Reference left, Reference right) => left.Equals(right);

        /// <summary>
        /// Inequality operator.
        /// </summary>
        public static bool operator !=(Reference left, Reference right) => !(left == right);

        /// <summary>
        /// Create a reference from book scope name.
        /// </summary>
        /// <param name="reference">Only book scope names are supported.</param>
        /// <returns>Reference.</returns>
        public static Reference From(string reference) => new Reference(reference);

        /// <inheritdoc />
        public override string ToString() => _value;

        /// <inheritdoc />
        public bool Equals(Reference other) => _value == other._value;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is Reference other && Equals(other);

        /// <inheritdoc />
        public override int GetHashCode() => _value.GetHashCode();
    }
}