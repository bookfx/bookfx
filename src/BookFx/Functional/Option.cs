namespace BookFx.Functional
{
    /// <summary>
    /// Is a container for the <see cref="None"/> and the <see cref="Some{T}"/> types.
    /// </summary>
    public static class Option
    {
        /// <summary>
        /// There is no value.
        /// </summary>
        public struct None
        {
            internal static readonly None Default = default;
        }

        /// <summary>
        /// Some value.
        /// </summary>
        public struct Some<T>
        {
            internal Some(T value) => Value = value;

            internal T Value { get; }
        }
    }
}