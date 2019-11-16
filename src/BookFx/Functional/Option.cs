namespace BookFx.Functional
{
    using System;

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
            internal Some(T value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException(
                        nameof(value),
                        "Cannot wrap a null value in a «Some»; use «None» instead.");
                }

                Value = value;
            }

            internal T Value { get; }
        }
    }
}