namespace BookFx.Functional
{
    using System;

    public static class Option
    {
        public struct None
        {
            internal static readonly None Default = default;
        }

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