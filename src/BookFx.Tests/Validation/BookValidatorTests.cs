namespace BookFx.Tests.Validation
{
    using BookFx.Functional;
    using BookFx.Validation;
    using FluentAssertions;
    using Xunit;

    public class BookValidatorTests
    {
        [Fact]
        public void SheetNameUniqueness_TwoUnnamedSheets_Valid()
        {
            var book = Make.Book(Make.Sheet(), Make.Sheet()).Get;

            var result = BookValidator.SheetNameUniqueness(book);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void SheetNameUniqueness_TwoUniqueSheetNames_Valid()
        {
            var book = Make.Book(Make.Sheet("A"), Make.Sheet("B")).Get;

            var result = BookValidator.SheetNameUniqueness(book);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void SheetNameUniqueness_NonUniqueSheetNames_Invalid()
        {
            var book = Make.Book(Make.Sheet("A"), Make.Sheet("A")).Get;

            var result = BookValidator.SheetNameUniqueness(book);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void BoxGlobalNameUniqueness_UniqueBoxNames_Valid()
        {
            var book = Make
                .Book(
                    Make.Sheet(Make.Row(Make.Value().NameGlobally("AA"), Make.Value().NameGlobally("AB"))),
                    Make.Sheet(Make.Row(Make.Value().NameGlobally("BA"), Make.Value().NameGlobally("BB"))))
                .Get;

            var result = BookValidator.BoxGlobalNameUniqueness(book);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void BoxGlobalNameUniqueness_NonUniqueBoxNames_Invalid()
        {
            var book = Make
                .Book(
                    Make.Sheet(Make.Value().NameGlobally("A")),
                    Make.Sheet(Make.Value().NameGlobally("A")))
                .Get;

            var result = BookValidator.BoxGlobalNameUniqueness(book);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Sheets_OneValidSheet_Valid()
        {
            var book = Make.Book(Make.Sheet()).Get;

            var result = BookValidator.Sheets(book);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Sheets_OneInvalidSheet_Invalid()
        {
            var book = Make.Book(Make.Sheet().Name(new string('A', 32))).Get;

            var result = BookValidator.Sheets(book);

            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public void Sheets_TwoInvalidSheets_TwoErrors()
        {
            var book = Make
                .Book(
                    Make.Sheet().Name(new string('A', 32)),
                    Make.Sheet().Name(new string('B', 32)))
                .Get;

            var result = BookValidator.Sheets(book);

            result.ErrorsUnsafe().Should().HaveCount(2);
        }

        [Fact]
        public void Validate_TwoNonUniqueInvalidSheetNames_ErrorsAreHarvested()
        {
            var invalidName = new string('A', 32);
            var book = Make
                .Book(
                    Make.Sheet().Name(invalidName),
                    Make.Sheet().Name(invalidName))
                .Get;

            var result = BookValidator.Validate(book);

            result
                .ErrorsUnsafe()
                .Should()
                .BeEquivalentTo(F.List<Error>(
                    Errors.Book.SheetNameIsNotUnique(invalidName),
                    Errors.Sheet.NameIsInvalid(invalidName),
                    Errors.Sheet.NameIsInvalid(invalidName)));
        }
    }
}