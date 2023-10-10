using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineLibrarySystem.Data.Models;

[Table("Books")]
public class Book
{
    [Key]
    public int BookId { get; set; }
    
    public string BookName { get; set; }
    
    public string Author { get; set; }
    
    public string Publisher { get; set; }
}