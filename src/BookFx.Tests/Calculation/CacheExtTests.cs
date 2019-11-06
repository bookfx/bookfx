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

            var result = cache.GetOrCompute(key, ThrowComputation).Run(cache);

            result.Should().Be(expected);
        }

        [Fact]
        public void GetOrCompute_InCache_NoCacheChanges()
        {
            var box = Make.Value().Get;
            var key = (box, Measure);
            var cache = Cache.Empty.Add(key, 3);

            var computation =
                from unused in cache.GetOrCompute(key, ThrowComputation)
                from result in Sc<Cache>.Get
                select result;
            var newCache = computation.Run(cache);

            newCache.Should().BeSameAs(cache);
        }

        [Fact]
        public void GetOrCompute_EmptyCache_ComputedResult()
        {
            const int expected = 3;
            var box = Make.Value().SpanRows(expected).Get;
            var key = (box, Measure);
            var cache = Cache.Empty;

            var result = cache.GetOrCompute(key, () => RowSpanComputation(box, Measure)).Run(cache);

            result.Should().Be(expected);
        }

        [Fact]
        public void GetOrCompute_EmptyCache_ResultInCache()
        {
            const int expected = 3;
            var box = Make.Value().SpanRows(expected).Get;
            var key = (box, Measure);
            var cache = Cache.Empty;

            var computation =
                from unused in cache.GetOrCompute(key, () => RowSpanComputation(box, Measure))
                from result in Sc<Cache>.Get
                select result;
            var newCache = computation.Run(cache);

            newCache.TryGetValue(key).Should().Be(Some(expected));
        }

        private static Sc<Cache, int> ThrowComputation() =>
            throw new InvalidOperationException();

        private static Sc<Cache, int> RowSpanComputation(BoxCore box, Measure measure) =>
            cache => (box.RowSpan.ValueUnsafe(), cache.Add((box, measure), box.RowSpan.ValueUnsafe()));
    }
}