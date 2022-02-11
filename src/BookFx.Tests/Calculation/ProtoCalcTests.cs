namespace BookFx.Tests.Calculation
{
    using BookFx.Calculation;
    using BookFx.Epplus;
    using BookFx.Functional;
    using FluentAssertions;
    using Xunit;
    using static BookFx.Functional.F;

    public class ProtoCalcTests
    {
        [Fact]
        public void PlugProtos_ProtoBoxWithoutSlots_RangeIsExpected()
        {
            const string protoRef = "ProtoRef";
            // A
            var protoBook = Make.Value().NameGlobally(protoRef).ToSheet().ToBook().ToBytes();
            var box = Make.Proto(protoBook, protoRef).Get;
            var bank = new ProtoBank(List(protoBook));

            var result = bank.PlugProtos(box);

            result
                .ToOption()
                .Bind(x => x.Proto)
                .Bind(x => x.Range)
                .Map(x => x.GetPosition())
                .Should()
                .Be(Some(Position.At(row: 1, col: 1)));
        }

        [Fact]
        public void PlugProtos_ProtoBoxWithSlotAtR2C1_SlotPositionIsR2C1()
        {
            const string protoRef = "ProtoRef";
            const string slotRef = "SlotRef";
            // A
            // B
            var protoBook = Make
                .Col("A", Make.Value("B").NameGlobally(slotRef))
                .NameGlobally(protoRef)
                .ToSheet()
                .ToBook()
                .ToBytes();
            var box = Make.Proto(protoBook, protoRef).Add(slotRef, Make.Value()).Get;
            var bank = new ProtoBank(List(protoBook));

            var result = bank.PlugProtos(box);

            result
                .ToOption()
                .Bind(x => x.Slots.Head())
                .Bind(x => x.Position)
                .Should()
                .Be(Some(Position.At(row: 2, col: 1)));
        }
    }
}