namespace BookFx
{
    using System.Drawing;
    using BookFx.Cores;
    using JetBrains.Annotations;

    /// <summary>
    /// A box border.
    /// </summary>
    [PublicAPI]
    public sealed class BoxBorder
    {
        /// <summary>
        /// The empty <see cref="BoxBorder"/>.
        /// </summary>
        public static readonly BoxBorder Empty = BoxBorderCore.Empty;

        private BoxBorder(BoxBorderCore core) => Get = core;

        /// <summary>
        /// Gets properties of the border.
        /// </summary>
        public BoxBorderCore Get { get; }

        /// <summary>
        /// Implicit convert from <see cref="BoxBorderCore"/> to <see cref="BoxBorder"/>.
        /// </summary>
        [Pure]
        public static implicit operator BoxBorder(BoxBorderCore core) => new BoxBorder(core);

        /// <summary>
        /// Restrict a part of a box to which the border applied.
        /// </summary>
        /// <param name="borderPart">A part of a box.</param>
        [Pure]
        public BoxBorder Restrict(BorderPart borderPart) => Get.With(part: borderPart);

        /// <summary>
        /// Define a border style.
        /// </summary>
        /// <param name="borderStyle">A border style.</param>
        [Pure]
        public BoxBorder Style(BorderStyle borderStyle) => Get.With(style: borderStyle);

        /// <summary>
        /// Define a border color.
        /// </summary>
        /// <param name="borderColor">A border color.</param>
        [Pure]
        public BoxBorder Color(Color borderColor) => Get.With(color: borderColor);
    }
}