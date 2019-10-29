namespace BookFx.Usage.S3CalendarDomain
{
    using System.Collections.Generic;
    using System.Linq;

    public static class YearExt
    {
        public static IEnumerable<Month> GetMonths(this Year year) =>
            Enumerable
                .Range(1, 12)
                .Select(monthOfYear => Month.Of(year, monthOfYear));
    }
}