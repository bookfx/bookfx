namespace BookFx.Tests.Calculation
{
    using System;
    using System.Collections.Generic;
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

    public class PlacementCalcTests
    {
        [Fact]
        public void Place_ValueEmpty_R1C1() =>
            ValueBox.Empty
                .Get
                .Place()
                .Placement
                .Should()
                .Be(Placement.At(1, 1, 1, 1));

        [Property]
        public void Place_ValueWithSpans_AsSpans(PositiveInt rowSpan, PositiveInt colSpan) =>
            Make.Value("A")
                .SpanRows(rowSpan.Get)
                .SpanCols(colSpan.Get)
                .Get
                .Place()
                .Placement
                .Should()
                .Be(Placement.At(1, 1, height: rowSpan.Get, width: colSpan.Get));

        [Fact]
        public void Place_RowEmpty_Empty() =>
            RowBox.Empty
                .Get
                .Place()
                .Placement
                .IsAbsent
                .Should()
                .BeTrue();

        [Property]
        public void Place_RowWith2Children_Expected(
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

            var placedParent = box.Place();
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
        public void Place_AutoSpanAndRowWithChild_ChildIsStretched(PositiveInt bRowSpan)
        {
            // AB
            var box = Make
                .Row(
                    Make.Value("A"),
                    Make.Value("B").SpanRows(bRowSpan.Get))
                .AutoSpan()
                .Get;

            var placedParent = box.Place();
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
        public void Place_ColEmpty_Empty() =>
            ColBox.Empty
                .Get
                .Place()
                .Placement
                .IsAbsent
                .Should()
                .BeTrue();

        [Property]
        public void Place_ColWith2Children_Expected(
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

            var placedParent = box.Place();
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
        public void Place_AutoSpanAndColWithChild_ChildIsStretched(PositiveInt bColSpan)
        {
            // A
            // B
            var box = Make
                .Col(
                    Make.Value("A"),
                    Make.Value("B").SpanCols(bColSpan.Get))
                .AutoSpan()
                .Get;

            var placedParent = box.Place();
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
        public void Place_StackEmpty_Empty() =>
            StackBox.Empty
                .Get
                .Place()
                .Placement
                .IsAbsent
                .Should()
                .BeTrue();

        [Property]
        public void Place_StackWith2Children_Expected(
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

            var placedParent = box.Place();
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
        public void Place_AutoSpanAndStackWithChild_ChildIsStretched(
            PositiveInt bRowSpan,
            PositiveInt bColSpan)
        {
            // B over A
            var box = Make
                .Stack(
                    Make.Value("A"),
                    Make.Value("B").SpanRows(bRowSpan.Get).SpanCols(bColSpan.Get))
                .AutoSpan()
                .Get;

            var placedParent = box.Place();
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
        public void Place_ProtoWithSlot_SlotBoxPlaced()
        {
            const string protoRef = "ProtoRef";
            const string slotRef = "SlotRef";
            var protoBook = Make
                .Col(
                    "A",
                    Make.Value("Slot default value").NameGlobally(slotRef))
                .NameGlobally(protoRef)
                .ToSheet()
                .ToBook()
                .ToBytes();
            var bank = new ProtoBank(List(protoBook));
            var box = bank
                .PlugProtos(Make.Proto(protoBook, protoRef).Add(slotRef, "Slot value").Get);

            var result = box.ValueUnsafe().Place();

            result
                .Slots
                .Single()
                .Box
                .Placement
                .Should()
                .Be(Placement.At(row: 2, col: 1, height: 1, width: 1));
        }

        [Fact]
        public void Place_NonuniqueChildren_Placed()
        {
            var child = Make.Value();
            var box = Make.Row(child, child).Get;

            var result = box.Place();

            result.Placement.Should().Be(Placement.At(row: 1, col: 1, height: 1, width: 2));
        }

        [Fact]
        public void Place_NonUniqueChildren_Placed()
        {
            var child1 = Make.Value();
            var child2 = Make.Value();
            var box = Make.Row(child1, child2, child1, child2).Get;

            var result = box.Place();

            result.Placement.Should().Be(Placement.At(row: 1, col: 1, height: 1, width: 4));
        }

        [Fact]
        public void Place_SameBoxInDifferentParents_Placed()
        {
            var child = Make.Value();
            var parent1 = Make.Row(child);
            var parent2 = Make.Row(child);
            var box = Make.Row(parent1, parent2).Get;

            var result = box.Place();

            result.Placement.Should().Be(Placement.At(row: 1, col: 1, height: 1, width: 2));
        }

        [Fact]
        public void Place_AutoSpanAndHigherSibling_GrewUp()
        {
            const int siblingHigh = 10;
            var root = Make
                .Row(
                    Make.Value(),
                    Make.Value().SpanRows(siblingHigh))
                .AutoSpanRows()
                .Get;

            var result = root.Place();

            result.Children[0].Placement.Dimension.Height.Should().Be(siblingHigh);
        }

        [Fact]
        public void Place_AutoSpanAndHigherUncle_GrewUp()
        {
            const int uncleHigh = 10;
            var root = Make
                .Row(
                    Make.Col(Make.Value()),
                    Make.Value().SpanRows(uncleHigh))
                .AutoSpanRows()
                .Get;

            var result = root.Place();

            result.Children[0].Children[0].Placement.Dimension.Height.Should().Be(uncleHigh);
        }

        [Fact]
        public void Place_AutoSpanAndWiderSibling_GrewUp()
        {
            const int siblingWidth = 10;
            var root = Make
                .Col(
                    Make.Value(),
                    Make.Value().SpanCols(siblingWidth))
                .AutoSpanCols()
                .Get;

            var result = root.Place();

            result.Children[0].Placement.Dimension.Width.Should().Be(siblingWidth);
        }

        [Fact]
        public void Place_AutoSpanAndWiderUncle_GrewUp()
        {
            const int uncleWidth = 10;
            var root = Make
                .Col(
                    Make.Row(Make.Value()),
                    Make.Value().SpanCols(uncleWidth))
                .AutoSpanCols()
                .Get;

            var result = root.Place();

            result.Children[0].Children[0].Placement.Dimension.Width.Should().Be(uncleWidth);
        }

        [Property(Arbitrary = new[] { typeof(PlacingBoxArb) })]
        public void Place_IsNotAbsent_ChildIsInsideParent(BoxCore root)
        {
            var rootWithoutAutoSpan = root.Place();
            var rootWithAutoSpan = root.With(rowAutoSpan: true, colAutoSpan: true).Place();

            Assert(rootWithoutAutoSpan, rootWithoutAutoSpan.ImmediateDescendants());
            Assert(rootWithAutoSpan, rootWithAutoSpan.ImmediateDescendants());

            static void Assert(BoxCore parent, IEnumerable<BoxCore> children)
            {
                foreach (var child in children.Where(x => !x.Placement.IsAbsent))
                {
                    var fromRow = parent.Placement.Position.Row;
                    var toRow = parent.Placement.ToRow;
                    child.Placement.Position.Row.Should().BeInRange(fromRow, toRow);
                    child.Placement.ToRow.Should().BeInRange(fromRow, toRow);

                    var fromCol = parent.Placement.Position.Col;
                    var toCol = parent.Placement.ToCol;
                    child.Placement.Position.Col.Should().BeInRange(fromCol, toCol);
                    child.Placement.ToCol.Should().BeInRange(fromCol, toCol);

                    Assert(child, child.ImmediateDescendants());
                }
            }
        }

        [Property(Arbitrary = new[] { typeof(PlacingBoxArb) })]
        public void Place_ColsWhoseChildrenCanGrow_ChildrenGrewUpToParentHeight(BoxCore root)
        {
            var result = root.With(rowAutoSpan: true).Place();

            var colsWhoseChildrenCanGrow = result
                .SelfAndDescendants()
                .Where(x => x.Type == BoxType.Col)
                .Where(ChildrenCanGrowHeight);

            foreach (var box in colsWhoseChildrenCanGrow)
            {
                box.Children.Sum(x => x.Placement.Dimension.Height).Should().Be(box.Placement.Dimension.Height);
            }
        }

        [Property(Arbitrary = new[] { typeof(PlacingBoxArb) })]
        public void Place_RowsWhoseChildrenCanGrow_ChildrenGrewUpToParentHeight(BoxCore root)
        {
            var result = root.With(rowAutoSpan: true).Place();

            foreach (var box in result.SelfAndDescendants().Where(x => x.Type == BoxType.Row))
            {
                foreach (var child in box.Children.Where(ChildrenCanGrowHeight))
                {
                    child.Placement.Dimension.Height.Should().Be(box.Placement.Dimension.Height);
                }
            }
        }

        [Property(Arbitrary = new[] { typeof(PlacingBoxArb) })]
        public void Place_RowsWhoseChildrenCanGrow_ChildrenGrewUpToParentWidth(BoxCore root)
        {
            var result = root.With(colAutoSpan: true).Place();

            var rowsWhoseChildrenCanGrow = result
                .SelfAndDescendants()
                .Where(x => x.Type == BoxType.Row)
                .Where(ChildrenCanGrowWidth);

            foreach (var box in rowsWhoseChildrenCanGrow)
            {
                box.Children.Sum(x => x.Placement.Dimension.Width).Should().Be(box.Placement.Dimension.Width);
            }
        }

        [Property(Arbitrary = new[] { typeof(PlacingBoxArb) })]
        public void Place_ColsWhoseChildrenCanGrow_ChildrenGrewUpToParentWidth(BoxCore root)
        {
            var result = root.With(colAutoSpan: true).Place();

            foreach (var box in result.SelfAndDescendants().Where(x => x.Type == BoxType.Col))
            {
                foreach (var child in box.Children.Where(ChildrenCanGrowWidth))
                {
                    child.Placement.Dimension.Width.Should().Be(box.Placement.Dimension.Width);
                }
            }
        }

        private static bool ChildrenCanGrowHeight(BoxCore box) =>
            box
                .Descendants()
                .Where(x => x.Type == BoxType.Value)
                .Any(x => x.RowSpan.IsNone);

        private static bool ChildrenCanGrowWidth(BoxCore box) =>
            box
                .Descendants()
                .Where(x => x.Type == BoxType.Value)
                .Any(x => x.ColSpan.IsNone);
    }
}