namespace BookFx.Usage.S3CalendarDomain
{
    using System;
    using System.Linq;

    public static class DayExt
    {
        public static bool IsHoliday(this Day day) => day.Date.DayOfWeek.IsHoliday();

        public static Day Next(this Day day, DayOfWeek dayOfWeek) =>
            Enumerable.Range(1, 7).Select(x => day.Date.AddDays(x)).First(x => x.DayOfWeek == dayOfWeek);
    }
}