namespace BookFx
{
    using BookFx.Cores;
    using JetBrains.Annotations;

    [PublicAPI]
    public abstract class CompositeBox : Box
    {
        private protected CompositeBox(BoxCore core)
            : base(core)
        {
        }
    }
}