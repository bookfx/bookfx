namespace BookFx.Usage
{
    using System;

    public static class S5ProtoSheet
    {
        public static byte[] Create()
        {
            byte[] preexistingCalendarBookBytes = S3Calendar.Create(DateTime.Now.AddMonths(2).Year);
            byte[] preexistingStyleBookBytes = S2Style.Create();

            return Make
                .Book()
                .Add(Make.Sheet(preexistingCalendarBookBytes, "en").Name("First Sheet"))
                .Add(Make.Sheet(preexistingCalendarBookBytes, "ru").Name("Second Sheet"))
                .Add(Make.Sheet(preexistingStyleBookBytes).Name("Third Sheet"))
                .Add(Make.Value("I am a regular sheet.").ToSheet().Name("Fourth Sheet"))
                .ToBytes();
        }
    }
}