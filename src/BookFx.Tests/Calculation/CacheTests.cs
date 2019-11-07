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
        private const Measure Measure = BookFx.Calculation.Measure.MinHight;

        [Fact]
        public void GetOrCompute_InCache_ResultFromCache()
        {
            const int expected = 3;
            var (box, boxCount) = Make.Value().Get.Number();
            var key = (box, Measure);
            var cache = Cache.Create(boxCount);
            cache.GetOrCompute(key, () => expected);

            var result = cache.GetOrCompute(key, Throw);

            result.Should().Be(expected);
        }

        [Fact]
        public void GetOrCompute_EmptyCache_ComputedResult()
        {
            const int expected = 3;
            var (box, boxCount) = Make.Value().SpanRows(expected).Get.Number();
            var key = (box, Measure);
            var cache = Cache.Create(boxCount);

            var result = cache.GetOrCompute(key, () => RowSpan(box));

            result.Should().Be(expected);
        }

        [Fact]
        public void GetOrCompute_EmptyCache_ResultInCache()
        {
            const int expected = 3;
            var (box, boxCount) = Make.Value().SpanRows(expected).Get.Number();
            var key = (box, Measure);
            var cache = Cache.Create(boxCount);

            cache.GetOrCompute(key, () => RowSpan(box));

            cache.GetOrCompute(key, Throw).Should().Be(expected);
        }

        private static int Throw() =>
            throw new InvalidOperationException();

        private static int RowSpan(BoxCore box) =>
            box.RowSpan.GetOrElse(1);
    }
}