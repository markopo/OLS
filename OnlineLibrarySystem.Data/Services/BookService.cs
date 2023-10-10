using OnlineLibrarySystem.Data.Dto;
using OnlineLibrarySystem.Data.Mapper;
using OnlineLibrarySystem.Data.Repositories;

namespace OnlineLibrarySystem.Data.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IEnumerable<BookDto>> FindAll() => 
        (await _bookRepository.FindAll()).Select(BookMapper.Map);

    public async Task<BookDto> Find(int bookId)
    {
        var book = await _bookRepository.Find(bookId);
        return book != null ? BookMapper.Map(book) : null;
    }

    public async Task<IEnumerable<BookDto>> FindByAuthor(string author) => 
        (await _bookRepository.FindByAuthor(author)).Select(BookMapper.Map);          

    public async Task<IEnumerable<BookDto>> FindByBookName(string bookName) => 
        (await _bookRepository.FindByBookName(bookName)).Select(BookMapper.Map);    
   

    public async Task<IEnumerable<BookDto>> FindByPublisher(string publisher) => 
        (await _bookRepository.FindByPublisher(publisher)).Select(BookMapper.Map);

    public async Task<BookDto> Save(BookDto book)
    {
        var bookSaved = await _bookRepository.Save(BookMapper.Map(book));
        return bookSaved != null ? BookMapper.Map(bookSaved) : null;
    }

    public async Task<bool> Delete(int bookId)
    {
        return await _bookRepository.Delete(bookId);
    }
}