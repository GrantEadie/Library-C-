using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
  
  public class BooksController : Controller 
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public BooksController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userBooks = _db.Books.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userBooks);
    }
    
    public ActionResult Create()
    {
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "CopyName");
      ViewBag.GenreId = new SelectList(_db.Genres, "GenreId", "GenreName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Book book, int AuthorId, int CopyId, int GenreId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      book.User = currentUser;
      _db.Books.Add(book);
      if (AuthorId != 0)
      {
        _db.AuthorBook.Add(new AuthorBook()
        { AuthorId = AuthorId, BookId = book.BookId });
      }
      if (GenreId != 0)
      {
        _db.BookGenre.Add(new BookGenre()
        { GenreId = GenreId, BookId = book.BookId });
      }
      if (CopyId != 0)
      {
        _db.BookCopy.Add(new BookCopy() 
        { CopyId = CopyId, BookId = book.BookId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisBook = _db.Books
          .Include(book => book.Authors)
          .ThenInclude(join => join.Author)
          .Include(book => book.Genres)
          .ThenInclude(join => join.Genre)
          .Include(book => book.Copies)
          .ThenInclude(join => join.Copy)
          .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }

    public ActionResult Edit(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "Name");
      ViewBag.GenreId = new SelectList(_db.Genres, "GenreId", "Name");
      ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "Name");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult Edit(Book book, int AuthorId, int GenreId, int CopyId)
    {
      if (AuthorId != 0)
      {
        _db.AuthorBook.Add(new AuthorBook(){ AuthorId = AuthorId, BookId = book.BookId});
      }
      if (GenreId != 0)
      {
        _db.BookGenre.Add(new BookGenre() 
        { GenreId = GenreId, BookId = book.BookId});
      }
      if (CopyId != 0)
      {
        _db.BookCopy.Add(new BookCopy()
        { CopyId = CopyId, BookId = book.BookId});
      }
      _db.Entry(book).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAuthor(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddAuthor(Book book, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.AuthorBook.Add(new AuthorBook() { AuthorId = AuthorId, BookId = book.BookId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


    public ActionResult AddGenre(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.GenreId = new SelectList(_db.Genres, "GenreId", "GenreName");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddGenre(Book book, int GenreId)
    {
      if (GenreId != 0)
      {
        _db.BookGenre.Add(new BookGenre() { GenreId = GenreId, BookId = book.BookId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCopy(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "CopyName");
      return View(thisBook);
    }

    [HttpPost]
    public ActionResult AddCopy(Book book, int CopyId)
    {
      if (CopyId != 0)
      {
        _db.BookCopy.Add(new BookCopy() { CopyId = CopyId, BookId = book.BookId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAuthor(int joinId)
    {
      var joinEntry = _db.AuthorBook.FirstOrDefault(entry => entry.AuthorBookId == joinId);
      _db.AuthorBook.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCopy(int joinId)
    {
      var joinEntry = _db.BookCopy.FirstOrDefault(entry => entry.BookCopyId == joinId);
      _db.BookCopy.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteGenre(int joinId)
    {
      var joinEntry = _db.BookGenre.FirstOrDefault(entry => entry.BookGenreId == joinId);
      _db.BookGenre.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}