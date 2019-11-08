namespace BookFx.Tests.Calculation
{
    using System;
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Functional;
    using FluentAssertions;
    using Xunit;

    public class CacheTests
    {
        [Fact]
        public void GetOrCompute_InCache_ResultFromCache()
        {
            const int expected = 3;
            var (box, boxCount) = Make.Value().Get.Number();
            var cache = Cache.Create(boxCount);
            cache.MinHeight(box, () => expected);

            var result = cache.MinHeight(box, Throw);

            result.Should().Be(expected);
        }

        [Fact]
        public void GetOrCompute_EmptyCache_ComputedResult()
        {
            const int expected = 3;
            var (box, boxCount) = Make.Value().SpanRows(expected).Get.Number();
            var cache = Cache.Create(boxCount);

            var result = cache.MinHeight(box, () => RowSpan(box));

            result.Should().Be(expected);
        }

        [Fact]
        public void GetOrCompute_EmptyCache_ResultInCache()
        {
            const int expected = 3;
            var (box, boxCount) = Make.Value().SpanRows(expected).Get.Number();
            var cache = Cache.Create(boxCount);

            cache.MinHeight(box, () => RowSpan(box));

            cache.MinHeight(box, Throw).Should().Be(expected);
        }

        private static int Throw() =>
            throw new InvalidOperationException();

        private static int RowSpan(BoxCore box) =>
            box.RowSpan.GetOrElse(1);
    }
}