namespace BookFx.Tests.Functional
{
    using System.Collections.Immutable;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck.Xunit;

    public class EnumerableScExtTests
    {
        [Property]
        public void Traverse_Return_ResultEqualList(int[] list, object state) =>
            list.Traverse(Sc<object>.Return).Run(state).Should().Equal(list);

        [Property]
        public void Traverse_Log_StateEqualList(int[] list)
        {
            var sc =
                from unused in list.Traverse(LogSc)
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