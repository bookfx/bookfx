namespace BookFx
{
    internal static class BorderPartsExt
    {
        public static bool IsSupersetOf(this BorderParts @this, BorderParts others) => (@this & others) == others;
    }
}