namespace BookFx.Usage
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using BookFx.Usage.Functional;
    using BookFx.Usage.S3CalendarDomain;
    using static BookFx.Make;
    using static BookFx.Usage.Functional.F;

    public static class S3Calendar
    {
        public static byte[] Create(Year year) =>
            GetCultures()
                .Select(culture => CalendarSheet(year, culture))
                .ToBook()
                .ToBytes();

        private static IEnumerable<CultureInfo> GetCultures() =>
            List("zh", "es", "en", "ru", "ja", "de", "fr")
                .Select(culture => new CultureInfo(culture));

        private static Sheet CalendarSheet(Year year, CultureInfo culture) =>
            List(CalendarTitleBox(year, culture), CalendarBodyBox(year, culture))
                .ToGridCol(gap: 15)
                .ToSheet()
                .Name(culture.TwoLetterISOLanguageName);

        private static Box CalendarTitleBox(Year year, CultureInfo culture) =>
            Value(year.Number.ToString(culture))
                .AutoSpan()
                .Style(Style.YearTitle);

        private static Box CalendarBodyBox(Year year, CultureInfo culture) =>
            year
                .GetMonths()
                .Select(month => MonthBox(culture, month))
                .ToGrid(width: 3, rowGap: 15, colGap: 5);

        private static Box MonthBox(CultureInfo culture, Month month) =>
            List(MonthTitleBox(culture, month), MonthBodyBox(culture, month))
                .ToGridCol(gap: 4.5)
                .SizeCols(Enumerable.Repeat(TrackSize.Fit, 7));

        private static Box MonthTitleBox(CultureInfo culture, Month month) =>
            Value(culture.MonthName(month))
                .AutoSpan()
                .Style(Style.MonthTitle);

        private static Box MonthBodyBox(CultureInfo culture, Month month) =>
            Col(
                WeekHeadBox(culture),
                DayGridBox(culture, month));

        private static Box DayGridBox(CultureInfo culture, Month month) =>
            Enumerable
                .Repeat(Value(), 7 - culture.FirstWeekLength(month))
                .Concat(month.Days().Select(DayBox))
                .ToGrid(width: 7)
                .Style(Style.Day);

        private static Box WeekHeadBox(CultureInfo culture) =>
            culture
                .DaysOfWeek()
                .Select(dayOfWeek => WeekDayBox(culture, dayOfWeek))
                .ToRow()
                .Style(Style.WeekHead);

        private static Box WeekDayBox(CultureInfo culture, DayOfWeek dayOfWeek) =>
            Value(culture.DayOfWeekName(dayOfWeek))
                .Style(dayOfWeek.IsHoliday() ? Style.Holiday : Style.Workday);

        private static Box DayBox(Day day) =>
            Value(day.DayOfMonth)
                .Style(day.IsHoliday() ? Style.Holiday : Style.Workday);

        private static Box ToGrid(this IEnumerable<Box> boxes, int width) =>
            boxes
                .Split(width)
                .Select(Row)
                .ToCol();

        private static Box ToGrid(this IEnumerable<Box> boxes, int width, double rowGap, double colGap) =>
            boxes
                .Split(width)
                .Select(chunk => chunk.ToGridRow(colGap))
                .ToGridCol(rowGap);

        private static Box ToGridRow(this IEnumerable<Box> boxes, double gap) =>
            boxes
                .Delimit(Value().SizeCols(gap))
                .ToRow();

        private static Box ToGridCol(this IEnumerable<Box> boxes, double gap) =>
            boxes
                .Delimit(Value().SizeRows(gap))
                .ToCol();

        private static class Style
        {
            public static readonly BoxStyle YearTitle = Style().Font(16).Center().Bold();
            public static readonly BoxStyle MonthTitle = Style().Font(14).Center().Bold();
            public static readonly BoxStyle Day = Style().Bold();
            public static readonly BoxStyle Workday = Style().Color(Color.Black);
            public static readonly BoxStyle Holiday = Style().Color(Color.Red);
            public static readonly BoxStyle WeekHead = Style().Font(9).Center().Middle();
        }
    }
}