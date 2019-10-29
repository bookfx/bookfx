namespace BookFx.Benchmark
{
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Engines;
    using BookFx.Benchmark.ClassProps;
    using BookFx.Functional;

    [MemoryDiagnoser]
    public class ClassPropsBm
    {
        private const int N = 10000;
        private const string Value = "value";
        private readonly Consumer _consumer = new Consumer();

        [Benchmark]
        public void CreateEmptyProp1() =>
            Enumerable
                .Range(1, N)
                .Map(_ => Prop1Class.Empty)
                .Consume(_consumer);

        [Benchmark]
        public void CreateEmptyProp2() =>
            Enumerable
                .Range(1, N)
                .Map(_ => Prop2Class.Empty)
                .Consume(_consumer);

        [Benchmark]
        public void CreateEmptyStack1() =>
            Enumerable
                .Range(1, N)
                .Map(_ => Stack1Class.Empty)
                .Consume(_consumer);

        [Benchmark]
        public void CreateEmptyStack2() =>
            Enumerable
                .Range(1, N)
                .Map(_ => Stack2Class.Empty)
                .Consume(_consumer);

        [Benchmark]
        public void CreateOneValueProp1() =>
            Enumerable
                .Range(1, N)
                .Map(x => Prop1Class.Empty.WithProp1(x))
                .Consume(_consumer);

        [Benchmark]
        public void CreateOneValueProp2() =>
            Enumerable
                .Range(1, N)
                .Map(x => Prop2Class.Empty.WithProp1(x))
                .Consume(_consumer);

        [Benchmark]
        public void CreateOneValueStack1() =>
            Enumerable
                .Range(1, N)
                .Map(x => Stack1Class.Empty.WithProp1(x))
                .Consume(_consumer);

        [Benchmark]
        public void CreateOneValueStack2() =>
            Enumerable
                .Range(1, N)
                .Map(x => Stack2Class.Empty.WithProp1(x))
                .Consume(_consumer);

        [Benchmark]
        public void CreateThreeValuesProp1() =>
            Enumerable
                .Range(1, N)
                .Map(x => Prop1Class.Empty.WithProp1(x).WithProp2(x).WithProp6(Value))
                .Consume(_consumer);

        [Benchmark]
        public void CreateThreeValuesProp2() =>
            Enumerable
                .Range(1, N)
                .Map(x => Prop2Class.Empty.WithProp1(x).WithProp2(x).WithProp6(Value))
                .Consume(_consumer);

        [Benchmark]
        public void CreateThreeValuesStack1() =>
            Enumerable
                .Range(1, N)
                .Map(x => Stack1Class.Empty.WithProp1(x).WithProp2(x).WithProp6(Value))
                .Consume(_consumer);

        [Benchmark]
        public void CreateThreeValuesStack2() =>
            Enumerable
                .Range(1, N)
                .Map(x => Stack2Class.Empty.WithProp1(x).WithProp2(x).WithProp6(Value))
                .Consume(_consumer);

        [Benchmark]
        public void PollEmptyProp1() =>
            Enumerable.Range(1, N)
                .Map(_ => Prop1Class.Empty)
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollEmptyProp2() =>
            Enumerable.Range(1, N)
                .Map(_ => Prop2Class.Empty)
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollEmptyStack1() =>
            Enumerable.Range(1, N)
                .Map(_ => Stack1Class.Empty)
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollEmptyStack2() =>
            Enumerable.Range(1, N)
                .Map(_ => Stack2Class.Empty)
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollOneValueProp1() =>
            Enumerable.Range(1, N)
                .Map(x => Prop1Class.Empty.WithProp1(x))
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollOneValueProp2() =>
            Enumerable.Range(1, N)
                .Map(x => Prop2Class.Empty.WithProp1(x))
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollOneValueStack1() =>
            Enumerable.Range(1, N)
                .Map(x => Stack1Class.Empty.WithProp1(x))
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollOneValueStack2() =>
            Enumerable.Range(1, N)
                .Map(x => Stack2Class.Empty.WithProp1(x))
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollThreeValuesProp1() =>
            Enumerable.Range(1, N)
                .Map(x => Prop1Class.Empty.WithProp1(x).WithProp2(x).WithProp6(Value))
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollThreeValuesProp2() =>
            Enumerable.Range(1, N)
                .Map(x => Prop2Class.Empty.WithProp1(x).WithProp2(x).WithProp6(Value))
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollThreeValuesStack1() =>
            Enumerable.Range(1, N)
                .Map(x => Stack1Class.Empty.WithProp1(x).WithProp2(x).WithProp6(Value))
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);

        [Benchmark]
        public void PollThreeValuesStack2() =>
            Enumerable.Range(1, N)
                .Map(x => Stack2Class.Empty.WithProp1(x).WithProp2(x).WithProp6(Value))
                .Map(x => (x.Prop1, x.Prop2, x.Prop6))
                .Consume(_consumer);
    }
}