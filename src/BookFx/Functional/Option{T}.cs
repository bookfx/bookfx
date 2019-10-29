namespace BookFx.Functional
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using static F;

    public struct Option<T> : IEquatable<Option.None>, IEquatable<Option<T>>
    {
        private readonly T _value;

        private Option(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            IsSome = true;
            _value = value;
        }

        public bool IsSome { get; }

        public bool IsNone => !IsSome;

        public static implicit operator Option<T>(Option.None none) => default;

        public static implicit operator Option<T>(Option.Some<T> some) => new Option<T>(some.Value);

        public static implicit operator Option<T>(T value) => value == null ? None : Some(value);

        public static bool operator ==(Option<T> @this, Option<T> other) => @this.Equals(other);

        public static bool operator !=(Option<T> @this, Option<T> other) => !(@this == other);

        public TR Match<TR>(Func<TR> none, Func<T, TR> some) => IsSome ? some(_value) : none();

        [Pure]
        public IEnumerable<T> AsEnumerable()
        {
            if (IsSome)
            {
                yield return _value;
            }
        }

        public override bool Equals(object obj) => obj is Option<T> other && Equals(other);

        public override int GetHashCode() => IsSome ? _value!.GetHashCode() : 0;

        public bool Equals(Option.None none) => IsNone;

        public bool Equals(Option<T> other) =>
            IsSome == other.IsSome &&
            (IsNone || _value!.Equals(other._value));

        public override string ToString() =>
            Match(
                none: () => "None",
                some: value => $"Some({value})");
    }
}