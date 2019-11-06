namespace BookFx.Tests.Functional
{
    using System.Collections.Immutable;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck.Xunit;

    public class EnumerableScExtTests
    {
        [Property]
        public void Traverse_ScOf_ResultEqualList(int[] list, object state) =>
            list.Traverse(Sc<object>.ScOf).Run(state).Should().Equal(list);

        [Property]
        public void Traverse_Log_StateEqualList(int[] list)
        {
            var sc =
                from unused in list.Traverse(LogSc)
                from log in Sc<ImmutableList<int>>.GetState
                select log;

            var result = sc.Run(ImmutableList<int>.Empty);

            result.Should().Equal(list);
        }

        private static Sc<ImmutableList<int>, int> LogSc(int value) =>
            from log in Sc<ImmutableList<int>>.GetState
            from unused in Sc<ImmutableList<int>>.PutState(log.Add(value))
            select value;
    }
}