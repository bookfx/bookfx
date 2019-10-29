namespace BookFx.Usage.S3CalendarDomain
{
    public class Month
    {
        private Month(Year year, int monthOfYear)
        {
            Year = year;
            MonthOfYear = monthOfYear;
        }

        public Year Year { get; }

        public int MonthOfYear { get; }

        public static Month Of(Year year, int monthOfYear) => new Month(year, monthOfYear);
    }
}