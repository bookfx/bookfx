namespace BookFx.Tests.Calculation
{
    using System;
    using System.Linq;
    using BookFx.Calculation;
    using BookFx.Functional;
    using FluentAssertions;
    using FsCheck;
    using FsCheck.Xunit;
    using Xunit;
    using static BookFx.Functional.F;

    public class PlacementCalcTests
    {
        [Fact]
        public void WithPlacement_ValueEmpty_R1C1() =>
            ValueBox.Empty
                .Get
                .WithMinDimension()
                .WithPlacement()
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(1, 1, 1, 1));

        [Property]
        public void WithPlacement_ValueWithSpans_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            Make.Value("A")
                .SpanRows(rowSpan.Get)
                .SpanCols(colSpan.Get)
                .Get
                .WithMinDimension()
                .WithPlacement()
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(1, 1, height: rowSpan.Get, width: colSpan.Get));

        [Fact]
        public void WithPlacement_RowEmpty_Empty() =>
            RowBox.Empty
                .Get
                .WithMinDimension()
                .WithPlacement()
                .ValueUnsafe()
                .Placement
                .IsAbsent
                .Should()
                .BeTrue();

        [Property]
        public void WithPlacement_RowWith2Children_Expected(
            PositiveInt aRowSpan,
            PositiveInt aColSpan,
            PositiveInt bRowSpan,
            PositiveInt bColSpan)
        {
            // AB
            var box = Make
                .Row(
                    Make.Value("A").SpanRows(aRowSpan.Get).SpanCols(aColSpan.Get),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get)
                )
                .Get;

            var placedParent = box.WithMinDimension().WithPlacement();
            var placedA = placedParent.Map(x => x.Children[0]);
            var placedB = placedParent.Map(x => x.Children[1]);

            placedParent
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: Math.Max(aRowSpan.Get, bRowSpan.Get),
                    width: aColSpan.Get + bColSpan.Get));

            placedA
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: aRowSpan.Get,
                    width: aColSpan.Get));

            placedB
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: aColSpan.Get + 1,
                    height: bRowSpan.Get,
                    width: bColSpan.Get));
        }

        [Property]
        public void WithPlacement_RowWithChildWithAutoSpan_ChildIsStretched(PositiveInt bRowSpan)
        {
            // AB
            var box = Make
                .Row(
                    Make.Value("A"),
                    Make.Value("B").SpanRows(bRowSpan.Get))
                .Get;

            var placedParent = box.WithMinDimension().WithPlacement();
            var placedA = placedParent.Map(x => x.Children[0]);

            placedA
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: bRowSpan.Get,
                    width: 1));
        }

        [Fact]
        public void WithPlacement_ColEmpty_Empty() =>
            ColBox.Empty
                .Get
                .WithMinDimension()
                .WithPlacement()
                .ValueUnsafe()
                .Placement
                .IsAbsent
                .Should()
                .BeTrue();

        [Property]
        public void WithPlacement_ColWith2Children_Expected(
            PositiveInt aRowSpan,
            PositiveInt aColSpan,
            PositiveInt bRowSpan,
            PositiveInt bColSpan)
        {
            // A
            // B
            var box = Make
                .Col(
                    Make.Value("A").SpanRows(aRowSpan.Get).SpanCols(aColSpan.Get),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get)
                )
                .Get;

            var placedParent = box.WithMinDimension().WithPlacement();
            var placedA = placedParent.Map(x => x.Children[0]);
            var placedB = placedParent.Map(x => x.Children[1]);

            placedParent
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: aRowSpan.Get + bRowSpan.Get,
                    width: Math.Max(aColSpan.Get, bColSpan.Get)));

            placedA
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: aRowSpan.Get,
                    width: aColSpan.Get));

            placedB
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: aRowSpan.Get + 1,
                    col: 1,
                    height: bRowSpan.Get,
                    width: bColSpan.Get));
        }

        [Property]
        public void WithPlacement_ColWithChildWithAutoSpan_ChildIsStretched(PositiveInt bColSpan)
        {
            // A
            // B
            var box = Make
                .Col(
                    Make.Value("A"),
                    Make.Value("B").SpanCols(bColSpan.Get))
                .Get;

            var placedParent = box.WithMinDimension().WithPlacement();
            var placedA = placedParent.Map(x => x.Children[0]);

            placedA
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: 1,
                    width: bColSpan.Get));
        }

        [Fact]
        public void WithPlacement_StackEmpty_Empty() =>
            StackBox.Empty
                .Get
                .WithMinDimension()
                .WithPlacement()
                .ValueUnsafe()
                .Placement
                .IsAbsent
                .Should()
                .BeTrue();

        [Property]
        public void WithPlacement_StackWith2Children_Expected(
            PositiveInt aRowSpan,
            PositiveInt aColSpan,
            PositiveInt bRowSpan,
            PositiveInt bColSpan)
        {
            // B over A
            var box = Make
                .Stack(
                    Make.Value("A").SpanRows(aRowSpan.Get).SpanCols(aColSpan.Get),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get)
                )
                .Get;

            var placedParent = box.WithMinDimension().WithPlacement();
            var placedA = placedParent.Map(x => x.Children[0]);
            var placedB = placedParent.Map(x => x.Children[1]);

            placedParent
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: Math.Max(aRowSpan.Get, bRowSpan.Get),
                    width: Math.Max(aColSpan.Get, bColSpan.Get)));

            placedA
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: aRowSpan.Get,
                    width: aColSpan.Get));

            placedB
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: bRowSpan.Get,
                    width: bColSpan.Get));
        }

        [Property]
        public void WithPlacement_StackWithChildWithAutoSpan_ChildIsStretched(
            PositiveInt bRowSpan,
            PositiveInt bColSpan)
        {
            // B over A
            var box = Make
                .Stack(
                    Make.Value("A"),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get))
                .Get;

            var placedParent = box.WithMinDimension().WithPlacement();
            var placedA = placedParent.Map(x => x.Children[0]);

            placedA
                .Map(x => x.Placement)
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: bRowSpan.Get,
                    width: bColSpan.Get));
        }

        [Fact]
        public void WithPlacement_ProtoWithSlot_SlotBoxPlaced()
        {
            const string protoRef = "ProtoRef";
            const string slotRef = "SlotRef";
            var protoBook = Make
                .Col(
                    "A",
                    Make.Value("Slot default value").Name(slotRef))
                .Name(protoRef)
                .ToSheet()
                .ToBook()
                .ToBytes();
            var bank = new ProtoBank(List(protoBook));
            var box = bank
                .PlugProtos(Make.Proto(protoBook, protoRef).Add(slotRef, "Slot value").Get)
                .Map(x => x.WithMinDimension());

            var result = box.ValueUnsafe().WithPlacement();

            result
                .Map(x => x.Slots.Single().Box.Placement)
                .Should()
                .Be(Placement.At(row: 2, col: 1, height: 1, width: 1));
        }
    }
}