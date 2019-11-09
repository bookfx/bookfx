namespace BookFx.Calculation
{
    using BookFx.Cores;

    internal class Layout
    {
        private Layout(Relations relations, Cache cache)
        {
            Relations = relations;
            Cache = cache;
        }

        public Relations Relations { get; }

        public Cache Cache { get; }

        public static Layout Create(BoxCore box, int boxCount) =>
            new Layout(Relations.Create(box), Cache.Create(boxCount));
    }
}