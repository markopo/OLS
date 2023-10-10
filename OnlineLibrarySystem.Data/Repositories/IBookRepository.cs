using OnlineLibrarySystem.Data.Dto;
using OnlineLibrarySystem.Data.Models;

namespace OnlineLibrarySystem.Data.Repositories;

public interface IBookRepository
{
    Task<IEnumerable<Book>>FindAll();

    Task<Book> Find(int bookId);
    
    Task<IEnumerable<Book>> FindByAuthor(string author);
    
    Task<IEnumerable<Book>> FindByBookName(string bookName);
    
    Task<IEnumerable<Book>> FindByPublisher(string publisher);

    Task<Book> Save(Book book);

    Task<bool> Delete(int bookId);
}