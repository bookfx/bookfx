namespace BookFx.Tests.Functional
{
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck.Xunit;
    using Xunit;

    public class ScTests
    {
        [Property]
        public void Select_IdentityFunctorLaw_Obey(object state, object value) =>
            Sc<object>.Return(value).Select(x => x).Run(state).Should().Be(value);

        [Property]
        public void Select_CompositionFunctorLaw_Obey(int state, int value)
        {
            var functor = Sc<object>.Return(value);

            static int A(int x) => x - 2;

            static int B(int x) => x * 50;

            static int C(int x) => B(A(x));

            functor.Select(A).Select(B).Run(state).Should().Be(functor.Select(C).Run(state));
        }

        [Property]
        public void Get_Always_ReturnsState(object state) =>
            (
                from result in Sc<object>.Get
                select result
            )
            .Run(state)
            .Should()
            .Be(state);

        [Property]
        public void Return_Always_ReturnsValue(object state, object value) =>
            (
                from result in Sc<object>.Return(value)
                select result
            )
            .Run(state)
            .Should()
            .Be(value);

        [Property]
        public void PutGet_Always_ReturnsValue(object state, object newState) =>
            (
                from unit in Sc<object>.Put(newState)
                from result in Sc<object>.Get
                select result
            )
            .Run(state)
            .Should()
            .Be(newState);

        [Fact]
        public void SelectMany_3Increments_2()
        {
            static Sc<int, int> GetAndIncrement() => x => (x, x + 1);

            var computation =
                from v0 in GetAndIncrement()
                from v1 in GetAndIncrement()
                from v2 in GetAndIncrement()
                select v2;

            computation.Run(0).Should().Be(2);
        }
    }
}