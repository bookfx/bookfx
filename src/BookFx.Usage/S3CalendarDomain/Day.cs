namespace BookFx.Usage.S3CalendarDomain
{
    using System;

    public class Day
    {
        private Day(Month month, int dayOfMonth) => Date = new DateTime(month.Year, month.MonthOfYear, dayOfMonth);

        public int DayOfMonth => Date.Day;

        public DateTime Date { get; }

        public static implicit operator Day(DateTime date) => Of(Month.Of(date.Year, date.Month), date.Day);

        public static Day Of(Month month, int dayOfMonth) => new Day(month, dayOfMonth);
    }
}