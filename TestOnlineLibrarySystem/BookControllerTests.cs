using Moq;
using OnlineLibrarySystem.Controllers;
using OnlineLibrarySystem.Data.Dto;
using OnlineLibrarySystem.Data.Services;

namespace TestOnlineLibrarySystem;

public class BookControllerTests
{
    [Fact]
    public async Task TestGetSuccess()
    {
        var mockBookService = new Mock<IBookService>();
        
        mockBookService.Setup(m => m.FindAll())
            .ReturnsAsync(new List<BookDto>().AsEnumerable());
        
        var controller = new BooksController(mockBookService.Object);

        var result = await controller.Get();
        
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task TestGetAuthor()
    {
        var mockBookService = new Mock<IBookService>();
        
        mockBookService.Setup(m => m.FindByAuthor(It.IsAny<string>()))
            .ReturnsAsync(new List<BookDto>().AsEnumerable());
        
        var controller = new BooksController(mockBookService.Object);

        var result = await controller.GetByAuthor("tolkien");
        
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task TestGetByAuthor()
    {
        var mockBookService = new Mock<IBookService>();
        
        mockBookService.Setup(m => m.FindByAuthor(It.IsAny<string>()))
            .ReturnsAsync(new List<BookDto>().AsEnumerable());
        
        var controller = new BooksController(mockBookService.Object);

        var result = await controller.GetByAuthor("tolkien");
        
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task TestGetByBookName()
    {
        var mockBookService = new Mock<IBookService>();
        
        mockBookService.Setup(m => m.FindByBookName(It.IsAny<string>()))
            .ReturnsAsync(new List<BookDto>().AsEnumerable());
        
        var controller = new BooksController(mockBookService.Object);

        var result = await controller.GetByAuthor("tolkien");
        
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task TestGetByPublisher()
    {
        var mockBookService = new Mock<IBookService>();
        
        mockBookService.Setup(m => m.FindByPublisher(It.IsAny<string>()))
            .ReturnsAsync(new List<BookDto>().AsEnumerable());
        
        var controller = new BooksController(mockBookService.Object);

        var result = await controller.GetByPublisher("tolkien");
        
        Assert.NotNull(result);
    }

    [Fact]
    public async Task TestPost()
    {
        var mockBookService = new Mock<IBookService>();
        
        mockBookService.Setup(m => m.Save(It.IsAny<BookDto>()))
            .ReturnsAsync(new BookDto(1, "d", "d", "d"));
        
        var controller = new BooksController(mockBookService.Object);

        var result = await controller.Post(new BookDto(0, "d", "d", "d"));
        
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task TestPut()
    {
        var mockBookService = new Mock<IBookService>();
        
        mockBookService.Setup(m => m.Find(It.IsAny<int>()))
            .ReturnsAsync(new BookDto(25, "da", "da", "da"));
        
        mockBookService.Setup(m => m.Save(It.IsAny<BookDto>()))
            .ReturnsAsync(new BookDto(25, "dajm", "dajm", "dajm"));

        
        var controller = new BooksController(mockBookService.Object);

        var result = await controller.Put(25, new BookDto(25, "dajm", "dajm", "dajm"));
        
        Assert.NotNull(result);
        Assert.Equal("dajm", result.Publisher);
        Assert.Equal("dajm", result.Author);
        Assert.Equal("dajm", result.BookName);
    }
    
    [Fact]
    public async Task TestDelete()
    {
        var mockBookService = new Mock<IBookService>();
        
        mockBookService.Setup(m => m.Delete(It.IsAny<int>()))
            .ReturnsAsync(true);
        
        var controller = new BooksController(mockBookService.Object);

        var result = await controller.Delete(12);
        
        Assert.Equal(true, result);
    }

}