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
        /// Creates an empty <see cref="ValueBox"/> with style. The box content will not be set.
        /// </summary>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(BoxStyle style) => ValueBox.Empty.Style(style);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="ValueTuple"/> and style. The box content will be cleared.
        /// </summary>
        /// <param name="content"><see cref="ValueTuple"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(ValueTuple content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="string"/> or formula and style.
        /// </summary>
        /// <param name="content">Value of <see cref="string"/> or formula. The formula must begin with '='.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(string content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="bool"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="bool"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(bool content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="byte"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="byte"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(byte content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="sbyte"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="sbyte"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(sbyte content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="short"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="short"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(short content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="ushort"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="ushort"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(ushort content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="int"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="int"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(int content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with <see cref="uint"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="uint"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(uint content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="long"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="long"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(long content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="ulong"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="ulong"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(ulong content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="float"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="float"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(float content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="double"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="double"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(double content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="decimal"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="decimal"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(decimal content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="DateTime"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="DateTime"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(DateTime content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);

        /// <summary>
        /// Creates a <see cref="ValueBox"/> with long <see cref="Guid"/> and style.
        /// </summary>
        /// <param name="content">Value of <see cref="Guid"/>.</param>
        /// <param name="style">An instance of <see cref="BoxStyle"/>.</param>
        [Pure]
        public static ValueBox Value(Guid content, BoxStyle style) =>
            BoxCore.Create(type: BoxType.Value, value: content, style: style.Get);
    }
}