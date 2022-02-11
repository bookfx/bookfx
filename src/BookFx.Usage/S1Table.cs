namespace BookFx.Usage
{
    using System.Linq;
    using static BookFx.Make;

    public static class S1Table
    {
        private const string TotalFormula = "=SUM(Data C)";

        public static byte[] Create() =>
            Col()
                .Add(Head())
                .Add(Col()
                    .Add(Data("00001", "First long name with auto fit", 1000, 1020, 1500, 1550))
                    .Add(Data("00002", "Second long name with auto fit", 1200, 1240, 1600, 1690))
                    .NameLocally("Data"))
                .Add(Total())
                .AutoSpan()
                .Style(Style().DefaultBorder())
                .SetPrintArea()
                .ToSheet()
                .ToBook()
                .ToBytes();

        private static Box Head() =>
            Row()
                .SizeCols(TrackSize.Fit, TrackSize.Fit, 10, 10, 10, 10)
                .Add("Code", "Name", HeadPlanFact("Beginning of year"), HeadPlanFact("End of year"))
                .Style(Style().Center().Middle().Bold());

        private static Box HeadPlanFact(string title) => Col(title, Row("Plan", "Fact"));

        private static Box Data(string code, string name, params decimal[] values) =>
            Row()
                .Add(code, name)
                .Add(values.Select(value => Value(value, Style().Money())));

        private static Box Total() =>
            Row()
                .Add(Value("Grand total").SpanCols(2))
                .Add(Enumerable.Repeat(Value(TotalFormula, Style().Money()), 4))
                .Style(Style().Bold());
    }
}