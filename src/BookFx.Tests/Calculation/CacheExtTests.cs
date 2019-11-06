namespace BookFx.Tests.Calculation
{
    using System;
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Functional;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;

    public class CacheExtTests
    {
        private const Measure Measure = BookFx.Calculation.Measure.MinHight;

        [Fact]
        public void GetOrCompute_InCache_ResultFromCache()
        {
            const int expected = 3;
            var box = Make.Value().Get;
            var key = (box, Measure);
            var cache = Cache.Empty.Add(key, expected);

            var result = cache.GetOrCompute(key, ThrowSc).Run(cache);

            result.Should().Be(expected);
        }

        [Fact]
        public void GetOrCompute_InCache_NoCacheChanges()
        {
            var box = Make.Value().Get;
            var key = (box, Measure);
            var cache = Cache.Empty.Add(key, 3);
            var sc =
                from unused in cache.GetOrCompute(key, ThrowSc)
                from result in Sc<Cache>.Get
                select result;

            var newCache = sc.Run(cache);

            newCache.Should().BeSameAs(cache);
        }

        [Fact]
        public void GetOrCompute_EmptyCache_ComputedResult()
        {
            const int expected = 3;
            var box = Make.Value().SpanRows(expected).Get;
            var key = (box, Measure);
            var cache = Cache.Empty;

            var result = cache.GetOrCompute(key, () => RowSpanSc(box)).Run(cache);

            result.Should().Be(expected);
        }

        [Fact]
        public void GetOrCompute_EmptyCache_ResultInCache()
        {
            const int expected = 3;
            var box = Make.Value().SpanRows(expected).Get;
            var key = (box, Measure);
            var cache = Cache.Empty;
            var sc =
                from unused in cache.GetOrCompute(key, () => RowSpanSc(box))
                from result in Sc<Cache>.Get
                select result;

            var newCache = sc.Run(cache);

            newCache.TryGetValue(key).Should().Be(Some(expected));
        }

        private static Sc<Cache, int> ThrowSc() =>
            throw new InvalidOperationException();

        private static Sc<Cache, int> RowSpanSc(BoxCore box) =>
            Sc<Cache>.Return(box.RowSpan.GetOrElse(1));
    }
}