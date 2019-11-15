# BookFx

[![nuget-img]][nuget-link]

**en** | [ru]

BookFx proposes to use the functional style to describe Excel workbooks.
To implement this it provides model like the HTML [DOM].
Without details, BookFx model is a tree of nodes, which renders to a xlsx-file.

This approach opens multiple opportunities:

- nodes can be implemented as reusable components;
- placing of nodes can be driven by composition of nodes;
- hierarchy of nodes is convenient to applying styles;
- unit testing of components doesn't require to render workbook.

BookFx model is [immutable][Immutable object],
and methods of the library has no [side effects][Side effect],
hence BookFx allows you to write [pure functions][Pure function].

Thus, BookFx:

- helps to better structure a describing of workbook;
- takes the pain out of the calculating sizes and addresses of ranges;
- saves you the trouble of using imperative API came from the world of VBA macros;
- opens up opportunities of the functional programming.

In addition, BookFx supports prototyping. That means that you can use parts of xlsx files as components. Slots are supported too.

BookFx requires [.NET Standard 2.0] and depends on [EPPlus] which is used as a render to XLSX Office Open XML format.

## Table of Contents

- [Installation](#installation)
- [Getting Started](#getting-started)
- [Examples of Use](#examples-of-use)
- [Concepts](#concepts)
    - [Layout System](#Layout-System)
    - [Spanning and Merging](#Spanning-and-Merging)
    - [Values and Formulas](#Values-and-Formulas)
    - [Prototyping](#Prototyping)
- [API Reference](#api-reference)
- [License](#license)

## Installation

```shell
PM> Install-Package BookFx
```

## Getting Started

The `Make` class is the entry point of BookFx. It exposes methods to create books, sheets, boxes, styles and borders. The exit point is `ToBytes()`.

```c#
Make.Book().ToBytes()
```

![book-empty]

The main properties of BookFx classes can be defined using overloads of `Make` methods.

```c#
Make.Book(Make.Sheet("First"), Make.Sheet("Second")).ToBytes()
```

Another way is chaining.

```c#
Make
    .Book()
    .Add(Make.Sheet().Name("First"))
    .Add(Make.Sheet().Name("Second"))
    .ToBytes()
```

Both examples gives the same result.

![sheet-name]

`Box` is the building block of a spreadsheet. It can be composite and always describes a range — cell, row, column or rectangle of cells. There are several types of box. The first is `ValueBox`.

```c#
Make.Value("Box A1").ToSheet().ToBook().ToBytes()
```

![box-a1]

The second box type is `RowBox`.

```c#
Make.Row(Make.Value("Box A1"), Make.Value("Box B1")).ToSheet().ToBook().ToBytes()
```

![box-a1-b1]

`ValueBox` can be implicitly converted from some of built-in types, so above expression can be written shorter.

```c#
Make.Row("Box A1", "Box B1").ToSheet().ToBook().ToBytes()
```

![box-a1-b1]

Here is the complete list of boxes. To getting started we need the first three.

Type | Creating | Destination
-- | - | -
`ValueBox` | `Make.Value()` | Values, formulas and empty ranges.
`RowBox` | `Make.Row()` | From left to right box placement.
`ColBox` | `Make.Col()` | From top to bottom box placement.
`StackBox` | `Make.Stack()` | Layer by layer box placement.
`ProtoBox` | `Make.Proto()` | Composing from templates.

Let's describe this table header.

![box-header]

In terms of BookFx it can be thought of as composition of elements, like this.

![box-header-model]

Is is easy to see the common pattern.

![box-plan-fact-model]

We can extract this pattern in a function. Essentially it is a component.

```c#
Box PlanFact(string title) => Make.Col(title, Make.Row("Plan", "Fact"));
```

Test it.

```c#
PlanFact("Beginning of year").ToSheet().ToBook().ToBytes()
```

![box-plan-fact]

Now let's use `PlanFact` as component.

```c#
Make
    .Row()
    .Add("Code", "Name", PlanFact("Beginning of year"), PlanFact("End of year"))
    .AutoSpan()
    .Style(Make.Style().Center().Middle().Bold().DefaultBorder())
    .ToSheet()
    .ToBook()
    .ToBytes()
```

![box-header]

Done.

The full version is in examples of use, see below.

## Examples of Use

The `BookFx.Usage` project contains a few examples of use. Run it and get results in the `src\BookFx.Usage\bin\Debug\netcoreapp2.1\Results\` folder.

### [S1Table.cs]

This is a full version of [Getting Started](#getting-started) example. It makes a table with totals.

### [S2Style.cs]

This demonstrates some style features of BookFx.

### [S3Calendar.cs]

Wow! Calendar!

[![s-3-calendar]][S3Calendar.cs]

## Concepts

### Layout System

Every sheet of book can contain one root box. It is placed in the upper left corner.

Composite boxes contain other boxes and are stretched to fit them:

- boxes are placed in row from left to right inside of `RowBox`;
- boxes are placed in column from top to bottom inside of `ColBox`;
- boxes are placed in stack one above the other inside of `StackBox`.

A `ValueBox` cannot contains other boxes, but can be placed in several cells.
More about it see in the [Spanning and Merging](#spanning-and-merging) section.

The size of a `ProtoBox` is always equal to the size of its prototype, and inner boxes of `ProtoBox` are placed using the mechanism of slots. Further in the [Prototyping](#prototyping) section.

### Spanning and Merging

A `ValueBox`, like any other box type, can be placed in several cells.
A `ValueBox` methods `SpanRows`, `SpanCols` and their combination `Span` are used to define the number of spanned cells.
The cell spanning inside of `ValueBox` is works like `rowspan` and `colspan` HTML table attributes, but in BookFx cells inside a `ValueBox` is not always should be merged.

The `Merge` method is used to merge cells, but BookFx merges ranges of a `ValueBox` automatically if the box has a value or a formula. In some cases it may be require do not merge cells automatically. For that there is the `Merge(false)`.

In addition to automatically merging, BookFx supports automatically spanning, which is activated by methods `AutoSpanRows`, `AutoSpanCols` and their combination `AutoSpan`.
In this mode a box and its inners are stretched to sizes of their containers through the last stretchable `ValueBox`.
A `ValueBox` is considered to be stretchable when its `Span` is not specified and its `AutoSpan` is not deactivated. We've used `AutoSpan` in the [Getting Started](#getting-started) section.

### Values and Formulas

The `ValueBox` is intended for values and formulas. It can be created either by `Make.Value` or using implicit convertion from one of built-in types: `string`, `int`, `decimal`, `DateTime`, etc.

Formulas should begin with `=`. The `'` is used for escaping. Only `R1C1` reference style is supported.

```c#
Make.Value("=SUM(RC[1]:RC[3])")
```

### Prototyping

BookFx supports using parts of other books as prototypes.

```c#
Make
    .Proto(protoBook, "Prototype1")
    .Add("Slot1", "Value1")
    .Add("Slot2", Make.Row("Value2", "Value3"));
```

Here

- `protoBook` – `byte[]` of a xlsx-file content;
- `"Prototype1"` – name of the range in `protoBook`;
- `"Slot1"` and `"Slot2"` – names of ranges in `Prototype1`, in which other boxes can be placed.

Also BookFx supports adding whole sheets from other books.

```c#
Make.Sheet("New Sheet Name", protoBook, "Prototype Sheet Name");
```

`"Prototype Sheet Name"` sheet will be copied from `protoBook` xlsx-file and then it will be renamed to `"New Sheet Name"`. See also other overloads of `Make.Sheet`.

## API Reference

- `Make` - the model elements factory
    - `Make.Book` - make a `Book`
    - `Make.Sheet` - make a `Sheet`
    - `Make.Row` - make a `RowBox`
    - `Make.Col` - make a `ColBox`
    - `Make.Stack` - make a `StackBox`
    - `Make.Value` - make a `ValueBox`
    - `Make.Proto` - make a `ProtoBox`
    - `Make.Style` - make a `BoxStyle`
    - `Make.Border` - make a `BoxBorder`
- `Book` - an Excel book
    - `Book.Add` - add sheet(s)
    - `Book.ToBytes` - render to xlsx
- `Sheet` - an Excel sheet
    - `Sheet.Name` - define a sheet name
    - `Sheet.TabColor` - define tab color
    - `Sheet.SetPageView` - define page view
    - `Sheet.Fit` - fit the height and the width of printout to pages
    - `Sheet.FitToHeight` - fit the height of printout to pages
    - `Sheet.FitToWidth` - fit the width of printout to pages
    - `Sheet.Scale` - define a scale
    - `Sheet.ToBook` - make a `Book` with one sheet
- `Box` - a box of any type
    - `Box.Name` - define a name of the range
    - `Box.AutoSpan` - activate `AutoSpan` mode for rows and columns
    - `Box.AutoSpanRows` - activate `AutoSpan` mode for rows
    - `Box.AutoSpanCols` - activate `AutoSpan` mode for columns
    - `Box.Style` - define a style
    - `Box.SizeRows` - define heights of rows
    - `Box.SizeCols` - define widths of columns
    - `Box.SetPrintArea` - define print area by the box
    - `Box.HideRows` - hide rows
    - `Box.HideCols` - hide columns
    - `Box.Freeze` - freeze the box range
    - `Box.FreezeRows` - freeze rows of the box
    - `Box.FreezeCols` - freeze columns of the box
    - `Box.AutoFilter` - add auto filter to the lower row of the box
    - `Box.ToSheet` - make a `Sheet` with the root box
- `RowBox` - a row of boxes
    - `RowBox.Add` - add box(es) in row
- `ColBox` - a column of boxes
    - `ColBox.Add` - add box(es) in column
- `StackBox` - a stack of boxes
    - `StackBox.Add` - add box(es) in stack
- `ValueBox` - a box with a value, with a formula or an empty box
    - `ValueBox.Span` - span rows and columns
    - `ValueBox.SpanRows` - span rows
    - `ValueBox.SpanCols` - span columns
    - `ValueBox.Merge` - merge cells
- `ProtoBox` - a prototype
    - `ProtoBox.Add` - add a box into a slot
- `BoxStyle` - a style
    - `BoxStyle.Borders` - define borders
    - `BoxStyle.DefaultBorder` - define regular borders
    - `BoxStyle.Font` - define a font, its size and color
    - `BoxStyle.Back` - define a background color
    - `BoxStyle.Color` - define a font color and/or a background color
    - `BoxStyle.Bold` - in bold
    - `BoxStyle.Italic` - in italic
    - `BoxStyle.Underline` - underline
    - `BoxStyle.Strike` - strike
    - `BoxStyle.Wrap` - define a text wrap
    - `BoxStyle.Align` - define an alignment
    - `BoxStyle.Left` - align to the left
    - `BoxStyle.Center` - align at the center horizontally
    - `BoxStyle.Right` - align to the right
    - `BoxStyle.Top` - align to the top
    - `BoxStyle.Middle` - align at the middle vertically
    - `BoxStyle.Bottom` - align to the bottom
    - `BoxStyle.Indent` - define an indent
    - `BoxStyle.Format` - define a custom format
    - `BoxStyle.DefaultFormat` - define the `General` format
    - `BoxStyle.Text` - define define the `@` format (Text)
    - `BoxStyle.Integer` - define the `#,##0` format (Integer)
    - `BoxStyle.Money` - define the `#,##0.00` format (Number with a thousands separator)
    - `BoxStyle.Percent` - define the `0%` format (Percentage, integer)
    - `BoxStyle.DateShort` - define the `dd.mm.yyyy` format (Short date)
- `BoxBorder` - a border
    - `BoxBorder.Restrict` - restrict a part of a box to which the border applied
    - `BoxBorder.Style` - define a border style
    - `BoxBorder.Color` - define a border color

## License

The project is licensed under the [LGPL-3.0-or-later](https://spdx.org/licenses/LGPL-3.0-or-later.html).

### The copyright notice

``` txt
BookFx. Making complex Excel workbooks out of simple components.
Copyright (c) 2019 Zhenya Gusev
```

### The license notice

``` txt
This library is free software: you can redistribute it and/or modify
it under the terms of the GNU Lesser General Public License as published by
the Free Software Foundation, either version 3 of the License, or
any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public License
along with this library. If not, see <https://www.gnu.org/licenses/>.
```

<!-- links -->

[nuget-img]: https://img.shields.io/nuget/v/BookFx?color=informational
[nuget-link]: https://www.nuget.org/packages/BookFx
[ru]: README-ru.md
[DOM]: https://en.wikipedia.org/wiki/Document_Object_Model
[Immutable object]: https://en.wikipedia.org/wiki/Immutable_object
[Side effect]: https://en.wikipedia.org/wiki/Side_effect_(computer_science)
[Pure function]: https://en.wikipedia.org/wiki/Functional_programming#Pure_functions
[.NET Standard 2.0]: https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md
[EPPlus]: https://github.com/JanKallman/EPPlus
[book-empty]: docs/img/book-empty.svg "Empty book"
[sheet-name]: docs/img/sheet-name.svg "Named sheets"
[box-a1]: docs/img/box-a1.svg "Box A1"
[box-a1-b1]: docs/img/box-a1-b1.svg "Box A1, Box B1"
[box-header]: docs/img/box-header.svg "Header box"
[box-header-model]: docs/img/box-header-model.svg "Header box model"
[box-plan-fact]: docs/img/box-plan-fact.svg "PlanFact box"
[box-plan-fact-model]: docs/img/box-plan-fact-model.svg "PlanFact box model"
[S1Table.cs]: src/BookFx.Usage/S1Table.cs
[S2Style.cs]: src/BookFx.Usage/S2Style.cs
[S3Calendar.cs]: src/BookFx.Usage/S3Calendar.cs
[s-3-calendar]: docs/img/s-3-calendar-en.png "S3Calendar.cs result"
