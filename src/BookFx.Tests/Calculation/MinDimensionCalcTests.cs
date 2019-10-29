namespace BookFx.Tests.Calculation
{
    using System;
    using System.Linq;
    using BookFx.Calculation;
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
        public void WithMinDimension_ValueEmpty_1x1() =>
            ValueBox.Empty.Get.WithMinDimension().MinDimension.Should().Be(Dimension.Of(1, 1));

        [Fact]
        public void WithMinDimension_Value_1x1() =>
            Make.Value("A").Get.WithMinDimension().MinDimension.Should().Be(Dimension.Of(1, 1));

        [Property]
        public void WithMinDimension_ValueWithSpans_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            Make.Value("A")
                .SpanRows(rowSpan.Get)
                .SpanCols(colSpan.Get)
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Of(height: rowSpan.Get, width: colSpan.Get));

        [Fact]
        public void WithMinDimension_RowEmpty_Empty() =>
            Make
                .Row()
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Empty);

        [Property]
        public void WithMinDimension_RowWithChild_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            Make
                .Row(Make.Value("A")
                    .SpanRows(rowSpan.Get)
                    .SpanCols(colSpan.Get)
                )
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Of(height: rowSpan.Get, width: colSpan.Get));

        [Property]
        public void WithMinDimension_RowWith2ChildrenWithSpans_Expected(
            PositiveInt aRowSpan,
            PositiveInt aColSpan,
            PositiveInt bRowSpan,
            PositiveInt bColSpan) =>
            Make
                .Row(
                    Make.Value("A").SpanRows(aRowSpan.Get).SpanCols(aColSpan.Get),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get)
                )
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Of(
                    height: Math.Max(aRowSpan.Get, bRowSpan.Get),
                    width: aColSpan.Get + bColSpan.Get));

        [Fact]
        public void WithMinDimension_RowWith2ChildrenWithoutSpans_1x2() =>
            Make
                .Row(Make.Value("A"), Make.Value("B"))
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Of(height: 1, width: 2));

        [Fact]
        public void WithMinDimension_RowWithRowWithValue_All1x1()
        {
            var box = Make.Row(Make.Row(Make.Value("A"))).Get;

            var result = box.WithMinDimension();

            var genX = result;
            var genY = genX.Children.First();
            var genZ = genY.Children.First();
            genX.MinDimension.Should().Be(Dimension.Of(1, 1));
            genY.MinDimension.Should().Be(Dimension.Of(1, 1));
            genZ.MinDimension.Should().Be(Dimension.Of(1, 1));
        }

        [Fact]
        public void WithMinDimension_ColEmpty_Empty() =>
            ColBox.Empty
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Empty);

        [Property]
        public void WithMinDimension_ColWithChild_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            Make
                .Col(Make.Value("A")
                    .SpanRows(rowSpan.Get)
                    .SpanCols(colSpan.Get)
                )
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Of(height: rowSpan.Get, width: colSpan.Get));

        [Property]
        public void WithMinDimension_ColWith2ChildrenWithSpans_Expected(
            PositiveInt aRowSpan,
            PositiveInt aColSpan,
            PositiveInt bRowSpan,
            PositiveInt bColSpan) =>
            Make
                .Col(
                    Make.Value("A").SpanRows(aRowSpan.Get).SpanCols(aColSpan.Get),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get)
                )
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Of(
                    height: aRowSpan.Get + bRowSpan.Get,
                    width: Math.Max(aColSpan.Get, bColSpan.Get)));

        [Fact]
        public void WithMinDimension_ColWith2ChildrenWithoutSpans_2x1() =>
            Make
                .Col(Make.Value("A"), Make.Value("B"))
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Of(height: 2, width: 1));

        [Fact]
        public void WithMinDimension_StackEmpty_Empty() =>
            StackBox.Empty
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Empty);

        [Property]
        public void WithMinDimension_StackWithChild_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            Make
                .Stack(Make.Value("A")
                    .SpanRows(rowSpan.Get)
                    .SpanCols(colSpan.Get)
                )
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Of(height: rowSpan.Get, width: colSpan.Get));

        [Property]
        public void WithMinDimension_StackWith2ChildrenWithSpans_Expected(
            PositiveInt aRowSpan,
            PositiveInt aColSpan,
            PositiveInt bRowSpan,
            PositiveInt bColSpan) =>
            Make
                .Stack(
                    Make.Value("A").SpanRows(aRowSpan.Get).SpanCols(aColSpan.Get),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get)
                )
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Of(
                    height: Math.Max(aRowSpan.Get, bRowSpan.Get),
                    width: Math.Max(aColSpan.Get, bColSpan.Get)));

        [Fact]
        public void WithMinDimension_StackWith2ChildrenWithoutSpans_1x1() =>
            Make
                .Stack(Make.Value("A"), Make.Value("B"))
                .Get
                .WithMinDimension()
                .MinDimension
                .Should()
                .Be(Dimension.Of(height: 1, width: 1));

        [Property(Arbitrary = new[] { typeof(ValidColumnNumberArb) }, MaxTest = 5)]
        public void WithMinDimension_ProtoWithoutSlots_AsProto(int protoHeight, int protoWidth)
        {
            const string protoRef = "ProtoRef";
            var protoBook = Make
                .Value()
                .SpanRows(protoHeight)
                .SpanCols(protoWidth)
                .Name(protoRef)
                .ToSheet()
                .ToBook()
                .ToBytes();
            var box = Make.Proto(protoBook, protoRef).Get;
            var bank = new ProtoBank(List(protoBook));

            var result = bank.PlugProtos(box).Map(x => x.WithMinDimension()).ValueUnsafe();

            result.MinDimension.Should().Be(Dimension.Of(height: protoHeight, width: protoWidth));
        }

        [Fact]
        public void WithMinDimension_ProtoWithSlotBox1x1_SlotBox1x1()
        {
            const string protoRef = "ProtoRef";
            var protoBook = Make
                .Value()
                .Name(protoRef)
                .ToSheet()
                .ToBook()
                .ToBytes();
            var box = Make.Proto(protoBook, protoRef).Add(protoRef, "SlotValue").Get;
            var bank = new ProtoBank(List(protoBook));

            var result = bank.PlugProtos(box).Map(x => x.WithMinDimension()).ValueUnsafe();

            result.Slots.Single().Box.MinDimension.Should().Be(Dimension.Of(1, 1));
        }
    }
}