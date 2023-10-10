using OnlineLibrarySystem.Data.Dto;

namespace OnlineLibrarySystem.Data.Services;

public interface IBookService
{
    Task<IEnumerable<BookDto>> FindAll();
    
    Task<BookDto> Find(int bookId);
    
    Task<IEnumerable<BookDto>> FindByAuthor(string author);
    
    Task<IEnumerable<BookDto>> FindByBookName(string bookName);
    
    Task<IEnumerable<BookDto>> FindByPublisher(string publisher);
    
    Task<BookDto> Save(BookDto book);

    Task<bool> Delete(int bookId);
}