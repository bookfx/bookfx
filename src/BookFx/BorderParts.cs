namespace BookFx
{
    using System;

    /// <summary>
    /// Parts of a box to whom the border applied. This is a flags enum.
    /// </summary>
    [Flags]
    public enum BorderParts
    {
        /// <summary>
        /// Inside top part.
        /// </summary>
        InsideTop = 1,

        /// <summary>
        /// Inside right part.
        /// </summary>
        InsideRight = 1 << 1,

        /// <summary>
        /// Inside bottom part.
        /// </summary>
        InsideBottom = 1 << 2,

        /// <summary>
        /// Inside left part.
        /// </summary>
        InsideLeft = 1 << 3,

        /// <summary>
        /// Outside top part.
        /// </summary>
        OutsideTop = 1 << 4,

        /// <summary>
        /// Outside right part.
        /// </summary>
        OutsideRight = 1 << 5,

        /// <summary>
        /// Outside bottom part.
        /// </summary>
        OutsideBottom = 1 << 6,

        /// <summary>
        /// Outside left part.
        /// </summary>
        OutsideLeft = 1 << 7,

        /// <summary>
        /// Inside horizontal parts.
        /// </summary>
        InsideHorizontal = InsideTop | InsideBottom,

        /// <summary>
        /// Inside vertical parts.
        /// </summary>
        InsideVertical = InsideRight | InsideLeft,

        /// <summary>
        /// Inside parts.
        /// </summary>
        Inside = InsideHorizontal | InsideVertical,

        /// <summary>
        /// Outside horizontal parts.
        /// </summary>
        OutsideHorizontal = OutsideTop | OutsideBottom,

        /// <summary>
        /// Outside vertical parts.
        /// </summary>
        OutsideVertical = OutsideRight | OutsideLeft,

        /// <summary>
        /// Outside parts.
        /// </summary>
        Outside = OutsideHorizontal | OutsideVertical,

        /// <summary>
        /// Top parts.
        /// </summary>
        Top = InsideTop | OutsideTop,

        /// <summary>
        /// Right parts.
        /// </summary>
        Right = InsideRight | OutsideRight,

        /// <summary>
        /// Bottom parts.
        /// </summary>
        Bottom = InsideBottom | OutsideBottom,

        /// <summary>
        /// Left parts.
        /// </summary>
        Left = InsideLeft | OutsideLeft,

        /// <summary>
        /// Horizontal parts.
        /// </summary>
        Horizontal = Top | Bottom,

        /// <summary>
        /// Vertical parts.
        /// </summary>
        Vertical = Right | Left,

        /// <summary>
        /// All parts.
        /// </summary>
        All = Inside | Outside,
    }
}