using OnlineLibrarySystem.Data.Dto;
using OnlineLibrarySystem.Data.Models;

namespace OnlineLibrarySystem.Data.Mapper;

public static class BookMapper
{
    public static BookDto Map(Book book) => book != null ? 
        new BookDto(book.BookId, book.BookName, book.Author, book.Publisher)
        : null;

    public static Book Map(BookDto book) => book != null
        ? new Book
        {
           BookId = book.BookId,
           BookName = book.BookName,
           Author = book.Author,
           Publisher = book.Publisher
        }
        : null;


}