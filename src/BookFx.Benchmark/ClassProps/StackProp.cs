namespace BookFx.Benchmark.ClassProps
{
    using System;

    public struct StackProp : IEquatable<StackProp>
    {
        public StackProp(StackPropKey key, object value)
        {
            Key = key;
            Value = value;
        }

        public StackPropKey Key { get; }

        public object Value { get; }

        public static bool operator ==(StackProp left, StackProp right) => left.Equals(right);

        public static bool operator !=(StackProp left, StackProp right) => !left.Equals(right);

        public bool Equals(StackProp other) => Key == other.Key && Value.Equals(other.Value);

        public override bool Equals(object? obj) => obj is StackProp other && Equals(other);

        public override int GetHashCode() => ((int)Key * 397) ^ Value.GetHashCode();
    }
}