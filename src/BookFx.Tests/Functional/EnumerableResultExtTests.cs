namespace BookFx.Tests.Functional
{
    using System.Collections.Generic;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using Xunit;
    using static System.Linq.Enumerable;
    using static BookFx.Functional.F;

    public class EnumerableResultExtTests
    {
        [Property]
        public void TraverseA_ManyValid_ValidMany(NonEmptyArray<int> many) =>
            many.Get.TraverseA(Valid).ValueUnsafe().Should().Equal(many.Get);

        [Property]
        public void TraverseA_ManyInvalid_Invalid(NonEmptyArray<int> many) =>
            many.Get.TraverseA(_ => Invalid<int>()).Should().Be(Invalid<IEnumerable<int>>());

        [Property]
        public void TraverseA_ManyInvalid_InvalidWithManyErrors(NonEmptyArray<int> many) =>
            many.Get
                .TraverseA(x => Invalid<int>(x.ToString()))
                .ErrorsUnsafe()
                .Map(x => x.ToString())
                .Should()
                .Equal(many.Get.Map(x => x.ToString()));

        [Fact]
        public void TraverseA_EmptyValid_ValidEmpty() => Empty<int>().TraverseA(Valid).Should().Be(Valid(Empty<int>()));

        [Fact]
        public void TraverseA_EmptyInvalid_ValidEmpty() =>
            Empty<int>().TraverseA(_ => Invalid<int>()).Should().Be(Valid(Empty<int>()));

        [Property]
        public void TraverseM_ManyValid_ValidMany(NonEmptyArray<int> many) =>
            many.Get.TraverseM(Valid).ValueUnsafe().Should().Equal(many.Get);

        [Property]
        public void TraverseM_ManyInvalid_Invalid(NonEmptyArray<int> many) =>
            many.Get.TraverseM(_ => Invalid<int>()).Should().Be(Invalid<IEnumerable<int>>());

        [Property]
        public void TraverseM_ManyInvalid_InvalidWithFirstError(NonEmptyArray<int> many) =>
            many.Get
                .TraverseM(x => Invalid<int>(x.ToString()))
                .ErrorsUnsafe()
                .Map(x => x.ToString())
                .Should()
                .Equal(many.Get.Take(1).Map(x => x.ToString()));

        [Fact]
        public void TraverseM_EmptyValid_ValidEmpty() => Empty<int>().TraverseM(Valid).Should().Be(Valid(Empty<int>()));

        [Fact]
        public void TraverseM_EmptyInvalid_ValidEmpty() =>
            Empty<int>().TraverseM(_ => Invalid<int>()).Should().Be(Valid(Empty<int>()));

        [Property]
        public void HarvestErrors_ManyValid_ValidMany(NonEmptyArray<int> many) =>
            many.Get.Map(Valid).HarvestErrors().ValueUnsafe().Should().Equal(many.Get);

        [Property]
        public void HarvestErrors_ManyInvalid_Invalid(NonEmptyArray<int> many) =>
            many.Get.Map(_ => Invalid<int>()).HarvestErrors().Should().Be(Invalid<IEnumerable<int>>());

        [Property]
        public void HarvestErrors_ManyInvalid_InvalidWithManyErrors(NonEmptyArray<int> many) =>
            many.Get
                .Map(x => Invalid<int>(x.ToString()))
                .HarvestErrors()
                .ErrorsUnsafe()
                .Map(x => x.ToString())
                .Should()
                .Equal(many.Get.Map(x => x.ToString()));

        [Fact]
        public void HarvestErrors_EmptyValid_ValidEmpty() =>
            Empty<int>().Map(Valid).HarvestErrors().Should().Be(Valid(Empty<int>()));

        [Fact]
        public void HarvestErrors_EmptyInvalid_ValidEmpty() =>
            Empty<int>().Map(_ => Invalid<int>()).HarvestErrors().Should().Be(Valid(Empty<int>()));

        [Property]
        public void FailFast_ManyValid_ValidMany(NonEmptyArray<int> many) =>
            many.Get.Map(Valid).FailFast().ValueUnsafe().Should().Equal(many.Get);

        [Property]
        public void FailFast_ManyInvalid_Invalid(NonEmptyArray<int> many) =>
            many.Get.Map(_ => Invalid<int>()).FailFast().Should().Be(Invalid<IEnumerable<int>>());

        [Property]
        public void FailFast_ManyInvalid_InvalidWithFirstError(NonEmptyArray<int> many) =>
            many.Get
                .Map(x => Invalid<int>(x.ToString()))
                .FailFast()
                .ErrorsUnsafe()
                .Map(x => x.ToString())
                .Should()
                .Equal(many.Get.Take(1).Map(x => x.ToString()));

        [Fact]
        public void FailFast_EmptyValid_ValidEmpty() =>
            Empty<int>().Map(Valid).FailFast().Should().Be(Valid(Empty<int>()));

        [Fact]
        public void FailFast_EmptyInvalid_ValidEmpty() =>
            Empty<int>().Map(_ => Invalid<int>()).FailFast().Should().Be(Valid(Empty<int>()));
    }
}