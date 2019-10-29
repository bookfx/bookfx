namespace BookFx
{
    using BookFx.Cores;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    /// <summary>
    /// Sheet is a class to describe worksheet.
    /// </summary>
    [PublicAPI]
    public sealed class Sheet
    {
        public static readonly Sheet Empty = SheetCore.Empty;

        private Sheet(SheetCore core) => Get = core;

        public SheetCore Get { get; }

        [Pure]
        public static implicit operator Sheet(SheetCore core) => new Sheet(core);

        [Pure]
        public Sheet Name(string name) => Get.With(name: Some(name));

        [Pure]
        public Sheet SetPageView(PageView pageView) => Get.With(pageView: pageView);

        [Pure]
        public Sheet Scale(int scale) => Get.With(scale: scale);

        [Pure]
        public Book ToBook() => Make.Book(this);
    }
}