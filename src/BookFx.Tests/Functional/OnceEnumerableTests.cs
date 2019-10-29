namespace BookFx.Tests.Functional
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using Xunit;

    public class OnceEnumerableTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void Enumerate_Once_NoExceptions(int size) => new OnceEnumerable(size).Should().HaveCount(size);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void Enumerate_Twice_Exception(int size)
        {
            var enumerable = new OnceEnumerable(size);
            // ReSharper disable ReturnValueOfPureMethodIsNotUsed
            enumerable.FirstOrDefault();

            Action action = () => enumerable.FirstOrDefault();
            // ReSharper restore ReturnValueOfPureMethodIsNotUsed

            action.Should().Throw<InvalidOperationException>();
        }
    }
}