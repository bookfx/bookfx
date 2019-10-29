namespace BookFx.Benchmark
{
    using System.Collections.Immutable;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using BookFx.Functional;

    [MemoryDiagnoser]
    public class StackVsListBm
    {
        private const int N = 10000;
        private const string Value = "value";
        private readonly Consumer _consumer = new Consumer();

        [Benchmark]
        public void EmptyList() =>
            ImmutableList
                .CreateRange(Enumerable.Empty<string>())
                .Consume(_consumer);

        [Benchmark]
        public void EmptyStack() =>
            ImmutableStack
                .CreateRange(Enumerable.Empty<string>())
                .Reverse()
                .Consume(_consumer);

        [Benchmark]
        public void NList() =>
            ImmutableList
                .CreateRange(Enumerable.Range(1, N).Map(_ => Value))
                .Consume(_consumer);

        [Benchmark]
        public void NStack() =>
            ImmutableStack
                .CreateRange(Enumerable.Range(1, N).Map(_ => Value))
                .Reverse()
                .Consume(_consumer);
    }
}