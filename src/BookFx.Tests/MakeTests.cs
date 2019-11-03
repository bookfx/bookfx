namespace BookFx.Tests
{
    using System.Collections.Generic;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck.Xunit;
    using Xunit;

    public class MakeTests
    {
        [Property]
        public void Value_MethodGroupWithInt_OverloadFound(int[] ints) =>
            ints.Map(Make.Value).Should().BeAssignableTo<IEnumerable<ValueBox>>();
    }
}