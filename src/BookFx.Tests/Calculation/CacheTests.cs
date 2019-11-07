namespace BookFx.Tests.Calculation
{
    using System;
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Functional;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.Sc<BookFx.Calculation.Cache>;

    public class CacheTests
    {
        private const Measure Measure = BookFx.Calculation.Measure.MinHight;

        [Fact]
        public void GetOrCompute_InCache_ResultFromCache()
        {
            const int expected = 3;
            var (box, boxCount) = Make.Value().Get.Number();
            var key = (box, Measure);
            var cache = Cache.Create(boxCount).GetOrCompute(key, () => ScOf(expected)).Cache;

            var result = cache.GetOrCompute(key, ThrowSc).Result;

            result.Should().Be(expected);
        }

        [Fact]
        public void GetOrCompute_InCache_NoCacheChanges()
        {
            var (box, boxCount) = Make.Value().Get.Number();
            var key = (box, Measure);
            var cache = Cache.Create(boxCount).GetOrCompute(key, () => ScOf(3)).Cache;

            var newCache = cache.GetOrCompute(key, ThrowSc).Cache;

            newCache.Should().BeSameAs(cache);
        }

        [Fact]
        public void GetOrCompute_EmptyCache_ComputedResult()
        {
            const int expected = 3;
            var (box, boxCount) = Make.Value().SpanRows(expected).Get.Number();
            var key = (box, Measure);
            var cache = Cache.Create(boxCount);

            var result = cache.GetOrCompute(key, () => RowSpanSc(box)).Result;

            result.Should().Be(expected);
        }

        [Fact]
        public void GetOrCompute_EmptyCache_ResultInCache()
        {
            const int expected = 3;
            var (box, boxCount) = Make.Value().SpanRows(expected).Get.Number();
            var key = (box, Measure);
            var cache = Cache.Create(boxCount);

            var newCache = cache.GetOrCompute(key, () => RowSpanSc(box)).Cache;

            newCache.GetOrCompute(key, ThrowSc).Result.Should().Be(expected);
        }

        private static Sc<Cache, int> ThrowSc() =>
            throw new InvalidOperationException();

        private static Sc<Cache, int> RowSpanSc(BoxCore box) =>
            ScOf(box.RowSpan.GetOrElse(1));
    }
}