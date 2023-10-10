using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrarySystem.Data.Dto;
using OnlineLibrarySystem.Data.Services;

namespace OnlineLibrarySystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;

    public BooksController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IEnumerable<BookDto>> Get() => 
        await _bookService.FindAll();

    [HttpGet("author/{author}")]
    public async Task<IEnumerable<BookDto>> GetByAuthor(string author) => 
        await _bookService.FindByAuthor(author);


    [HttpGet("bookname/{bookName}")]
    public async Task<IEnumerable<BookDto>> GetByBookName(string bookName) => 
        await _bookService.FindByBookName(bookName);

    [HttpGet("publisher/{publisher}")]
    public async Task<IEnumerable<BookDto>> GetByPublisher(string publisher) => 
        await _bookService.FindByPublisher(publisher);

    [HttpPost]
    public async Task<BookDto> Post([FromBody] BookDto bookDto)
    {
       return await _bookService.Save(bookDto);
    }

    [HttpPut("{id}")]
    public async Task<BookDto> Put(int id, [FromBody] BookDto bookDto)
    {
        var book = await _bookService.Find(id);

        if (book == null) return null;
        
        book = book with
        {
            BookName = bookDto.BookName,
            Author = bookDto.Author,
            Publisher = bookDto.Publisher
        };
        return await _bookService.Save(book);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<bool> Delete(int id)
    {
        return await _bookService.Delete(id);
    }




}