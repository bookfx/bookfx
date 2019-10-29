namespace BookFx.Usage
{
    using System;

    public static class Program
    {
        public static void Main()
        {
            ResultStore.Save(S1Table.Create(), $"{nameof(S1Table)}.xlsx");
            ResultStore.Save(S2Style.Create(), $"{nameof(S2Style)}.xlsx");
            ResultStore.Save(S3Calendar.Create(DateTime.Now.Year), $"{nameof(S3Calendar)}.xlsx");
        }
    }
}