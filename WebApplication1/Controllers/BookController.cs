using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Service;

namespace WebApplication1.Controllers;

public class BookController : Controller
{
    private readonly IBookService _bookService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public BookController(IBookService bookService, IWebHostEnvironment webHostEnvironment)
    {
        _bookService = bookService;
        _webHostEnvironment = webHostEnvironment;
    }

    // Trang chính để xem danh sách sách
    public IActionResult Index()
    {
        var books = _bookService.GetAllBooks();
        return View(books);
    }

    // Trang tạo sách mới
    public IActionResult Create()
    {
        return View();
    }

    // Xử lý việc tạo sách mới
    [HttpPost]
    public async Task<IActionResult> Create(Book book, IFormFile image)
    {
        if (ModelState.IsValid)
        {
            if (image != null && image.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                // Kiểm tra thư mục images có tồn tại không
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, image.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                // Lưu đường dẫn hình ảnh vào đối tượng book
                book.Image = $"/images/{image.FileName}";
            }

            _bookService.AddBook(book);
            return RedirectToAction("Index", "Book");
        }

        return View(book);
    }


    // Trang chỉnh sửa sách
    public IActionResult Edit(int id)
    {
        var book = _bookService.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // Xử lý việc sửa sách
    [HttpPost]
    public async Task<IActionResult> Edit(Book book, IFormFile image)
    {
        if (ModelState.IsValid)
        {
            if (image != null && image.Length > 0)
            {
                // Tạo thư mục images trong wwwroot nếu chưa tồn tại
                var imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                // Lưu hình ảnh mới vào thư mục images
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(imageDirectory, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                // Cập nhật đường dẫn hình ảnh vào đối tượng book
                book.Image = $"/images/{fileName}";
                _bookService.UpdateBook(book);
            }
            
            return RedirectToAction("Index", "Book");
        }

        return View(book);
    }



    // Xử lý xóa sách
    public IActionResult Delete(int id)
    {
        var book = _bookService.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }
        _bookService.DeleteBook(id);
        return RedirectToAction("Index", "Book");
    }

    // Chi tiết sách
    public IActionResult Details(int id)
    {
        var book = _bookService.GetBookById(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }
}

