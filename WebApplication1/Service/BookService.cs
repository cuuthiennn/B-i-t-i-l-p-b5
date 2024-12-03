using WebApplication1.Models;

namespace WebApplication1.Service;

public class BookService : IBookService
{
    private readonly List<Book> _books;

    // Tiêm List<Book> vào BookService
    public BookService(List<Book> books)
    {
        _books = books;
    }

    public IEnumerable<Book> GetAllBooks()
    {
        return _books;
    }

    public Book GetBookById(int id)
    {
        return _books.FirstOrDefault(b => b.Id == id);
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public void UpdateBook(Book book)
    {
        var existingBook = GetBookById(book.Id);
        if (existingBook != null)
        {
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Description = book.Description;
            existingBook.Price = book.Price;
            existingBook.Image = book.Image;
        }
    }

    public void DeleteBook(int id)
    {
        var book = GetBookById(id);
        if (book != null)
        {
            _books.Remove(book);
        }
    }
}
