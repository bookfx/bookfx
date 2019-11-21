namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Is a container for the <see cref="None"/> and the <see cref="Some{T}"/> types.
    /// </summary>
    public static class Option
    {
        /// <summary>
        /// There is no value.
        /// </summary>
        public struct None : IEquatable<None>
        {
            internal static readonly None Default = default;

            /// <summary>
            /// The equality operator. Always true.
            /// </summary>
            public static bool operator ==(None left, None right) => true;

            /// <summary>
            /// The inequality operator. Always false.
            /// </summary>
            public static bool operator !=(None left, None right) => false;

            /// <inheritdoc />
            public bool Equals(None other) => true;

            /// <inheritdoc />
            public override bool Equals(object obj) => obj is None;

            /// <inheritdoc />
            public override int GetHashCode() => 0;
        }

        /// <summary>
        /// Some value.
        /// </summary>
        public struct Some<T> : IEquatable<Some<T>>
        {
            internal Some(T value) => Value = value;

            internal T Value { get; }

            /// <summary>
            /// Equality operator.
            /// </summary>
            public static bool operator ==(Some<T> left, Some<T> right) => left.Equals(right);

            /// <summary>
            /// Inequality operator.
            /// </summary>
            public static bool operator !=(Some<T> left, Some<T> right) => !(left == right);

            /// <inheritdoc />
            public bool Equals(Some<T> other) => EqualityComparer<T>.Default.Equals(Value, other.Value);

            /// <inheritdoc />
            public override bool Equals(object obj) => obj is Some<T> other && Equals(other);

            /// <inheritdoc />
            public override int GetHashCode() => EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }
}