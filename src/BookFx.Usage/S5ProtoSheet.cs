namespace BookFx.Usage
{
    using System;

    public static class S5ProtoSheet
    {
        public static byte[] Create()
        {
            byte[] preexistingTableBookBytes = S1Table.Create();
            byte[] preexistingCalendarBookBytes = S3Calendar.Create(DateTime.Now.AddMonths(2).Year);

            return Make
                .Book()
                .Add(Make.Sheet(preexistingTableBookBytes).Name("First Sheet"))
                .Add(Make.Sheet(preexistingCalendarBookBytes, "en").Name("Second Sheet"))
                .Add(Make.Sheet(preexistingCalendarBookBytes, "ru").Name("Third Sheet"))
                .Add(Make.Value("I am a regular sheet.").ToSheet().Name("Fourth Sheet"))
                .ToBytes();
        }
    }
}