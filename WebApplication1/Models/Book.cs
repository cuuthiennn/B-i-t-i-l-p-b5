namespace WebApplication1.Models;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }  // Nullable string
    public string? Author { get; set; }  // Nullable string
    public string? Description { get; set; }  // Nullable string
    public string? Image { get; set; }  // Nullable string
    public decimal Price { get; set; }
}

