namespace BookFx.Usage.S3CalendarDomain
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public static class CultureInfoExt
    {
        public static IEnumerable<DayOfWeek> DaysOfWeek(this CultureInfo culture) =>
            Enumerable.Range(0, 7).Select(x => (DayOfWeek)(((int)culture.DateTimeFormat.FirstDayOfWeek + x) % 7));

        public static string MonthName(this CultureInfo culture, Month month) =>
            culture.DateTimeFormat.GetMonthName(month.MonthOfYear);

        public static string DayOfWeekName(this CultureInfo culture, DayOfWeek dayOfWeek) =>
            culture.DateTimeFormat.ShortestDayNames[(int)dayOfWeek].ToLower(culture);

        public static int FirstWeekLength(this CultureInfo culture, Month month) =>
            month.FirstDay().Next(culture.DateTimeFormat.FirstDayOfWeek).DayOfMonth - 1;
    }
}