namespace BookFx.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Tests.Arbitraries;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using Xunit;
    using static BookFx.Functional.F;

    [Properties(Arbitrary = new[] { typeof(ValidColSizeArb) })]
    public class TrackSizesExtTests
    {
        private static readonly TrackSize Some = TrackSize.Some(10);

        private static IEnumerable<TrackSize> Empty => Enumerable.Empty<TrackSize>();

        [Property]
        public void TakeExact_CountIsLength_Same(TrackSize[] sizes) =>
            sizes.TakeExact(sizes.Length).Should().Equal(sizes);

        [Property]
        public void TakeExact_CountIsZero_Empty(TrackSize[] sizes) => sizes.TakeExact(0).Should().BeEmpty();

        [Property]
        public void TakeExact_Empty_HaveCountOfNone(NonNegativeInt count) =>
            Empty.TakeExact(count.Get).Should().Equal(Enumerable.Repeat(TrackSize.None, count.Get));

        [Fact]
        public void Complement_EmptyToEmpty_Empty() => Empty.Complement(Empty).Should().HaveCount(0);

        [Fact]
        public void Complement_SomeToNone_Some() =>
            List(Some).Complement(List(TrackSize.None)).Should().Equal(Some);

        [Fact]
        public void Complement_FitToNone_Fit() =>
            List(TrackSize.Fit).Complement(List(TrackSize.None)).Should().Equal(TrackSize.Fit);

        [Fact]
        public void Complement_NoneToFit_Fit() =>
            List(TrackSize.None).Complement(List(TrackSize.Fit)).Should().Equal(TrackSize.Fit);

        [Fact]
        public void Complement_NoneToSome_Some() =>
            List(TrackSize.None).Complement(List(Some)).Should().Equal(Some);
    }
}