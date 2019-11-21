namespace BookFx.Benchmark.ClassProps
{
    using System.Collections.Immutable;
    using System.Runtime.CompilerServices;
    using BookFx.Functional;
    using static BookFx.Functional.F;

    public class Stack2Class
    {
        public static readonly Stack2Class Empty = new Stack2Class(ImmutableStack<StackProp>.Empty);

        private readonly ImmutableStack<StackProp> _props;

        private Stack2Class(ImmutableStack<StackProp> props) => _props = props;

        public Option<int> Prop1 => Get<int>(StackPropKey.Prop1);

        public Option<int> Prop2 => Get<int>(StackPropKey.Prop2);

        public Option<int> Prop3 => Get<int>(StackPropKey.Prop3);

        public Option<int> Prop4 => Get<int>(StackPropKey.Prop4);

        public Option<int> Prop5 => Get<int>(StackPropKey.Prop5);

        public Option<object> Prop6 => Get<object>(StackPropKey.Prop6);

        public Option<object> Prop7 => Get<object>(StackPropKey.Prop7);

        public Option<object> Prop8 => Get<object>(StackPropKey.Prop8);

        public Option<object> Prop9 => Get<object>(StackPropKey.Prop9);

        public Option<object> Prop10 => Get<object>(StackPropKey.Prop10);

        public Stack2Class WithProp1(int value) => CreateWith(StackPropKey.Prop1, value);

        public Stack2Class WithProp2(int value) => CreateWith(StackPropKey.Prop2, value);

        public Stack2Class WithProp3(int value) => CreateWith(StackPropKey.Prop3, value);

        public Stack2Class WithProp4(int value) => CreateWith(StackPropKey.Prop4, value);

        public Stack2Class WithProp5(int value) => CreateWith(StackPropKey.Prop5, value);

        public Stack2Class WithProp6(object value) => CreateWith(StackPropKey.Prop6, value);

        public Stack2Class WithProp7(object value) => CreateWith(StackPropKey.Prop7, value);

        public Stack2Class WithProp8(object value) => CreateWith(StackPropKey.Prop8, value);

        public Stack2Class WithProp9(object value) => CreateWith(StackPropKey.Prop9, value);

        public Stack2Class WithProp10(object value) => CreateWith(StackPropKey.Prop10, value);

        private Option<T> Get<T>(StackPropKey key)
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
        private Stack2Class CreateWith(StackPropKey key, object value) =>
            new Stack2Class(_props.Push(new StackProp(key, value)));
    }
}