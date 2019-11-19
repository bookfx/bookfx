﻿namespace BookFx
{
    using System.Collections.Generic;
    using System.Linq;
    using BookFx.Cores;
    using BookFx.Functional;
    using JetBrains.Annotations;
    using static BookFx.Functional.F;

    /// <summary>
    /// This is the entry point of BookFx.
    /// </summary>
    [PublicAPI]
    public static partial class Make
    {
        /// <summary>
        /// Creates an empty book.
        /// </summary>
        [Pure]
        public static Book Book() => BookFx.Book.Empty;

        /// <summary>
        /// Creates a book with <paramref name="sheets"/>.
        /// </summary>
        /// <param name="sheets">IEnumerable of creating book sheets.</param>
        [Pure]
        public static Book Book(IEnumerable<Sheet> sheets) =>
            BookCore.Create(sheets.Map(x => x.Get));

        /// <summary>
        /// Creates a book with <paramref name="sheets"/>.
        /// </summary>
        /// <param name="sheets">Arrays of creating book sheets.</param>
        [Pure]
        public static Book Book(params Sheet[] sheets) => Book(sheets.AsEnumerable());

        /// <summary>
        /// Creates an empty unnamed sheet.
        /// </summary>
        [Pure]
        public static Sheet Sheet() => BookFx.Sheet.Empty;

        /// <summary>
        /// Creates an empty named sheet.
        /// </summary>
        /// <param name="name">
        /// A name of creating sheet.
        /// Name length should be from 1 to 31
        /// and name should be free of following characters: ':', '\', '/', '?', '*', '[' or ']'.
        /// </param>
        [Pure]
        public static Sheet Sheet(string name) => SheetCore.Create().With(name: Some(name));

        /// <summary>
        /// Creates a named sheet with box.
        /// </summary>
        /// <param name="name">
        /// A name of creating sheet.
        /// Name length should be from 1 to 31
        /// and name should be free of following characters: ':', '\', '/', '?', '*', '[' or ']'.
        /// </param>
        /// <param name="box">A root box of creating sheet.</param>
        [Pure]
        public static Sheet Sheet(string name, Box box) =>
            SheetCore.Create().With(name: Some(name), box: box.Get);

        /// <summary>
        /// Creates an unnamed sheet with box.
        /// </summary>
        /// <param name="box">A root box of creating sheet.</param>
        [Pure]
        public static Sheet Sheet(Box box) => SheetCore.Create().With(box: box.Get);

        /// <summary>
        /// Creates an unnamed sheet as a copy of only sheet of <paramref name="protoBook"/>.
        /// </summary>
        /// <param name="protoBook">Bytes of book package which contains the copying sheet.</param>
        [Pure]
        public static Sheet Sheet(byte[] protoBook) =>
            SheetCore.Create().With(protoBook: Some(protoBook));

        /// <summary>
        /// Creates an unnamed sheet as a copy of <paramref name="protoName"/> sheet of <paramref name="protoBook"/>.
        /// </summary>
        /// <param name="protoBook">Bytes of book package which contains the copying sheet.</param>
        /// <param name="protoName">A name of copying sheet.</param>
        [Pure]
        public static Sheet Sheet(byte[] protoBook, string protoName) =>
            SheetCore.Create().With(protoBook: Some(protoBook), protoName: Some(protoName));

        /// <summary>
        /// Creates a named sheet as a copy of <paramref name="protoName"/> sheet of <paramref name="protoBook"/>.
        /// </summary>
        /// <param name="name">
        /// A name of creating sheet.
        /// Name length should be from 1 to 31
        /// and name should be free of following characters: ':', '\', '/', '?', '*', '[' or ']'.
        /// </param>
        /// <param name="protoBook">Bytes of book package which contains the copying sheet.</param>
        /// <param name="protoName">A name of copying sheet.</param>
        [Pure]
        public static Sheet Sheet(string name, byte[] protoBook, string protoName) =>
            SheetCore.Create().With(name: Some(name), protoBook: Some(protoBook), protoName: Some(protoName));

        /// <summary>
        /// Creates an empty <see cref="RowBox"/>.
        /// </summary>
        [Pure]
        public static RowBox Row() => RowBox.Empty;

        /// <summary>
        /// Creates a <see cref="RowBox"/> with children.
        /// </summary>
        /// <param name="children">IEnumerable of children <see cref="Box"/>'es.</param>
        [Pure]
        public static RowBox Row(IEnumerable<Box> children) => RowBox.Empty.Add(children);

        /// <summary>
        /// Creates a <see cref="RowBox"/> with children.
        /// </summary>
        /// <param name="child">First child.</param>
        /// <param name="others">Other children.</param>
        [Pure]
        public static RowBox Row(Box child, params Box[] others) =>
            Row(others.Prepend(child));

        /// <summary>
        /// Creates an empty <see cref="ColBox"/>.
        /// </summary>
        [Pure]
        public static ColBox Col() => ColBox.Empty;

        /// <summary>
        /// Creates a <see cref="ColBox"/> with children.
        /// </summary>
        /// <param name="children">IEnumerable of children <see cref="Box"/>'es.</param>
        [Pure]
        public static ColBox Col(IEnumerable<Box> children) =>
            ColBox.Empty.Add(children);

        /// <summary>
        /// Creates a <see cref="ColBox"/> with children.
        /// </summary>
        /// <param name="child">First child.</param>
        /// <param name="others">Other children.</param>
        [Pure]
        public static ColBox Col(Box child, params Box[] others) =>
            Col(others.Prepend(child));

        /// <summary>
        /// Creates an empty <see cref="StackBox"/>.
        /// </summary>
        [Pure]
        public static StackBox Stack() => StackBox.Empty;

        /// <summary>
        /// Creates a <see cref="StackBox"/> with children.
        /// </summary>
        /// <param name="children">IEnumerable of children <see cref="Box"/>'es.</param>
        [Pure]
        public static StackBox Stack(IEnumerable<Box> children) => StackBox.Empty.Add(children);

        /// <summary>
        /// Creates a <see cref="StackBox"/> with children.
        /// </summary>
        /// <param name="child">First child.</param>
        /// <param name="others">Other children.</param>
        [Pure]
        public static StackBox Stack(Box child, params Box[] others) =>
            Stack(others.Prepend(child));

        /// <summary>
        /// Creates a <see cref="ProtoBox"/>.
        /// </summary>
        /// <param name="book">Bytes of book with box prototype.</param>
        /// <param name="reference">Reference to a prototype in the <paramref name="book"/>.</param>
        [Pure]
        public static ProtoBox Proto(byte[] book, Reference reference) =>
            BoxCore.Create(BoxType.Proto).With(proto: ProtoCore.Create(book, reference));

        /// <summary>
        /// Creates an empty <see cref="BoxStyle"/>.
        /// </summary>
        [Pure]
        public static BoxStyle Style() => BoxStyle.Empty;

        /// <summary>
        /// Mixes two styles.
        /// </summary>
        /// <param name="a">The first style.</param>
        /// <param name="b">The second style.</param>
        [Pure]
        public static BoxStyle Style(BoxStyle a, BoxStyle b) => BoxStyleCore.Mix(a.Get, b.Get);

        /// <summary>
        /// Mixes many styles.
        /// </summary>
        /// <param name="styles">IEnumerable of styles.</param>
        [Pure]
        public static BoxStyle Style(IEnumerable<BoxStyle> styles) =>
            styles.Aggregate(BoxStyle.Empty, Style);

        /// <summary>
        /// Mixes many styles.
        /// </summary>
        /// <param name="a">The first style.</param>
        /// <param name="b">The second style.</param>
        /// <param name="others">Other styles.</param>
        [Pure]
        public static BoxStyle Style(
            BoxStyle a,
            BoxStyle b,
            params BoxStyle[] others) =>
            Style(others.Prepend(b).Prepend(a));

        /// <summary>
        /// Creates an empty <see cref="BoxBorder"/>.
        /// </summary>
        [Pure]
        public static BoxBorder Border() => BoxBorder.Empty;

        /// <summary>
        /// Creates a <see cref="BoxBorder"/> with restriction to a <paramref name="part"/>.
        /// </summary>
        /// <param name="part">The part of border.</param>
        [Pure]
        public static BoxBorder Border(BorderPart part) => BoxBorder.Empty.Restrict(part);

        /// <summary>
        /// Creates a <see cref="BoxBorder"/> a border style.
        /// </summary>
        /// <param name="style">The border style.</param>
        [Pure]
        public static BoxBorder Border(BorderStyle style) => BoxBorder.Empty.Style(style);

        /// <summary>
        /// Creates a <see cref="BoxBorder"/> with a <paramref name="style"/> applied to a <paramref name="part"/>.
        /// </summary>
        /// <param name="part">The part of border.</param>
        /// <param name="style">The border style.</param>
        [Pure]
        public static BoxBorder Border(BorderPart part, BorderStyle style) =>
            BoxBorder.Empty.Restrict(part).Style(style);
    }
}