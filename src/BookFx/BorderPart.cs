namespace BookFx
{
    using System;

    [Flags]
    public enum BorderPart : byte
    {
        InsideTop = 1 << 0,
        InsideRight = 1 << 1,
        InsideBottom = 1 << 2,
        InsideLeft = 1 << 3,

        OutsideTop = 1 << 4,
        OutsideRight = 1 << 5,
        OutsideBottom = 1 << 6,
        OutsideLeft = 1 << 7,

        InsideHorizontal = InsideTop | InsideBottom,
        InsideVertical = InsideRight | InsideLeft,
        Inside = InsideHorizontal | InsideVertical,

        OutsideHorizontal = OutsideTop | OutsideBottom,
        OutsideVertical = OutsideRight | OutsideLeft,
        Outside = OutsideHorizontal | OutsideVertical,

        Top = InsideTop | OutsideTop,
        Right = InsideRight | OutsideRight,
        Bottom = InsideBottom | OutsideBottom,
        Left = InsideLeft | OutsideLeft,

        Horizontal = Top | Bottom,
        Vertical = Right | Left,

        All = Inside | Outside,
    }
}