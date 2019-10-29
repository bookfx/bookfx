namespace BookFx.Benchmark.ClassProps
{
    using System.Collections.Immutable;
    using BookFx.Functional;

    public class Stack1Class
    {
        public static readonly Stack1Class Empty = new Stack1Class(ImmutableStack<Prop>.Empty);

        private readonly ImmutableStack<Prop> _props;

        private Stack1Class(ImmutableStack<Prop> props) => _props = props;

        private enum PropKey
        {
            Prop1,
            Prop2,
            Prop3,
            Prop4,
            Prop5,
            Prop6,
            Prop7,
            Prop8,
            Prop9,
            Prop10,
        }

        public Option<int> Prop1 => _props.Find(x => x.Key == PropKey.Prop1).Map(x => (int)x.Value);

        public Option<int> Prop2 => _props.Find(x => x.Key == PropKey.Prop2).Map(x => (int)x.Value);

        public Option<int> Prop3 => _props.Find(x => x.Key == PropKey.Prop3).Map(x => (int)x.Value);

        public Option<int> Prop4 => _props.Find(x => x.Key == PropKey.Prop4).Map(x => (int)x.Value);

        public Option<int> Prop5 => _props.Find(x => x.Key == PropKey.Prop5).Map(x => (int)x.Value);

        public Option<object> Prop6 => _props.Find(x => x.Key == PropKey.Prop6).Map(x => x.Value);

        public Option<object> Prop7 => _props.Find(x => x.Key == PropKey.Prop7).Map(x => x.Value);

        public Option<object> Prop8 => _props.Find(x => x.Key == PropKey.Prop8).Map(x => x.Value);

        public Option<object> Prop9 => _props.Find(x => x.Key == PropKey.Prop9).Map(x => x.Value);

        public Option<object> Prop10 => _props.Find(x => x.Key == PropKey.Prop10).Map(x => x.Value);

        public Stack1Class WithProp1(int value) => CreateWith(PropKey.Prop1, value);

        public Stack1Class WithProp2(int value) => CreateWith(PropKey.Prop2, value);

        public Stack1Class WithProp3(int value) => CreateWith(PropKey.Prop3, value);

        public Stack1Class WithProp4(int value) => CreateWith(PropKey.Prop4, value);

        public Stack1Class WithProp5(int value) => CreateWith(PropKey.Prop5, value);

        public Stack1Class WithProp6(object value) => CreateWith(PropKey.Prop6, value);

        public Stack1Class WithProp7(object value) => CreateWith(PropKey.Prop7, value);

        public Stack1Class WithProp8(object value) => CreateWith(PropKey.Prop8, value);

        public Stack1Class WithProp9(object value) => CreateWith(PropKey.Prop9, value);

        public Stack1Class WithProp10(object value) => CreateWith(PropKey.Prop10, value);

        private Stack1Class CreateWith(PropKey key, object value) => new Stack1Class(_props.Push(new Prop(key, value)));

        private struct Prop
        {
            public Prop(PropKey key, object value)
            {
                Key = key;
                Value = value;
            }

            public PropKey Key { get; }

            public object Value { get; }
        }
    }
}