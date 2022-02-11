namespace BookFx.Tests.Calculation
{
    using System;
    using System.Linq;
    using BookFx.Calculation;
    using BookFx.Cores;
    using BookFx.Functional;
    using BookFx.Tests.Arbitraries;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using Xunit;
    using static BookFx.Functional.F;

    public class MinDimensionCalcTests
    {
        [Fact]
        public void MinDimension_ValueEmpty_1x1() => GetMinDimension(ValueBox.Empty.Get).Should().Be((1, 1));

        [Fact]
        public void MinDimension_Value_1x1() => GetMinDimension(Make.Value("A").Get).Should().Be((1, 1));

        [Property]
        public void MinDimension_ValueWithSpans_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            GetMinDimension(Make.Value("A").SpanRows(rowSpan.Get).SpanCols(colSpan.Get).Get)
                .Should()
                .Be((rowSpan.Get, colSpan.Get));

        [Fact]
        public void MinDimension_RowEmpty_Empty() => GetMinDimension(Make.Row().Get).Should().Be((0, 0));

        [Property]
        public void MinDimension_RowWithChild_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            GetMinDimension(Make.Row(Make.Value("A").SpanRows(rowSpan.Get).SpanCols(colSpan.Get)).Get)
                .Should()
                .Be((rowSpan.Get, colSpan.Get));

        [Property]
        public void MinDimension_RowWith2ChildrenWithSpans_Expected(
            PositiveInt aRowSpan,
            PositiveInt aColSpan,
            PositiveInt bRowSpan,
            PositiveInt bColSpan)
        {
            var box = Make
                .Row(
                    Make.Value("A").SpanRows(aRowSpan.Get).SpanCols(aColSpan.Get),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get)
                )
                .Get;

            var result = GetMinDimension(box);

            result.Should().Be((Math.Max(aRowSpan.Get, bRowSpan.Get), aColSpan.Get + bColSpan.Get));
        }

        [Fact]
        public void MinDimension_RowWith2ChildrenWithoutSpans_1x2() =>
            GetMinDimension(Make.Row(Make.Value("A"), Make.Value("B")).Get).Should().Be((1, 2));

        [Fact]
        public void MinDimension_RowWithRowWithValue_All1x1()
        {
            var box = Make.Row(Make.Row(Make.Value("A"))).Get;

            var genX = box;
            var genY = genX.Children.First();
            var genZ = genY.Children.First();
            GetMinDimension(genX).Should().Be((1, 1));
            GetMinDimension(genY).Should().Be((1, 1));
            GetMinDimension(genZ).Should().Be((1, 1));
        }

        [Fact]
        public void MinDimension_ColEmpty_Empty() => GetMinDimension(ColBox.Empty.Get).Should().Be((0, 0));

        [Property]
        public void MinDimension_ColWithChild_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            GetMinDimension(Make.Col(Make.Value("A").SpanRows(rowSpan.Get).SpanCols(colSpan.Get)).Get)
                .Should()
                .Be((rowSpan.Get, colSpan.Get));

        [Property]
        public void MinDimension_ColWith2ChildrenWithSpans_Expected(
            PositiveInt aRowSpan,
            PositiveInt aColSpan,
            PositiveInt bRowSpan,
            PositiveInt bColSpan)
        {
            var box = Make
                .Col(
                    Make.Value("A").SpanRows(aRowSpan.Get).SpanCols(aColSpan.Get),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get)
                )
                .Get;

            var result = GetMinDimension(box);

            result.Should().Be((aRowSpan.Get + bRowSpan.Get, Math.Max(aColSpan.Get, bColSpan.Get)));
        }

        [Fact]
        public void MinDimension_ColWith2ChildrenWithoutSpans_2x1() =>
            GetMinDimension(Make
                    .Col(Make.Value("A"), Make.Value("B"))
                    .Get)
                .Should()
                .Be((2, 1));

        [Fact]
        public void MinDimension_StackEmpty_Empty() => GetMinDimension(StackBox.Empty.Get).Should().Be((0, 0));

        [Property]
        public void MinDimension_StackWithChild_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            GetMinDimension(Make
                    .Stack(Make.Value("A")
                        .SpanRows(rowSpan.Get)
                        .SpanCols(colSpan.Get)
                    )
                    .Get)
                .Should()
                .Be((rowSpan.Get, colSpan.Get));

        [Property]
        public void MinDimension_StackWith2ChildrenWithSpans_Expected(
            PositiveInt aRowSpan,
            PositiveInt aColSpan,
            PositiveInt bRowSpan,
            PositiveInt bColSpan) =>
            GetMinDimension(Make
                    .Stack(
                        Make.Value("A").SpanRows(aRowSpan.Get).SpanCols(aColSpan.Get),
                        Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get)
                    )
                    .Get)
                .Should()
                .Be((
                    Math.Max(aRowSpan.Get, bRowSpan.Get),
                    Math.Max(aColSpan.Get, bColSpan.Get)));

        [Fact]
        public void MinDimension_StackWith2ChildrenWithoutSpans_1x1() =>
            GetMinDimension(Make.Stack(Make.Value("A"), Make.Value("B")).Get).Should().Be((1, 1));

        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) }, MaxTest = 5)]
        public void MinDimension_ProtoWithoutSlots_AsProto(int protoHeight, int protoWidth)
        {
            const string protoRef = "ProtoRef";
            var protoBook = Make
                .Value()
                .SpanRows(protoHeight)
                .SpanCols(protoWidth)
                .NameGlobally(protoRef)
                .ToSheet()
                .ToBook()
                .ToBytes();
            var box = Make.Proto(protoBook, protoRef).Get;
            var bank = new ProtoBank(List(protoBook));

            var result = GetMinDimension(bank.PlugProtos(box).ValueUnsafe());

            result.Should().Be((protoHeight, protoWidth));
        }

        [Fact]
        public void MinDimension_ProtoWithSlotBox1x1_SlotBox1x1()
        {
            const string protoRef = "ProtoRef";
            var protoBook = Make
                .Value()
                .NameGlobally(protoRef)
                .ToSheet()
                .ToBook()
                .ToBytes();
            var box = Make.Proto(protoBook, protoRef).Add(protoRef, "SlotValue").Get;
            var bank = new ProtoBank(List(protoBook));

            var result = GetMinDimension(bank.PlugProtos(box).ValueUnsafe().Slots.Single().Box);

            result.Should().Be((1, 1));
        }

        private static (int Height, int Width) GetMinDimension(BoxCore box)
        {
            var (numberedBox, boxCount) = box.Number();
            var layout = Layout.Create(numberedBox, boxCount);

            return (numberedBox.MinHeight(layout), numberedBox.MinWidth(layout));
        }
    }
}