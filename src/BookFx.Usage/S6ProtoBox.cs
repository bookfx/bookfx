namespace BookFx.Usage
{
    using System.Drawing;

    public static class S6ProtoBox
    {
        public static byte[] Create()
        {
            byte[] preexistingStyleBookBytes = S2Style.Create();

            return Make
                .Row()

                // use the prototype as-is, with with default slot content
                .Add(Make.Proto(preexistingStyleBookBytes, S2Style.PrototypeName))

                // use the prototype with a custom slot content
                .Add(Make
                    .Proto(preexistingStyleBookBytes, S2Style.PrototypeName)
                    .Add(S2Style.SlotName, Make.Value("On pink").Style(Make.Style().Back(Color.Pink))))

                // use the prototype with another custom slot content
                .Add(Make
                    .Proto(preexistingStyleBookBytes, S2Style.PrototypeName)
                    .Add(S2Style.SlotName, Make.Value("On gray").Style(Make.Style().Color(Color.White, Color.Gray))))

                .ToSheet()
                .ToBook()
                .ToBytes();
        }
    }
}