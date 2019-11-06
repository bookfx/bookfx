namespace BookFx.Tests.Functional
{
    using System.Linq;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using Xunit;
    using static BookFx.Functional.F;

    public class EnumerableExtTests
    {
        [Fact]
        public void Bind_NoneOnly_Empty() =>
            Enumerable.Range(1, 3).Bind<int, Option<int>>(_ => None).Should().BeEmpty();

        [Fact]
        public void Bind_Some_NonEmpty() =>
            Enumerable.Range(1, 3).Bind<int, Option<int>>(x => Some(x)).Should().NotBeEmpty();

        [Property]
        public void Match2_HeadWithTail_Equal(int[] list) =>
            list
                .Match(
                    empty: Enumerable.Empty<int>,
                    more: (x, xs) => xs.Prepend(x))
                .Should()
                .Equal(list);

        [Property]
        public void Match2_MapAndHeadWithTail_Equal(int[] list) =>
            list
                .Map(x => x)
                .Match(
                    empty: Enumerable.Empty<int>,
                    more: (x, xs) => xs.Prepend(x))
                .Should()
                .Equal(list);

        [Property]
        public void Match3_HeadWithTail_Equal(int[] list) =>
            list
                .Match(
                    empty: Enumerable.Empty<int>,
                    one: x => new[] { x },
                    more: (x, xs) => xs.Prepend(x))
                .Should()
                .Equal(list);

        [Property]
        public void Match3_MapAndHeadWithTail_Equal(int[] list) =>
            list
                .Map(x => x)
                .Match(
                    empty: Enumerable.Empty<int>,
                    one: x => new[] { x },
                    more: (x, xs) => xs.Prepend(x))
                .Should()
                .Equal(list);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void Match2_OnceEnumerable_NoExceptions(int size) =>
            new OnceEnumerable(size).Match(
                    empty: Enumerable.Empty<int>,
                    more: (x, xs) => xs.Prepend(x))
                .Should()
                .HaveCount(size);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void Match3_OnceEnumerable_NoExceptions(int size) =>
            new OnceEnumerable(size).Match(
                    empty: Enumerable.Empty<int>,
                    one: x => new[] { x },
                    more: (x, xs) => xs.Prepend(x))
                .Should()
                .HaveCount(size);

        [Property]
        public void Find_NonEmptyStrings_First(NonEmptyArray<NonNull<string>> list) =>
            list.Get.Map(x => x.Get).Find(_ => true).Should().Be(Some(list.Get[0].Get));

        [Property]
        public void Find_NonEmptyDecimals_First(NonEmptyArray<decimal> list) =>
            list.Get.Find(_ => true).Should().Be(Some(list.Get[0]));

        [Fact]
        public void Find_EmptyStrings_None() =>
            Enumerable.Empty<string>().Find(_ => true).Should().Be((Option<string>)None);

        [Fact]
        public void Find_EmptyDecimals_None() =>
            Enumerable.Empty<decimal>().Find(_ => true).Should().Be((Option<decimal>)None);

        [Property]
        public void Neighbors_Always_PrevsAreListWithoutLast(object[] list) =>
            list.Neighbors().Map(x => x.Prev).Should().BeEquivalentTo(list.Take(list.Length - 1));

        [Property]
        public void Neighbors_Always_NextsAreListTail(object[] list) =>
            list.Neighbors().Map(x => x.Next).Should().BeEquivalentTo(list.Skip(1));
    }
}