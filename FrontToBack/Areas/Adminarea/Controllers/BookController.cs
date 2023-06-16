using FrontToBack.DAL;
using FrontToBack.Models.Practice;
using FrontToBack.ViewModels.AdminVM.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack.Areas.Adminarea.Controllers
{
    [Area(nameof(Adminarea))]
    public class BookController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BookController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var data = _appDbContext.Books
            .Include(b=>b.BookGenres)
            .ThenInclude(b=>b.Genre)
            .ToList();
            return View(data);
        }
        public IActionResult Create()
        {
            ViewBag.Genre = new SelectList(_appDbContext.Genres.ToList(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
         public IActionResult Create(BookCreateVM bookCreateVM)
        {
            ViewBag.Genre = new SelectList(_appDbContext.Genres.ToList(), "Id", "Name");
            Book book = new();
            book.Name= bookCreateVM.Name;
            List<BookGenre> bookGenres = new List<BookGenre>();
            foreach (var item in bookCreateVM.GenreIds)
            {
                BookGenre bookGenre = new();
                bookGenre.BookId = book.Id;
                bookGenre.GenreId = item;
                bookGenres.Add(bookGenre);
            }
            book.BookGenres = bookGenres;
            _appDbContext.Books.Add(book);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
