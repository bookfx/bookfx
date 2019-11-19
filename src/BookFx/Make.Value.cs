namespace BookFx
{
    using System;
    using BookFx.Cores;
    using JetBrains.Annotations;

    /// <summary>
    /// <inheritdoc cref="Make"/>
    /// </summary>
    public static partial class Make
    {
        /// <summary>
        /// Creates an empty <see cref="ValueBox"/>. The box content will not be set.
        /// </summary>
        [Pure]
        public static ValueBox Value() => ValueBox.Empty;

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="ValueTuple"/>. The box content will be cleared.
        /// </summary>
        /// <param name="content"><see cref="ValueTuple"/>.</param>
        [Pure]
        public static ValueBox Value(ValueTuple content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="string"/> or formula.
        /// </summary>
        /// <param name="content"> Value of <see cref="string"/> or formula. The formula must begin with '='. </param>
        [Pure]
        public static ValueBox Value(string content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="bool"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="bool"/>.</param>
        [Pure]
        public static ValueBox Value(bool content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="byte"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="byte"/>.</param>
        [Pure]
        public static ValueBox Value(byte content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="sbyte"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="sbyte"/>.</param>
        [Pure]
        public static ValueBox Value(sbyte content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="short"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="short"/>.</param>
        [Pure]
        public static ValueBox Value(short content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="ushort"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="ushort"/>.</param>
        [Pure]
        public static ValueBox Value(ushort content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="int"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="int"/>.</param>
        [Pure]
        public static ValueBox Value(int content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="uint"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="uint"/>.</param>
        [Pure]
        public static ValueBox Value(uint content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="long"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="long"/>.</param>
        [Pure]
        public static ValueBox Value(long content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="ulong"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="ulong"/>.</param>
        [Pure]
        public static ValueBox Value(ulong content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="float"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="float"/>.</param>
        [Pure]
        public static ValueBox Value(float content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="double"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="double"/>.</param>
        [Pure]
        public static ValueBox Value(double content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="decimal"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="decimal"/>.</param>
        [Pure]
        public static ValueBox Value(decimal content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="DateTime"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="DateTime"/>.</param>
        [Pure]
        public static ValueBox Value(DateTime content) => BoxCore.Create(type: BoxType.Value, value: content);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="Guid"/>.
        /// </summary>
        /// <param name="content">Value of <see cref="Guid"/>.</param>
        [Pure]
        public static ValueBox Value(Guid content) => BoxCore.Create(type: BoxType.Value, value: content);
    }
}