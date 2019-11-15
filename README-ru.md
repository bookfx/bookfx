# BookFx

[![nuget-img]][nuget-link]

[en] | **ru**

BookFx предлагает использовать функциональный стиль для описания книг Excel.
Для этого предоставляется модель наподобие HTML [DOM].
Если без подробностей, модель BookFx – это дерево узлов, которое рендерится в xlsx-файл.

Такой подход открывает множество возможностей:

- узлы могут быть реализованы как повторно используемые компоненты;
- размещение узлов определяется их композицией;
- к иерархии узлов удобно применять стили;
- для unit-тестирования не требуется рендеринг книги.

Модель BookFx неизменяемая ([immutable][Immutable object]),
и у методов библиотеки нет побочных эффектов ([side effects][Side effect]),
поэтому BookFx позволяет писать чистые функции ([pure functions][Pure function]).

Таким образом, BookFx:

- помогает лучше структурировать описание книги;
- берет на себя вычисления размеров и адресов диапазонов;
- избавляет от необходимости использовать императивный API, пришедший из мира VBA-макросов;
- раскрывает возможности функционального программирования.

Кроме того, BookFx поддерживает прототипирование. Это означает, что части xlsx-файлов можно использовать в качестве компонентов. Слоты тоже поддерживаются.

BookFx требует [.NET Standard 2.0] и зависит от [EPPlus], который используется в качестве рендера в формат XLSX Office Open XML.

## Содержание

- [Установка](#установка)
- [Начало работы](#начало-работы)
- [Примеры использования](#примеры-использования)
- [Концепции](#концепции)
    - [Система размещения](#система-размещения)
    - [Охват и объединение](#охват-и-объединение)
    - [Значения и формулы](#значения-и-формулы)
    - [Прототипирование](#прототипирование)
- [Справочник API](#справочник-api)
- [Лицензия](#лицензия)

## Установка

```shell
PM> Install-Package BookFx
```

## Начало работы

Точка входа BookFx это класс `Make`. Он предоставляет методы для создания книг, листов, box'ов, стилей и границ. Точка выхода это `ToBytes()`.

```c#
Make.Book().ToBytes()
```

![book-empty]

Основные свойства классов BookFx могут быть заданы с использованием перегрузок методов `Make`.

```c#
Make.Book(Make.Sheet("First"), Make.Sheet("Second")).ToBytes()
```

Другой способ это chaining.

```c#
Make
    .Book()
    .Add(Make.Sheet().Name("First"))
    .Add(Make.Sheet().Name("Second"))
    .ToBytes()
```

Результат обоих примеров один и тот же.

![sheet-name]

`Box` это строительный блок листа. Он может быть составным и всегда описывает диапазон — ячейку, строку, колонку или прямоугольник из ячеек. Есть несколько типов box'ов. Первый это `ValueBox`.

```c#
Make.Value("Box A1").ToSheet().ToBook().ToBytes()
```

![box-a1]

Второй тип это `RowBox`.

```c#
Make.Row(Make.Value("Box A1"), Make.Value("Box B1")).ToSheet().ToBook().ToBytes()
```

![box-a1-b1]

`ValueBox` неявно преобразовывается из некоторых встроенных типов, поэтому выражение выше может быть написано короче.

```c#
Make.Row("Box A1", "Box B1").ToSheet().ToBook().ToBytes()
```

![box-a1-b1]

В таблице перечислены все типы box'ов. Для начала работы нам понадобятся первые три.

Тип | Создание | Назначение
-- | - | -
`ValueBox` | `Make.Value()` | Значения, формулы и пустые диапазоны.
`RowBox` | `Make.Row()` | Размещение box'ов слева направо.
`ColBox` | `Make.Col()` | Размещение box'ов сверху вниз.
`StackBox` | `Make.Stack()` | Размещение box'ов слоями.
`ProtoBox` | `Make.Proto()` | Составление из шаблонов.

Опишем вот такую шапку таблицы.

![box-header]

В терминах BookFx она может быть представлена как композиция элементов, вот так.

![box-header-model]

Легко увидеть общий паттерн.

![box-plan-fact-model]

Мы можем извлечь этот паттерн в функцию. По сути это компонент.

```c#
Box PlanFact(string title) => Make.Col(title, Make.Row("Plan", "Fact"));
```

Протестируем его.

```c#
PlanFact("Beginning of year").ToSheet().ToBook().ToBytes()
```

![box-plan-fact]

Теперь используем `PlanFact` как компонент.

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

Готово.

Полная версия в примерах использования ниже.

## Примеры использования

Проект `BookFx.Usage` содержит несколько примеров использования. Результаты его выполнения сохраняются в папку `src\BookFx.Usage\bin\Debug\netcoreapp2.1\Results\`.

### [S1Table.cs]

Это полная версия примера из [Начало работы](#начало-работы). Она создает таблицу с итогами.

### [S2Style.cs]

Этот пример демонстрирует некоторые возможности стилей BookFx.

### [S3Calendar.cs]

Ого! Календарь!

[![s-3-calendar]][S3Calendar.cs]

## Концепции

### Система размещения

Каждый лист книги может содержать один корневой box. Он размещается в верхнем левом углу.

Составные box'ы содержат внутри себя другие box'ы и растягиваются чтобы вместить их:

- внутри `RowBox` box'ы располагаются в строку слева направо;
- внутри `ColBox` box'ы располагаются в колонку сверху вниз;
- внутри `StackBox` box'ы располагаются в стопку один над другим.

`ValueBox` не может содержать другие box'ы, но может располагаться на нескольких ячейках.
Подробнее об этом в разделе [Охват и объединение](#охват-и-объединение).

Размер `ProtoBox` всегда равен размеру прототипа, а внутренние box'ы `ProtoBox`'а размещаются с использованием механизма слотов. Подробнее в разделе [Прототипирование](#прототипирование).

### Охват и объединение

`ValueBox`, как и любой другой тип box'а, может располагаться на нескольких ячейках.
Для определения количества охватываемых `ValueBox` ячеек используются методы `SpanRows`, `SpanCols` и их комбинация `Span`.
Охват ячеек внутри `ValueBox` похож на работу атрибутов `rowspan` и `colspan` HTML-таблиц, однако в BookFx ячейки внутри `ValueBox` не всегда должны быть объединены.

Для объединения ячеек используется метод `Merge`, но BookFx объединяет диапазоны `ValueBox` автоматически, если в box'е есть значение или формула. В некоторых случаях может потребоваться не объединять ячейки автоматически. Для этого используется `Merge(false)`.

Кроме автоматического объединения BookFx поддерживает автоматический охват, который включается методами `AutoSpanRows`, `AutoSpanCols` и их комбинацией `AutoSpan`. В этом режиме box и все включенные в него box'ы растягиваются до размеров своих контейнеров за счет последних растягиваемых `ValueBox`'ов. `ValueBox` считается растягиваемым, если для него не задан `Span` и для него не выключен `AutoSpan`. Мы уже использовали `AutoSpan` в разделе [Начало работы](#начало-работы).

### Значения и формулы

И для значений, и для формул предназначен `ValueBox`, который может быть создан либо с помощью `Make.Value`, либо с помощью неявного преобразования из одного из базовых типов: `string`, `int`, `decimal`, `DateTime` и др.

Формулы должны начинаться с `=`. Для экранирования используется символ `'`. Поддерживается только стиль ссылок `R1C1`.

```c#
Make.Value("=SUM(RC[1]:RC[3])")
```

### Прототипирование

BookFx поддерживает использование фрагментов других книг в качестве прототипов.

```c#
Make
    .Proto(protoBook, "Prototype1")
    .Add("Slot1", "Value1")
    .Add("Slot2", Make.Row("Value2", "Value3"));
```

Здесь

- `protoBook` – `byte[]` содержимого xlsx-файла;
- `"Prototype1"` – имя диапазона в `protoBook`;
- `"Slot1"` и `"Slot2"` – имена диапазонов внутри `Prototype1`, в которых могут размещаться другие box'ы.

Также BookFx поддерживает добавление целых листов из других книг.

```c#
Make.Sheet("New Sheet Name", protoBook, "Prototype Sheet Name");
```

Из xlsx-файла `protoBook` будет скопирован лист `"Prototype Sheet Name"` и переименован в `"New Sheet Name"`. См. также другие перегрузки `Make.Sheet`.

## Справочник API

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
    - `Sheet.TabColor`
    - `Sheet.SetPageView`
    - `Sheet.Fit`
    - `Sheet.FitToHeight`
    - `Sheet.FitToWidth`
    - `Sheet.Scale`
    - `Sheet.ToBook`
- `Box`
    - `Box.Name`
    - `Box.AutoSpan`
    - `Box.AutoSpanRows`
    - `Box.AutoSpanCols`
    - `Box.Style`
    - `Box.SizeRows`
    - `Box.SizeCols`
    - `Box.SetPrintArea`
    - `Box.HideRows`
    - `Box.HideCols`
    - `Box.Freeze`
    - `Box.FreezeRows`
    - `Box.FreezeCols`
    - `Box.AutoFilter`
    - `Box.ToSheet`
- `CompositeBox`
    - `CompositeBox.Add`
- `ValueBox`
    - `ValueBox.Span`
    - `ValueBox.SpanRows`
    - `ValueBox.SpanCols`
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
    - `BoxStyle.Integer`
    - `BoxStyle.Money`
    - `BoxStyle.Percent`
    - `BoxStyle.DateShort`
- `BoxBorder`
    - `BoxBorder.Restrict`
    - `BoxBorder.Style`
    - `BoxBorder.Color`

## Лицензия

Этот проект лицензируется под [LGPL-3.0-or-later](https://spdx.org/licenses/LGPL-3.0-or-later.html).

### Уведомление об авторском праве

``` txt
BookFx. Making complex Excel workbooks out of simple components.
Copyright (c) 2019 Zhenya Gusev
```

### Уведомление о лицензии

``` txt
Эта библиотека является свободным программным обеспечением:
Вы можете распространять и/или изменять ее,
соблюдая условия Меньшей генеральной публичной лицензии GNU,
опубликованной Фондом свободного программного обеспечения;
либо редакции 3 Лицензии, либо любой редакции, выпущенной позже.

Эта библиотека распространяется в расчете на то, что она окажется полезной,
но БЕЗ КАКИХ-ЛИБО ГАРАНТИЙ, включая подразумеваемую гарантию КАЧЕСТВА либо
ПРИГОДНОСТИ ДЛЯ ОПРЕДЕЛЕННЫХ ЦЕЛЕЙ. Ознакомьтесь с Меньшей генеральной
публичной лицензией GNU для получения более подробной информации.

Вы должны были получить копию Меньшей генеральной публичной лицензии GNU
вместе с этой библиотекой. Если Вы ее не получили, то перейдите по адресу:
<http://www.gnu.org/licenses/>.
```

<!-- links -->

[nuget-img]: https://img.shields.io/nuget/v/BookFx?color=informational
[nuget-link]: https://www.nuget.org/packages/BookFx
[en]: README.md
[DOM]: https://en.wikipedia.org/wiki/Document_Object_Model
[Immutable object]: https://en.wikipedia.org/wiki/Immutable_object
[Side effect]: https://en.wikipedia.org/wiki/Side_effect_(computer_science)
[Pure function]: https://en.wikipedia.org/wiki/Functional_programming#Pure_functions
[.NET Standard 2.0]: https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md
[EPPlus]: https://github.com/JanKallman/EPPlus
[book-empty]: docs/img/book-empty.svg "Пустая книга"
[sheet-name]: docs/img/sheet-name.svg "Именованные листы"
[box-a1]: docs/img/box-a1.svg "Box A1"
[box-a1-b1]: docs/img/box-a1-b1.svg "Box A1, Box B1"
[box-header]: docs/img/box-header.svg "Box шапки"
[box-header-model]: docs/img/box-header-model.svg "Модель box'а шапки"
[box-plan-fact]: docs/img/box-plan-fact.svg "PlanFact box"
[box-plan-fact-model]: docs/img/box-plan-fact-model.svg "Модель PlanFact box'а"
[S1Table.cs]: src/BookFx.Usage/S1Table.cs
[S2Style.cs]: src/BookFx.Usage/S2Style.cs
[S3Calendar.cs]: src/BookFx.Usage/S3Calendar.cs
[s-3-calendar]: docs/img/s-3-calendar-ru.png "Результат S3Calendar.cs"
