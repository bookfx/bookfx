namespace BookFx.Usage
{
    using System.Drawing;
    using static BookFx.Make;

    public static class S2Style
    {
        public static string PrototypeName => "ThePrototype"; // will be used in S6ProtoBox

        public static string SlotName => "TheSlot"; // will be used in S6ProtoBox

        public static byte[] Create() =>
            Col()
                .Style(Style()
                    .Borders(
                        Border(BorderParts.Outside, BorderStyle.Medium),
                        Border(BorderParts.Inside, BorderStyle.Thin))
                )
                .Add(Row()
                    .Add(Value("Default"))
                    .Add(Value("Left", Style().Left()))
                    .Add(Value("Center", Style().Center()))
                    .Add(Value("Right", Style().Right()))
                )
                .Add(Row()
                    .Add(Value("Bold", Style().Bold()))
                    .Add(Value("Italic", Style().Italic()))
                    .Add(Value("Underline", Style().Underline()))
                    .Add(Value("Strike", Style().Strike()))
                )
                .Add(Row()
                    .Add(Value("Red", Style().Font(Color.Red)))
                    .Add(Value("On green", Style().Back(Color.LightGreen)).NameGlobally(SlotName))
                    .Add(Value("Arial 12", Style().Font("Arial", 12)))
                    .Add(Value("Wrapped long text", Style().Wrap()))
                )
                .NameGlobally(PrototypeName)
                .ToSheet()
                .ToBook()
                .ToBytes();
    }
}