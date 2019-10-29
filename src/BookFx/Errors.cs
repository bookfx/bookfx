﻿namespace BookFx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Epplus.Constraint;

    internal static class Errors
    {
        public static class Book
        {
            [Pure]
            public static SheetNameIsNotUniqueError SheetNameIsNotUnique(string name) =>
                new SheetNameIsNotUniqueError(name);

            [Pure]
            public static BoxNameIsNotUniqueError BoxNameIsNotUnique(string name) =>
                new BoxNameIsNotUniqueError(name);

            [Pure]
            public static AggregateError Aggregate(IEnumerable<Error> inners) =>
                new AggregateError(inners);

            public sealed class SheetNameIsNotUniqueError : Error
            {
                public SheetNameIsNotUniqueError(string name)
                    : base($"Sheet name «{name}» is not unique.")
                {
                }
            }

            public sealed class BoxNameIsNotUniqueError : Error
            {
                public BoxNameIsNotUniqueError(string name)
                    : base($"Box name «{name}» is not unique.")
                {
                }
            }

            public sealed class AggregateError : Error
            {
                public AggregateError(IEnumerable<Error> inners)
                    : base("The book has following errors.", inners)
                {
                }
            }
        }

        public static class Sheet
        {
            [Pure]
            public static NameIsInvalidError NameIsInvalid(string name) => new NameIsInvalidError(name);

            [Pure]
            public static RootBoxWidthTooBigError RootBoxWidthTooBig(int width) => new RootBoxWidthTooBigError(width);

            [Pure]
            public static RootBoxHeightTooBigError RootBoxHeightTooBig(int height) =>
                new RootBoxHeightTooBigError(height);

            [Pure]
            public static ManyPrintAreasError ManyPrintAreas() => new ManyPrintAreasError();

            [Pure]
            public static ScaleIsInvalidError ScaleIsInvalid(int scale) => new ScaleIsInvalidError(scale);

            [Pure]
            public static ManyFrozenRowsError ManyFrozenRows() => new ManyFrozenRowsError();

            [Pure]
            public static ManyFrozenColsError ManyFrozenCols() => new ManyFrozenColsError();

            [Pure]
            public static AggregateError Aggregate(
                SheetCore sheet,
                IEnumerable<Error> inners) =>
                new AggregateError(sheet, inners);

            public sealed class NameIsInvalidError : Error
            {
                public NameIsInvalidError(string name)
                    : base(
                        $"Sheet name «{name}» is invalid. " +
                        "Take care that name is not empty, " +
                        "name length is not longer than 31 and " +
                        @"name is free of following characters: ':', '\', '/', '?', '*', '[' or ']'.")
                {
                }
            }

            public sealed class RootBoxWidthTooBigError : Error
            {
                public RootBoxWidthTooBigError(int width)
                    : base($"Root box width {width} is too big. Max width is {MaxColumn}.")
                {
                }
            }

            public sealed class RootBoxHeightTooBigError : Error
            {
                public RootBoxHeightTooBigError(int height)
                    : base($"Root box height {height} is too big. Max height is {MaxRow}.")
                {
                }
            }

            public sealed class ManyPrintAreasError : Error
            {
                public ManyPrintAreasError()
                    : base("Many print areas found.")
                {
                }
            }

            public sealed class ScaleIsInvalidError : Error
            {
                public ScaleIsInvalidError(int scale)
                    : base($"Scale {scale} is invalid. Scale should be from {MinScale} to {MaxScale}.")
                {
                }
            }

            public sealed class ManyFrozenRowsError : Error
            {
                public ManyFrozenRowsError()
                    : base("Many frozen rows found.")
                {
                }
            }

            public sealed class ManyFrozenColsError : Error
            {
                public ManyFrozenColsError()
                    : base("Many frozen cols found.")
                {
                }
            }

            public sealed class AggregateError : Error
            {
                public AggregateError(SheetCore sheet, IEnumerable<Error> inners)
                    : base($"{GetSheetPart(sheet)} has following errors.", inners)
                {
                }

                private static string GetSheetPart(SheetCore sheet) =>
                    sheet.Name.Map(name => $"Sheet «{name}»").GetOrElse("Unnamed sheet");
            }
        }

        public static class Box
        {
            [Pure]
            public static RowSpanIsInvalidError RowSpanIsInvalid(int size) => new RowSpanIsInvalidError(size);

            [Pure]
            public static ColSpanIsInvalidError ColSpanIsInvalid(int size) => new ColSpanIsInvalidError(size);

            [Pure]
            public static RowSizeCountIsInvalidError RowSizeCountIsInvalid(int sizeCount, int boxHeight) =>
                new RowSizeCountIsInvalidError(sizeCount, boxHeight);

            [Pure]
            public static ColSizeCountIsInvalidError ColSizeCountIsInvalid(int sizeCount, int boxWidth) =>
                new ColSizeCountIsInvalidError(sizeCount, boxWidth);

            [Pure]
            public static RowSizesAreInvalidError RowSizesAreInvalid(float size) => new RowSizesAreInvalidError(size);

            [Pure]
            public static RowSizesAreInvalidError RowSizesAreInvalid(float size, IEnumerable<float> others) =>
                new RowSizesAreInvalidError(others.Prepend(size));

            [Pure]
            public static ColSizesAreInvalidError ColSizesAreInvalid(float size) => new ColSizesAreInvalidError(size);

            [Pure]
            public static ColSizesAreInvalidError ColSizesAreInvalid(float size, IEnumerable<float> others) =>
                new ColSizesAreInvalidError(others.Prepend(size));

            [Pure]
            public static BoxValueTypeDisallowedError ValueTypeIsDisallowed(Type type) =>
                new BoxValueTypeDisallowedError(type);

            [Pure]
            public static AggregateError Aggregate(
                BoxCore box,
                IEnumerable<Error> inners) =>
                new AggregateError(box, inners);

            public sealed class RowSpanIsInvalidError : Error
            {
                public RowSpanIsInvalidError(int size)
                    : base(
                        $"Box row span {size} is invalid. " +
                        $"Row span should be from 1 to {MaxRow}.")
                {
                }
            }

            public sealed class ColSpanIsInvalidError : Error
            {
                public ColSpanIsInvalidError(int size)
                    : base(
                        $"Box column span size {size} is invalid. " +
                        $"Column span size should be from 1 to {MaxColumn}.")
                {
                }
            }

            public sealed class RowSizeCountIsInvalidError : Error
            {
                public RowSizeCountIsInvalidError(int sizesCount, int boxHeight)
                    : base($"Number of box row sizes ({sizesCount}) greater than number of rows ({boxHeight}).")
                {
                }
            }

            public sealed class ColSizeCountIsInvalidError : Error
            {
                public ColSizeCountIsInvalidError(int sizesCount, int boxWidth)
                    : base($"Number of box column sizes ({sizesCount}) greater than number of columns ({boxWidth}).")
                {
                }
            }

            public sealed class RowSizesAreInvalidError : Error
            {
                public RowSizesAreInvalidError(float size)
                    : base($"Box row size {size} is invalid. " + ShouldPart())
                {
                }

                public RowSizesAreInvalidError(IEnumerable<float> sizes)
                    : base($"Box row sizes {string.Join(", ", sizes)} are invalid. " + ShouldPart())
                {
                }

                private static string ShouldPart() =>
                    $"Row size should be from {MinRowSize} to {MaxRowSize}.";
            }

            public sealed class ColSizesAreInvalidError : Error
            {
                public ColSizesAreInvalidError(float size)
                    : base($"Box column size {size} is invalid. " + ShouldPart())
                {
                }

                public ColSizesAreInvalidError(IEnumerable<float> sizes)
                    : base($"Box column sizes {string.Join(", ", sizes)} are invalid. " + ShouldPart())
                {
                }

                private static string ShouldPart() =>
                    $"Column size should be from {MinColSize} to {MaxColSize}.";
            }

            public sealed class BoxValueTypeDisallowedError : Error
            {
                public BoxValueTypeDisallowedError(Type type)
                    : base($"Value type is «{type}». It is disallowed.")
                {
                }
            }

            public sealed class AggregateError : Error
            {
                public AggregateError(BoxCore box, IEnumerable<Error> inners)
                    : base(
                        $"{box.Name.Map(name => $"Box «{name}»").GetOrElse("Unnamed box")} has following errors.",
                        inners)
                {
                }
            }
        }

        public static class Style
        {
            [Pure]
            public static FontSizeIsInvalidError FontSizeIsInvalid(float size) => new FontSizeIsInvalidError(size);

            [Pure]
            public static IndentSizeIsInvalidError IndentSizeIsInvalid(int size) => new IndentSizeIsInvalidError(size);

            [Pure]
            public static FontNameIsEmptyError FontNameIsEmpty() => new FontNameIsEmptyError();

            [Pure]
            public static FormatIsEmptyError FormatIsEmpty() => new FormatIsEmptyError();

            public sealed class FontSizeIsInvalidError : Error
            {
                public FontSizeIsInvalidError(float size)
                    : base(
                        $"Font size {size} is invalid. " +
                        $"Font size should be from {MinFontSize} to {MaxFontSize}.")
                {
                }
            }

            public sealed class IndentSizeIsInvalidError : Error
            {
                public IndentSizeIsInvalidError(int size)
                    : base(
                        $"Indent size {size} is invalid. " +
                        $"Indent size should be from {MinIndentSize} to {MaxIndentSize}.")
                {
                }
            }

            public sealed class FontNameIsEmptyError : Error
            {
                public FontNameIsEmptyError()
                    : base("Font name should be not empty.")
                {
                }
            }

            public sealed class FormatIsEmptyError : Error
            {
                public FormatIsEmptyError()
                    : base("Format should be not empty.")
                {
                }
            }
        }

        public static class Position
        {
            [Pure]
            public static IsInvalidError IsInvalid(int row, int col) => new IsInvalidError(row, col);

            public class IsInvalidError : Error
            {
                public IsInvalidError(int row, int col)
                    : base(
                        $"Position R{row}C{col} is invalid. " +
                        $"Row should be from 1 to {MaxRow}, " +
                        $"col should be from 1 to {MaxColumn}.")
                {
                }
            }
        }

        public static class Excel
        {
            [Pure]
            public static ProtoRefNotFoundError ProtoRefNotFound(Reference reference) =>
                new ProtoRefNotFoundError(reference);

            [Pure]
            public static SheetProtoNameShouldBeSpecifiedError SheetProtoNameShouldBeSpecified() =>
                new SheetProtoNameShouldBeSpecifiedError();

            [Pure]
            public static SheetProtoNameNotFoundError SheetProtoNameNotFound(string name) =>
                new SheetProtoNameNotFoundError(name);

            public sealed class ProtoRefNotFoundError : Error
            {
                public ProtoRefNotFoundError(Reference reference)
                    : base($"ProtoBox reference «{reference}» not found.")
                {
                }
            }

            public sealed class SheetProtoNameShouldBeSpecifiedError : Error
            {
                public SheetProtoNameShouldBeSpecifiedError()
                    : base("Sheet ProtoName should be specified for multisheet ProtoBook.")
                {
                }
            }

            public sealed class SheetProtoNameNotFoundError : Error
            {
                public SheetProtoNameNotFoundError(string name)
                    : base($"Sheet ProtoName «{name}» not found.")
                {
                }
            }
        }
    }
}