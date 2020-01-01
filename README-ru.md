# BookFx

[![nuget-img][]][nuget-link]
[![build-img][]][build-link]
[![tests-img][]][tests-link]
[![quality-img][]][quality-link]

[en][] | **ru**

BookFx предоставляет экстремально эффективный способ создания книг Excel любой сложности.

```c#
Make.Book().ToBytes()
```

И у нас уже есть xlsx с одним пустым листом!

![book-empty][]

Более приветливый вариант:

```c#
Make.Value("Hi, World!").ToSheet().ToBook().ToBytes()
```

![box-a1][]

Композиция вместо высчитывания адресов,
компонентный подход для устранения сложности,
функциональный стиль вместо VBA-подобной императивщины,
прототипирование компонентов со слотами из частей заготовленных xlsx-файлов,
формулы, шрифты, цвета, выравнивания, форматы.
Обо всем этом ниже.

BookFx требует [.NET Standard 2.0][.net standard 2.0] и зависит от [EPPlus][epplus], который используется в качестве рендера в формат XLSX Office Open XML.

## Содержание

- [Установка](#установка)
- [Начало работы](#начало-работы)
- [Примеры использования](#примеры-использования)
- [Концепции](#концепции)
    - [Описание модели](#описание-модели)
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

### Создание

Точка входа BookFx это класс `Make`.
Он предоставляет методы для создания книг, листов, box'ов, стилей и границ.
Точка выхода это `ToBytes()`.
Основные свойства классов BookFx могут быть заданы с использованием перегрузок методов `Make`:

```c#
Make.Book(Make.Sheet("First"), Make.Sheet("Second")).ToBytes()
```

Другой способ это chaining:

```c#
Make
    .Book()
    .Add(Make.Sheet().Name("First"))
    .Add(Make.Sheet().Name("Second"))
    .ToBytes()
```

Результат обоих примеров один и тот же.

![sheet-name][]

### Боксы

`Box` это строительный блок листа.
Он может быть составным и всегда описывает диапазон — ячейку, строку, колонку или прямоугольник из ячеек.

В таблице перечислены все типы box'ов.

| Тип | Создание | Назначение |
| -- | -- | -- |
| `ValueBox` | `Make.Value()` | Значения, формулы и пустые диапазоны. |
| `RowBox` | `Make.Row()` | Размещение box'ов слева направо. |
| `ColBox` | `Make.Col()` | Размещение box'ов сверху вниз. |
| `StackBox` | `Make.Stack()` | Размещение box'ов слоями. |
| `ProtoBox` | `Make.Proto()` | Составление из шаблонов. |

Что если поместить в `RowBox` два `ValueBox`'а?

```c#
Make.Row(Make.Value("Box A1"), Make.Value("Box B1")).ToSheet().ToBook().ToBytes()
```

![box-a1-b1][]

Логично. Два значения расположились в строку!

### Преобразования

В `ValueBox` реализованы [неявные преобразования][implicit convertions] из всех необходимых типов значений.
Что это значит?
Это значит, что не обязательно каждый раз повторять `Make.Value`,
потому что `ValueBox` будет создан автоматически.

```c#
Make.Row("Box A1", "Box B1").ToSheet().ToBook().ToBytes()
```

![box-a1-b1][]

Результат тот же самый!

### Композиция

Опишем вот такую шапку таблицы:

![box-header][]

В терминах BookFx она может быть представлена как композиция элементов, вот так:

![box-header-model][]

Легко увидеть общий паттерн.

![box-plan-fact-model][]

Мы можем извлечь этот паттерн в функцию:

```c#
Box PlanFact(string title) => Make.Col(title, Make.Row("Plan", "Fact"));
```

По сути это простой компонент. Протестируем его:

```c#
PlanFact("Beginning of year").ToSheet().ToBook().ToBytes()
```

![box-plan-fact][]

Теперь используем `PlanFact` как компонент и добавим стиль:

```c#
Box Head() => Make
    .Row()
    .Add("Code", "Name", PlanFact("Beginning of year"), PlanFact("End of year"))
    .Style(Make.Style().Center().Middle().Bold().DefaultBorder());
```

О, так это же еще один компонент!
Что же это получается?
Компонент – это функция. Функция – это компонент...
Похоже в наших руках безграничные возможности!

Теперь все просто:

```c#
Head().AutoSpan().ToSheet().ToBook().ToBytes()
```

![box-header][]

Готово.

Про `AutoSpan` можно почитать в разделе [Охват и объединение](#охват-и-объединение).
Полная версия в примерах использования ниже.

## Примеры использования

Проект `BookFx.Usage` содержит несколько примеров использования. Результаты его выполнения сохраняются в папку `src\BookFx.Usage\bin\Debug\netcoreapp2.1\Results\`.

### [S1Table.cs][s1table.cs]

Это полная версия примера из [Начало работы](#начало-работы). Она создает таблицу с итогами.

### [S2Style.cs][s2style.cs]

Этот пример демонстрирует некоторые возможности стилей BookFx.

### [S3Calendar.cs][s3calendar.cs]

Ого! Календарь!

[![s-3-calendar][]][s3calendar.cs]

### [S5ProtoSheet.cs][s5protosheet.cs]

Это демонстрация добавления в книгу листов из существующих книг. См. также [Прототипирование](#прототипирование).

### [S6ProtoBox.cs][s6protobox.cs]

Это пример [прототипирования](#прототипирование).

### [S7BalanceSheet.cs][s7balancesheet.cs]

Это демонстрация создания балансового отчета с шапкой и переменным количеством колонок и строк данных.

## Концепции

### Описание модели

Модель книги BookFx чем-то похожа на HTML [DOM][dom].
Это дерево узлов, которое рендерится в xlsx-файл.

Такой подход открывает множество возможностей:

- узлы могут быть реализованы как повторно используемые компоненты;
- размещение узлов определяется их композицией;
- к иерархии узлов удобно применять стили;
- для unit-тестирования не требуется рендеринг книги.

Модель BookFx неизменяемая ([immutable][immutable object]),
и у методов библиотеки нет побочных эффектов ([side effects][side effect]),
поэтому BookFx позволяет писать чистые функции ([pure functions][pure function]).

Таким образом, BookFx:

- помогает лучше структурировать описание книги;
- берет на себя вычисления размеров и адресов диапазонов;
- избавляет от необходимости использовать императивный API, пришедший из мира VBA-макросов;
- раскрывает возможности функционального программирования.

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

И для значений, и для формул предназначен `ValueBox`,
который может быть создан либо с помощью `Make.Value`, либо с помощью неявного преобразования из всех необходимых типов значений: `string`, `int`, `decimal`, `DateTime` и др.

Формулы должны начинаться с `=`. Для экранирования используется символ `'`. Поддерживается только стиль ссылок `R1C1`.

```c#
Make.Value("=SUM(RC[1]:RC[3])")
```

### Прототипирование

BookFx поддерживает использование фрагментов других книг в качестве прототипов:

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

См. пример [S6ProtoBox.cs][s6protobox.cs].

Также BookFx поддерживает добавление целых листов из других книг:

```c#
Make.Sheet("New Sheet Name", protoBook, "Prototype Sheet Name");
```

Из xlsx-файла `protoBook` будет скопирован лист `"Prototype Sheet Name"` и переименован в `"New Sheet Name"`. См. также другие перегрузки `Make.Sheet`.

См. также пример [S5ProtoSheet.cs][s5protosheet.cs].

## Справочник API

- `Make` - фабрика элементов модели
    - `Make.Book` - создать `Book`
    - `Make.Sheet` - создать `Sheet`
    - `Make.Row` - создать `RowBox`
    - `Make.Col` - создать `ColBox`
    - `Make.Stack` - создать `StackBox`
    - `Make.Value` - создать `ValueBox`
    - `Make.Proto` - создать `ProtoBox`
    - `Make.Style` - создать `BoxStyle`
    - `Make.Border` - создать `BoxBorder`
- `Book` - книга Excel
    - `Book.Add` - добавить лист(ы)
    - `Book.ToBytes` - выполнить рендеринг в xlsx
- `Sheet` - лист Excel
    - `Sheet.Name` - задать имя листа
    - `Sheet.TabColor` - задать цвет вкладки
    - `Sheet.SetPageView` - задать режим отображения
    - `Sheet.Portrait` - задать книжную ориентацию страницы
    - `Sheet.Landscape` - задать альбомную ориентацию страницы
    - `Sheet.Margin` - задать поля страницы
    - `Sheet.Fit` - вписать содержимое по высоте и ширине
    - `Sheet.FitToHeight` - вписать содержимое по высоте
    - `Sheet.FitToWidth` - вписать содержимое по ширине
    - `Sheet.Scale` - задать масштаб
    - `Sheet.ToBook` - создать `Book` с одним листом
- `Box` - box любого вида
    - `Box.Name` - задать имя области
    - `Box.AutoSpan` - включить режим `AutoSpan` для строк и колонок
    - `Box.AutoSpanRows` - включить режим `AutoSpan` для строк
    - `Box.AutoSpanCols` - включить режим `AutoSpan` для колонок
    - `Box.Style` - задать стиль
    - `Box.SizeRows` - задать высоту строк
    - `Box.SizeCols` - задать ширину колонок
    - `Box.SetPrintArea` - задать область печати по box'у
    - `Box.HideRows` - скрыть строки
    - `Box.HideCols` - скрыть колонки
    - `Box.Freeze` - закрепить область box'а
    - `Box.FreezeRows` - закрепить строки box'а
    - `Box.FreezeCols` - закрепить колонки box'а
    - `Box.AutoFilter` - добавить автофильтр в нижней строке box'а
    - `Box.ToSheet` - создать `Sheet` с корневым box'ом
- `RowBox` - строка box'ов
    - `RowBox.Add` - добавить box(ы) в строку
- `ColBox` - колонка box'ов
    - `ColBox.Add` - добавить box(ы) в колонку
- `StackBox` - стопка box'ов
    - `StackBox.Add` - добавить box(ы) в стопку
- `ValueBox` - box со значением, формулой, или пустой box
    - `ValueBox.Span` - охватить строки и колонки
    - `ValueBox.SpanRows` - охватить строки
    - `ValueBox.SpanCols` - охватить колонки
    - `ValueBox.Merge` - объединить ячейки
- `ProtoBox` - прототип
    - `ProtoBox.Add` - добавить box в слот
- `BoxStyle` - стиль
    - `BoxStyle.Borders` - задать границы
    - `BoxStyle.DefaultBorder` - задать обычные границы
    - `BoxStyle.Font` - задать шрифт, его размер и цвет
    - `BoxStyle.Back` - задать цвет фона
    - `BoxStyle.Color` - задать цвет шрифта и/или цвет фона
    - `BoxStyle.Bold` - выделить жирным
    - `BoxStyle.Italic` - выделить курсивом
    - `BoxStyle.Underline` - подчеркнуть
    - `BoxStyle.Strike` - зачеркнуть
    - `BoxStyle.Wrap` - задать перенос текста
    - `BoxStyle.Shrink` - задать автоподбор ширины текста
    - `BoxStyle.Align` - задать выравнивание
    - `BoxStyle.Left` - выровнять по левому краю
    - `BoxStyle.Center` - выровнять горизонтально по центру
    - `BoxStyle.CenterContinuous` - выровнять горизонтально по центру смежных ячеек
    - `BoxStyle.Right` - выровнять по правому краю
    - `BoxStyle.Top` - выровнять по верхнему краю
    - `BoxStyle.Middle` - выровнять вертикально по середине
    - `BoxStyle.Bottom` - выровнять по нижнему краю
    - `BoxStyle.Rotate` - повернуть текст
    - `BoxStyle.Indent` - задать отступ
    - `BoxStyle.Format` - задать произвольный формат
    - `BoxStyle.DefaultFormat` - задать формат `General` (Общий)
    - `BoxStyle.Text` - задать формат `@` (Текстовый)
    - `BoxStyle.Integer` - задать формат `#,##0` (Целое число)
    - `BoxStyle.Money` - задать формат `#,##0.00` (Числовой с разделителем триад)
    - `BoxStyle.Percent` - задать формат `0%` (Процентный, целое)
    - `BoxStyle.DateShort` - задать формат `dd.mm.yyyy` (Краткий формат даты)
- `BoxBorder` - граница
    - `BoxBorder.Restrict` - ограничить часть box'а, к которой применяется граница
    - `BoxBorder.Style` - задать стиль границы
    - `BoxBorder.Color` - задать цвет границы
- `EnumerableExt` - расширения IEnumerable для типов BookFx
    - `IEnumerable<Box>.ToBook` - создать `Book` из листов
    - `IEnumerable<Box>.ToRow` - создать `RowBox` из других box'ов
    - `IEnumerable<Box>.ToCol` - создать `ColBox` из других box'ов
    - `IEnumerable<Box>.ToStack` - создать `StackBox` из других box'ов
    - `IEnumerable<BoxStyle>.Mix` - смешать стили и создать новый

## Лицензия

Этот проект лицензируется под [LGPL-3.0-or-later](https://spdx.org/licenses/LGPL-3.0-or-later.html).

### Уведомление об авторском праве

``` txt
BookFx. Composing Excel spreadsheets based on a tree of nested components like the HTML DOM.
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
[build-img]: https://img.shields.io/appveyor/ci/bookfx/bookfx/master
[build-link]: https://ci.appveyor.com/project/bookfx/bookfx
[tests-img]: https://img.shields.io/appveyor/tests/bookfx/bookfx/master
[tests-link]: https://ci.appveyor.com/project/bookfx/bookfx
[quality-img]: https://img.shields.io/codacy/grade/bccabc29ebf943ba89ac4a1d03b5e70a/master
[quality-link]: https://app.codacy.com/gh/bookfx/bookfx/dashboard
[en]: README.md
[dom]: https://en.wikipedia.org/wiki/Document_Object_Model
[immutable object]: https://en.wikipedia.org/wiki/Immutable_object
[side effect]: https://en.wikipedia.org/wiki/Side_effect_(computer_science)
[pure function]: https://en.wikipedia.org/wiki/Functional_programming#Pure_functions
[.net standard 2.0]: https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md
[epplus]: https://github.com/JanKallman/EPPlus
[implicit convertions]: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/conversions#implicit-conversions
[book-empty]: docs/img/book-empty.svg "Пустая книга"
[sheet-name]: docs/img/sheet-name.svg "Именованные листы"
[box-a1]: docs/img/box-a1.svg "Box A1"
[box-a1-b1]: docs/img/box-a1-b1.svg "Box A1, Box B1"
[box-header]: docs/img/box-header.svg "Box шапки"
[box-header-model]: docs/img/box-header-model.svg "Модель box'а шапки"
[box-plan-fact]: docs/img/box-plan-fact.svg "PlanFact box"
[box-plan-fact-model]: docs/img/box-plan-fact-model.svg "Модель PlanFact box'а"
[s1table.cs]: src/BookFx.Usage/S1Table.cs
[s2style.cs]: src/BookFx.Usage/S2Style.cs
[s3calendar.cs]: src/BookFx.Usage/S3Calendar.cs
[s5protosheet.cs]: src/BookFx.Usage/S5ProtoSheet.cs
[s6protobox.cs]: src/BookFx.Usage/S6ProtoBox.cs
[s7balancesheet.cs]: src/BookFx.Usage/S7BalanceSheet.cs
[s-3-calendar]: docs/img/s-3-calendar-ru.png "Результат S3Calendar.cs"
