namespace BookFx.Tests.Functional
{
    using System;
    using BookFx.Functional;
    using BookFx.Tests.Arbitraries;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using static BookFx.Functional.F;

    [Properties(Arbitrary = new[] { typeof(OptionArb) })]
    public class OptionExtTests
    {
        [Property]
        public void Map_IdentityFunctorLaw_Obey(Option<object> functor) =>
            functor.Map(x => x).Should().Be(functor);

        [Property]
        public void Map_CompositionFunctorLaw_Obey(Option<int> functor)
        {
            int A(int x) =>
                x - 2;

            int B(int x) =>
                x * 50;

            int C(int x) =>
                B(A(x));

            functor.Map(A).Map(B).Should().Be(functor.Map(C));
        }

        [Property]
        public void Bind_RightIdentityMonadLaw_Obey(Option<object> m) =>
            m.Bind(Some).Should().Be(m);

        [Property]
        public void Bind_LeftIdentity_Obey(int x)
        {
            Option<int> Fn(int i) =>
                i % 2 == 0 ? Some(i) : None;

            Some(x).Bind(Fn).Should().Be(Fn(x));
        }

        [Property]
        public void Bind_LeftIdentityMonadLawWithRefValue_Obey(NonNull<string> x)
        {
            Option<string> Fn(string s) =>
                Some($"{s} World");

            Some(x.Get).Bind(Fn).Should().Be(Fn(x.Get));
        }

        [Property]
        public void Bind_AssociativityMonadLaw_Obey(Option<string> m)
        {
            Option<double> Parse(string s) =>
                double.TryParse(s, out var result)
                    ? Some(result)
                    : None;

            Option<double> Sqrt(double d) =>
                d < 0d ? None : Some(Math.Sqrt(d));

            m.Bind(Parse).Bind(Sqrt).Should().Be(m.Bind(x => Parse(x).Bind(Sqrt)));
        }
    }
}