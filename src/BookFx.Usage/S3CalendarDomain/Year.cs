namespace BookFx.Usage.S3CalendarDomain
{
    public class Year
    {
        private Year(int number) => Number = number;

        public int Number { get; }

        public static implicit operator Year(int number) => new Year(number);

        public static implicit operator int(Year year) => year.Number;
    }
}