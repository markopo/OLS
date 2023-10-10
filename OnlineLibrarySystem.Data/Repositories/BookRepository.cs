using Microsoft.EntityFrameworkCore;
using OnlineLibrarySystem.Data.Dto;
using OnlineLibrarySystem.Data.Models;

namespace OnlineLibrarySystem.Data.Repositories;

public class BookRepository : IBookRepository
{
    private readonly OnlineLibrarySystemDbContext _context;

    public BookRepository(OnlineLibrarySystemDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Book>> FindAll() => (await _context.Books.ToListAsync()).AsEnumerable();

    public async Task<Book> Find(int bookId) => await _context.Books.FirstOrDefaultAsync(b => b.BookId == bookId);

    public async Task<IEnumerable<Book>> FindByAuthor(string author) =>
        (await _context.Books.Where(b => b.Author.Contains(author)).ToListAsync()).AsEnumerable();

    public async Task<IEnumerable<Book>> FindByBookName(string bookName) => (await _context.Books
        .Where(b => b.BookName.Contains(bookName)).ToListAsync()).AsEnumerable();
  

    public async Task<IEnumerable<Book>> FindByPublisher(string publisher) => (await _context.Books
        .Where(b => b.Publisher.Contains(publisher)).ToListAsync()).AsEnumerable();

    public async Task<Book> Save(Book book)
    {
        if (book.BookId == 0)
        { 
          _context.Books.Add(book);    
        }
        else
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.BookId == book.BookId);
            if (existingBook != null)
            {
                existingBook.BookName = book.BookName;
                existingBook.Author = book.Author;
                existingBook.Publisher = book.Publisher;
            }
        }
   
        var numEntries = await _context.SaveChangesAsync();
        return numEntries == 1 ? book : null;
    }

    public async Task<bool> Delete(int bookId)
    {
        var numEntries = 0;
        var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == bookId);
        if (book == null) return false;
        
        _context.Books.Remove(book);
        numEntries = await _context.SaveChangesAsync();
        return numEntries == 1;
    }
}