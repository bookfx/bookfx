namespace BookFx.Tests.Functional
{
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;

    public class ErrorTests
    {
        [Property]
        public void Equals_Same_True(NonNull<string> message, NonNull<string>[] inners)
        {
            var error1 = CreateError(message, inners);
            var error2 = error1;

            var result = error1.Equals(error2);

            result.Should().BeTrue();
        }

        [Property]
        public void Equals_Like_True(NonNull<string> message, NonNull<string>[] inners)
        {
            var error1 = CreateError(message, inners);
            var error2 = CreateError(message, inners);

            var result = error1.Equals(error2);

            result.Should().BeTrue();
        }

        private static Error CreateError(NonNull<string> message, NonNull<string>[] inners) =>
            new Error(message.Get, inners.Map(x => new Error(x.Get)));
    }
}