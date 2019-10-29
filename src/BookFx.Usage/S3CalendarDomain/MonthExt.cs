namespace BookFx.Usage.S3CalendarDomain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class MonthExt
    {
        public static IEnumerable<Day> Days(this Month month) =>
            Enumerable
                .Range(1, month.DaysInMonth())
                .Select(dayOfMonth => Day.Of(month, dayOfMonth));

        public static Day FirstDay(this Month month) => Day.Of(month, 1);

        private static int DaysInMonth(this Month month) => DateTime.DaysInMonth(month.Year, month.MonthOfYear);
    }
}