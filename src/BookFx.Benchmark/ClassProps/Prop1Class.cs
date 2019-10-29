namespace BookFx.Benchmark.ClassProps
{
    using BookFx.Functional;
    using static BookFx.Functional.F;

    public class Prop1Class
    {
        public static readonly Prop1Class Empty =
            new Prop1Class(None, None, None, None, None, None, None, None, None, None);

        private Prop1Class(
            Option<int> prop1,
            Option<int> prop2,
            Option<int> prop3,
            Option<int> prop4,
            Option<int> prop5,
            Option<object> prop6,
            Option<object> prop7,
            Option<object> prop8,
            Option<object> prop9,
            Option<object> prop10)
        {
            Prop1 = prop1;
            Prop2 = prop2;
            Prop3 = prop3;
            Prop4 = prop4;
            Prop5 = prop5;
            Prop6 = prop6;
            Prop7 = prop7;
            Prop8 = prop8;
            Prop9 = prop9;
            Prop10 = prop10;
        }

        public Option<int> Prop1 { get; }

        public Option<int> Prop2 { get; }

        public Option<int> Prop3 { get; }

        public Option<int> Prop4 { get; }

        public Option<int> Prop5 { get; }

        public Option<object> Prop6 { get; }

        public Option<object> Prop7 { get; }

        public Option<object> Prop8 { get; }

        public Option<object> Prop9 { get; }

        public Option<object> Prop10 { get; }

        public Prop1Class WithProp1(int value) => CreateWith(prop1: value);

        public Prop1Class WithProp2(int value) => CreateWith(prop2: value);

        public Prop1Class WithProp3(int value) => CreateWith(prop3: value);

        public Prop1Class WithProp4(int value) => CreateWith(prop4: value);

        public Prop1Class WithProp5(int value) => CreateWith(prop5: value);

        public Prop1Class WithProp6(object value) => CreateWith(prop6: value);

        public Prop1Class WithProp7(object value) => CreateWith(prop7: value);

        public Prop1Class WithProp8(object value) => CreateWith(prop8: value);

        public Prop1Class WithProp9(object value) => CreateWith(prop9: value);

        public Prop1Class WithProp10(object value) => CreateWith(prop10: value);

        private Prop1Class CreateWith(
            Option<int>? prop1 = null,
            Option<int>? prop2 = null,
            Option<int>? prop3 = null,
            Option<int>? prop4 = null,
            Option<int>? prop5 = null,
            Option<object>? prop6 = null,
            Option<object>? prop7 = null,
            Option<object>? prop8 = null,
            Option<object>? prop9 = null,
            Option<object>? prop10 = null) =>
            new Prop1Class(
                prop1 ?? Prop1,
                prop2 ?? Prop2,
                prop3 ?? Prop3,
                prop4 ?? Prop4,
                prop5 ?? Prop5,
                prop6 ?? Prop6,
                prop7 ?? Prop7,
                prop8 ?? Prop8,
                prop9 ?? Prop9,
                prop10 ?? Prop10);
    }
}