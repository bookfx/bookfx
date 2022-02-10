namespace BookFx.Usage
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    using static BookFx.Make;

    public static class S7BalanceSheet
    {
        public static byte[] Create()
        {
            var dataToWriteToExcel = GetAccountingData();

            return
                Col()
                    .Add(MakeRowWithReportHeader("Company", dataToWriteToExcel.Company))
                    .Add(MakeRowWithReportHeader("Report", dataToWriteToExcel.ReportName))
                    .Add(MakeRowWithDateReportHeader("Date", dataToWriteToExcel.ReportDate))
                    .Add(MakeEmptyRow())
                    .Add(MakeRangeWithAccountingData(dataToWriteToExcel.ColumnLabels, dataToWriteToExcel.DataColumns))
                    .SetPrintArea()
                    .ToSheet()
                    .Name(dataToWriteToExcel.ReportName)
                    .ToBook()
                    .ToBytes();

            static Box MakeRowWithReportHeader(string type, string description)
            {
                return Row().Add(Value(type, Style().Bold())).Add(Value(description, Style().Italic()));
            }

            static Box MakeRowWithDateReportHeader(string type, DateTime date)
            {
                return Row().Add(Value(type, Style().Bold())).Add(Value(date, Style().Format("YYYY-MM-DD").Italic()));
            }

            static Box MakeEmptyRow()
            {
                return Row().Add(Value(string.Empty));
            }

            static Box MakeRangeWithAccountingData(IList<string> columnLabels, IEnumerable<DataColumn> dataColumns)
            {
                var retRow = Row();

                retRow = retRow.Add(MakeLabelColumn(columnLabels));

                foreach (var dataColumn in dataColumns)
                {
                    retRow = retRow.Add(MakeDataColumn(dataColumn));
                }

                return retRow;
            }

            static Box MakeLabelColumn(IList<string> columnLabels)
            {
                var retCol = Col().SizeCols(18).Style(Style().Wrap());

                retCol = retCol.Add(Row().Add(Value(columnLabels[0], Style().Bold())));

                for (int i = 1; i < columnLabels.Count; i++)
                {
                    retCol = retCol.Add(Row().Add(Value(columnLabels[i])));
                }

                return retCol;
            }

            static Box MakeDataColumn(DataColumn dataColumn)
            {
                var retCol = Col().SizeCols(12).Style(Style().Font("Consolas", 10));

                retCol = retCol.Add(Row().Add(Value(dataColumn.Date, Style().Text().Bold())));

                foreach (var number in dataColumn.Numbers)
                {
                    retCol = retCol.Add(Row().Add(Value(number)));
                }

                return retCol;
            }
        }

        private static AccountingDataToWriteToExcel GetAccountingData()
        {
            DataColumn firstColumn = new DataColumn("2019/01/31", new List<decimal> { 100, 50, -20, -40, -90 });
            DataColumn secondColumn = new DataColumn("2019/02/28", new List<decimal> { 120, 45, -35, -40, -90 });
            DataColumn thirdColumn = new DataColumn("2019/03/31", new List<decimal> { 95, 45, -10, -40, -90 });

            var columnLabels = new List<string>
            { "Dates", "Current assets", "Long term assets", "Current liabilities", "Long term liabilities", "Equity" };
            var dataColumns = new List<DataColumn> { firstColumn, secondColumn, thirdColumn };

            return new AccountingDataToWriteToExcel("Best company", "Balance Sheet", DateTime.Now, columnLabels, dataColumns);
        }

        private class AccountingDataToWriteToExcel
        {
            public AccountingDataToWriteToExcel(
                string company,
                string reportName,
                DateTime reportDate,
                IEnumerable<string> columnLabels,
                IEnumerable<DataColumn> dataColumns)
            {
                Company = company;
                ReportName = reportName;
                ReportDate = reportDate;
                ColumnLabels = ImmutableList<string>.Empty.AddRange(columnLabels);
                DataColumns = ImmutableList<DataColumn>.Empty.AddRange(dataColumns);
            }

            public string Company { get; }

            public string ReportName { get; }

            public DateTime ReportDate { get; }

            public ImmutableList<string> ColumnLabels { get; }

            public ImmutableList<DataColumn> DataColumns { get; }
        }

        private class DataColumn
        {
            public DataColumn(string date, IEnumerable<decimal> numbers)
            {
                Date = date;
                Numbers = ImmutableList<decimal>.Empty.AddRange(numbers);
            }

            public string Date { get; }

            public ImmutableList<decimal> Numbers { get; }
        }
    }
}
