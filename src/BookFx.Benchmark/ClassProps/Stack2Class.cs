namespace BookFx.Benchmark.ClassProps
{
    using System.Collections.Immutable;
    using System.Runtime.CompilerServices;
    using BookFx.Functional;
    using static BookFx.Functional.F;

    public class Stack2Class
    {
        public static readonly Stack2Class Empty = new Stack2Class(ImmutableStack<Prop>.Empty);

        private readonly ImmutableStack<Prop> _props;

        private Stack2Class(ImmutableStack<Prop> props) => _props = props;

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

        public Option<int> Prop1 => Get<int>(PropKey.Prop1);

        public Option<int> Prop2 => Get<int>(PropKey.Prop2);

        public Option<int> Prop3 => Get<int>(PropKey.Prop3);

        public Option<int> Prop4 => Get<int>(PropKey.Prop4);

        public Option<int> Prop5 => Get<int>(PropKey.Prop5);

        public Option<object> Prop6 => Get<object>(PropKey.Prop6);

        public Option<object> Prop7 => Get<object>(PropKey.Prop7);

        public Option<object> Prop8 => Get<object>(PropKey.Prop8);

        public Option<object> Prop9 => Get<object>(PropKey.Prop9);

        public Option<object> Prop10 => Get<object>(PropKey.Prop10);

        public Stack2Class WithProp1(int value) => CreateWith(PropKey.Prop1, value);

        public Stack2Class WithProp2(int value) => CreateWith(PropKey.Prop2, value);

        public Stack2Class WithProp3(int value) => CreateWith(PropKey.Prop3, value);

        public Stack2Class WithProp4(int value) => CreateWith(PropKey.Prop4, value);

        public Stack2Class WithProp5(int value) => CreateWith(PropKey.Prop5, value);

        public Stack2Class WithProp6(object value) => CreateWith(PropKey.Prop6, value);

        public Stack2Class WithProp7(object value) => CreateWith(PropKey.Prop7, value);

        public Stack2Class WithProp8(object value) => CreateWith(PropKey.Prop8, value);

        public Stack2Class WithProp9(object value) => CreateWith(PropKey.Prop9, value);

        public Stack2Class WithProp10(object value) => CreateWith(PropKey.Prop10, value);

        private Option<T> Get<T>(PropKey key)
        {
            var props = _props;

            while (!props.IsEmpty)
            {
                props = props.Pop(out var prop);

                if (prop.Key == key)
                {
                    return (T)prop.Value;
                }
            }

            return None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Stack2Class CreateWith(PropKey key, object value) => new Stack2Class(_props.Push(new Prop(key, value)));

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