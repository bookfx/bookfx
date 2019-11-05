namespace BookFx.Tests.Functional
{
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck.Xunit;

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
    }
}