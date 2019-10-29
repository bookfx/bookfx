namespace BookFx
{
    using JetBrains.Annotations;

    [PublicAPI]
    public struct Reference
    {
        private readonly string _value;

        private Reference(string value) => _value = value;

        public static implicit operator Reference(string reference) => new Reference(reference);

        /// <summary>
        /// Create a reference from book scope name.
        /// </summary>
        /// <param name="reference">Only book scope names are supported.</param>
        /// <returns>Reference.</returns>
        public static Reference From(string reference) => new Reference(reference);

        public override string ToString() => _value;
    }
}