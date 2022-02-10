namespace BookFx
{
    using System;
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
        /// The default <see cref="BoxBorder"/>. All border parts are thin.
        /// </summary>
        public static readonly BoxBorder Default = BoxBorderCore.Default;

        /// <summary>
        /// The empty <see cref="BoxBorder"/>.
        /// </summary>
        [Obsolete("Use Default instead.")]
        public static readonly BoxBorder Empty = BoxBorderCore.Default;

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
        /// Restrict parts of a box to whom the border applied.
        /// </summary>
        /// <param name="borderParts">Parts of a box. This is a flags enum.</param>
        [Pure]
        public BoxBorder Restrict(BorderParts borderParts) => Get.With(parts: borderParts);

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