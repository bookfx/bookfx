namespace BookFx.Tests.Functional
{
    using System;
    using System.Collections.Immutable;
    using System.Linq;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck.Xunit;

    public class ScCompositionTests
    {
        [Property]
        public void Compose_MaxOfReturns_Expected(int[] list, object state) =>
            list
                .Map(Sc<object>.Return)
                .Compose(int.MinValue, Math.Max)
                .Run(state)
                .Should()
                .Be(list.DefaultIfEmpty(int.MinValue).Max());

        [Property]
        public void Compose_MaxOfLogs_StateEqualList(int[] list)
        {
            var sc =
                from unused in list.Map(LogSc).Compose(int.MinValue, Math.Max)
                from log in Sc<ImmutableList<int>>.Get
                select log;

            var result = sc.Run(ImmutableList<int>.Empty);

            result.Should().Equal(list);
        }

        private static Sc<ImmutableList<int>, int> LogSc(int value) =>
            from log in Sc<ImmutableList<int>>.Get
            from unused in Sc<ImmutableList<int>>.Put(log.Add(value))
            select value;
    }
}