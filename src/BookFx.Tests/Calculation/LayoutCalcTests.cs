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

    public class LayoutCalcTests
    {
        [Fact]
        public void LayOut_ValueEmpty_R1C1() =>
            ValueBox.Empty
                .Get
                .LayOut()
                .Placement
                .Should()
                .Be(Placement.At(1, 1, 1, 1));

        [Property]
        public void LayOut_ValueWithSpans_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            Make.Value("A")
                .SpanRows(rowSpan.Get)
                .SpanCols(colSpan.Get)
                .Get
                .LayOut()
                .Placement
                .Should()
                .Be(Placement.At(1, 1, height: rowSpan.Get, width: colSpan.Get));

        [Fact]
        public void LayOut_RowEmpty_Empty() =>
            RowBox.Empty
                .Get
                .LayOut()
                .Placement
                .IsAbsent
                .Should()
                .BeTrue();

        [Property]
        public void LayOut_RowWith2Children_Expected(
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

            var placedParent = box.LayOut();
            var placedA = placedParent.Children[0];
            var placedB = placedParent.Children[1];

            placedParent
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: Math.Max(aRowSpan.Get, bRowSpan.Get),
                    width: aColSpan.Get + bColSpan.Get));

            placedA
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: aRowSpan.Get,
                    width: aColSpan.Get));

            placedB
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: aColSpan.Get + 1,
                    height: bRowSpan.Get,
                    width: bColSpan.Get));
        }

        [Property]
        public void LayOut_RowWithChildWithAutoSpan_ChildIsStretched(PositiveInt bRowSpan)
        {
            // AB
            var box = Make
                .Row(
                    Make.Value("A"),
                    Make.Value("B").SpanRows(bRowSpan.Get))
                .Get;

            var placedParent = box.LayOut();
            var placedA = placedParent.Children[0];

            placedA
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: bRowSpan.Get,
                    width: 1));
        }

        [Fact]
        public void LayOut_ColEmpty_Empty() =>
            ColBox.Empty
                .Get
                .LayOut()
                .Placement
                .IsAbsent
                .Should()
                .BeTrue();

        [Property]
        public void LayOut_ColWith2Children_Expected(
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

            var placedParent = box.LayOut();
            var placedA = placedParent.Children[0];
            var placedB = placedParent.Children[1];

            placedParent
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: aRowSpan.Get + bRowSpan.Get,
                    width: Math.Max(aColSpan.Get, bColSpan.Get)));

            placedA
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: aRowSpan.Get,
                    width: aColSpan.Get));

            placedB
                .Placement
                .Should()
                .Be(Placement.At(
                    row: aRowSpan.Get + 1,
                    col: 1,
                    height: bRowSpan.Get,
                    width: bColSpan.Get));
        }

        [Property]
        public void LayOut_ColWithChildWithAutoSpan_ChildIsStretched(PositiveInt bColSpan)
        {
            // A
            // B
            var box = Make
                .Col(
                    Make.Value("A"),
                    Make.Value("B").SpanCols(bColSpan.Get))
                .Get;

            var placedParent = box.LayOut();
            var placedA = placedParent.Children[0];

            placedA
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: 1,
                    width: bColSpan.Get));
        }

        [Fact]
        public void LayOut_StackEmpty_Empty() =>
            StackBox.Empty
                .Get
                .LayOut()
                .Placement
                .IsAbsent
                .Should()
                .BeTrue();

        [Property]
        public void LayOut_StackWith2Children_Expected(
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

            var placedParent = box.LayOut();
            var placedA = placedParent.Children[0];
            var placedB = placedParent.Children[1];

            placedParent
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: Math.Max(aRowSpan.Get, bRowSpan.Get),
                    width: Math.Max(aColSpan.Get, bColSpan.Get)));

            placedA
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: aRowSpan.Get,
                    width: aColSpan.Get));

            placedB
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: bRowSpan.Get,
                    width: bColSpan.Get));
        }

        [Property]
        public void LayOut_StackWithChildWithAutoSpan_ChildIsStretched(
            PositiveInt bRowSpan,
            PositiveInt bColSpan)
        {
            // B over A
            var box = Make
                .Stack(
                    Make.Value("A"),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get))
                .Get;

            var placedParent = box.LayOut();
            var placedA = placedParent.Children[0];

            placedA
                .Placement
                .Should()
                .Be(Placement.At(
                    row: 1,
                    col: 1,
                    height: bRowSpan.Get,
                    width: bColSpan.Get));
        }

        [Fact]
        public void LayOut_ProtoWithSlot_SlotBoxPlaced()
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
                .PlugProtos(Make.Proto(protoBook, protoRef).Add(slotRef, "Slot value").Get);

            var result = box.ValueUnsafe().LayOut();

            result
                .Slots
                .Single()
                .Box
                .Placement
                .Should()
                .Be(Placement.At(row: 2, col: 1, height: 1, width: 1));
        }

        [Fact]
        public void LayOut_NonuniqueChildren_Placed()
        {
            var child = Make.Value();
            var box = Make.Row(child, child).Get;

            var result = box.LayOut();

            result.Placement.Should().Be(Placement.At(row: 1, col: 1, height: 1, width: 2));
        }

        [Fact]
        public void LayOut_NonUniqueChildren_Placed()
        {
            var child1 = Make.Value();
            var child2 = Make.Value();
            var box = Make.Row(child1, child2, child1, child2).Get;

            var result = box.LayOut();

            result.Placement.Should().Be(Placement.At(row: 1, col: 1, height: 1, width: 2));
        }

        [Fact]
        public void LayOut_SameBoxInDifferentParents_Placed()
        {
            var child = Make.Value();
            var parent1 = Make.Row(child);
            var parent2 = Make.Row(child);
            var box = Make.Row(parent1, parent2).Get;

            var result = box.LayOut();

            result.Placement.Should().Be(Placement.At(row: 1, col: 1, height: 1, width: 2));
        }
    }
}