namespace BookFx.Functional
{
    internal static class ActExt
    {
        public static Tee<T> ToTee<T>(this Act<T> act) => x => act(x).Map(_ => x);
    }
}