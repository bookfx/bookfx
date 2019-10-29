namespace BookFx.Usage.S3CalendarDomain
{
    using System;

    public static class DayOfWeekExt
    {
        public static bool IsHoliday(this DayOfWeek dayOfWeek) =>
            dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday;
    }
}