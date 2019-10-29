# BookFx

![Nuget](https://img.shields.io/nuget/vpre/BookFx)

BookFx proposes to use the functional style to describe Excel workbooks.
To implement this BookFx provides model like the HTML [DOM].
It is a tree of renderable nodes.

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
- unlocks the door to the functional programming.

In addition, BookFx supports prototyping. That means that you can use parts of xlsx files as components. Slots are supported.

BookFx requires [.NET Standard 2.0] and depends on [EPPlus] which is used as a render to XLSX Office Open XML format.

## Table of Contents

- [Installation](#installation)
- [Getting Started](#getting-started)
- [Examples of Use](#examples-of-use)
- [Concepts](#concepts)
    - [Layout System](#Layout-System)
    - [Values and Formulas](#Values-and-Formulas)
    - [Spanning and Merging](#Spanning-and-Merging)
    - [Bordering](#Bordering)
    - [Style Mixing](#Style-Mixing)
    - [Sizing](#Sizing)
    - [Prototyping](#Prototyping)
    - [Testing](#Testing)
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

Here is the complete list of boxes. To getting started we need only the first three.

Type | Creating | Destination
- | - | -
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

Coming soon.

### Values and Formulas

Coming soon.

### Spanning and Merging

Coming soon.

### Bordering

Coming soon.

### Style Mixing

Coming soon.

### Sizing

Coming soon.

### Prototyping

Coming soon.

### Testing

Coming soon.

## API Reference

- `Make`
    - `Make.Book`
    - `Make.Sheet`
    - `Make.Row`
    - `Make.Col`
    - `Make.Stack`
    - `Make.Value`
    - `Make.Proto`
    - `Make.Style`
    - `Make.Border`
- `Book`
    - `Book.Add`
    - `Book.ToBytes`
- `Sheet`
    - `Sheet.Name`
    - `Sheet.SetPageView`
    - `Sheet.Scale`
    - `Sheet.ToBook`
- `Box`
    - `Box.Name`
    - `Box.Style`
    - `Box.SizeRows`
    - `Box.SizeCols`
    - `Box.SetPrintArea`
    - `Box.HideRows`
    - `Box.HideCols`
    - `Box.FreezeRows`
    - `Box.FreezeCols`
    - `Box.ToSheet`
- `CompositeBox`
    - `CompositeBox.Add`
- `ValueBox`
    - `ValueBox.SpanRows`
    - `ValueBox.SpanCols`
    - `ValueBox.Span`
    - `ValueBox.Merge`
- `ProtoBox`
    - `ProtoBox.Add`
- `BoxStyle`
    - `BoxStyle.Borders`
    - `BoxStyle.DefaultBorder`
    - `BoxStyle.Font`
    - `BoxStyle.Back`
    - `BoxStyle.Color`
    - `BoxStyle.Bold`
    - `BoxStyle.Italic`
    - `BoxStyle.Underline`
    - `BoxStyle.Strike`
    - `BoxStyle.Wrap`
    - `BoxStyle.Align`
    - `BoxStyle.Left`
    - `BoxStyle.Center`
    - `BoxStyle.Right`
    - `BoxStyle.Top`
    - `BoxStyle.Middle`
    - `BoxStyle.Bottom`
    - `BoxStyle.Indent`
    - `BoxStyle.Format`
    - `BoxStyle.DefaultFormat`
    - `BoxStyle.Text`
    - `BoxStyle.Money`
    - `BoxStyle.Percent`
    - `BoxStyle.DateShort`
- `BoxBorder`
    - `BoxBorder.Restrict`
    - `BoxBorder.Style`
    - `BoxBorder.Color`

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
[s-3-calendar]: docs/img/s-3-calendar.png "S3Calendar.cs result"
