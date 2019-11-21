namespace BookFx.Benchmark.ClassProps
{
    using System.Collections.Immutable;
    using BookFx.Functional;

    public class Stack1Class
    {
        public static readonly Stack1Class Empty = new Stack1Class(ImmutableStack<StackProp>.Empty);

        private readonly ImmutableStack<StackProp> _props;

        private Stack1Class(ImmutableStack<StackProp> props) => _props = props;

        public Option<int> Prop1 => _props.Find(x => x.Key == StackPropKey.Prop1).Map(x => (int)x.Value);

        public Option<int> Prop2 => _props.Find(x => x.Key == StackPropKey.Prop2).Map(x => (int)x.Value);

        public Option<int> Prop3 => _props.Find(x => x.Key == StackPropKey.Prop3).Map(x => (int)x.Value);

        public Option<int> Prop4 => _props.Find(x => x.Key == StackPropKey.Prop4).Map(x => (int)x.Value);

        public Option<int> Prop5 => _props.Find(x => x.Key == StackPropKey.Prop5).Map(x => (int)x.Value);

        public Option<object> Prop6 => _props.Find(x => x.Key == StackPropKey.Prop6).Map(x => x.Value);

        public Option<object> Prop7 => _props.Find(x => x.Key == StackPropKey.Prop7).Map(x => x.Value);

        public Option<object> Prop8 => _props.Find(x => x.Key == StackPropKey.Prop8).Map(x => x.Value);

        public Option<object> Prop9 => _props.Find(x => x.Key == StackPropKey.Prop9).Map(x => x.Value);

        public Option<object> Prop10 => _props.Find(x => x.Key == StackPropKey.Prop10).Map(x => x.Value);

        public Stack1Class WithProp1(int value) => CreateWith(StackPropKey.Prop1, value);

        public Stack1Class WithProp2(int value) => CreateWith(StackPropKey.Prop2, value);

        public Stack1Class WithProp3(int value) => CreateWith(StackPropKey.Prop3, value);

        public Stack1Class WithProp4(int value) => CreateWith(StackPropKey.Prop4, value);

        public Stack1Class WithProp5(int value) => CreateWith(StackPropKey.Prop5, value);

        public Stack1Class WithProp6(object value) => CreateWith(StackPropKey.Prop6, value);

        public Stack1Class WithProp7(object value) => CreateWith(StackPropKey.Prop7, value);

        public Stack1Class WithProp8(object value) => CreateWith(StackPropKey.Prop8, value);

        public Stack1Class WithProp9(object value) => CreateWith(StackPropKey.Prop9, value);

        public Stack1Class WithProp10(object value) => CreateWith(StackPropKey.Prop10, value);

        private Stack1Class CreateWith(StackPropKey key, object value) => new Stack1Class(_props.Push(new StackProp(key, value)));
    }
}