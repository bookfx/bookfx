namespace BookFx
{
    using JetBrains.Annotations;

    [PublicAPI]
    public enum BoxType : byte
    {
        Value,
        Row,
        Col,
        Stack,
        Proto,
    }
}