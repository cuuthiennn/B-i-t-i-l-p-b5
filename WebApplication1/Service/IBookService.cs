using WebApplication1.Models;

namespace WebApplication1.Service;

using System.Collections.Generic;
using System.Linq;

public interface IBookService
{
    IEnumerable<Book> GetAllBooks();
    Book GetBookById(int id);
    void AddBook(Book book);
    void UpdateBook(Book book);
    void DeleteBook(int id);
}

