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
  
  public class GenresController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public GenresController (UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userGenres = _db.Genres.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userGenres);
    }

    public ActionResult Create()
    {
      ViewBag.AuthorsId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Genre genre, int AuthorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //existential operator - if its not null, use the .Value
      var currentUser = await _userManager.FindByIdAsync(userId);
      genre.User = currentUser;
      _db.Genres.Add(genre);
      if (AuthorId != 0)
      {
        _db.AuthorGenre.Add (new AuthorGenre() { AuthorId = AuthorId, GenreId = genre.GenreId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Details(int id)
    {
      var thisGenre = _db.Genres
      .Include(genre => genre.Authors)
      .ThenInclude(join => join.Author)
      .Include(genre => genre.Books)
      .ThenInclude(join => join.Book)
      .FirstOrDefault(genre => genre.GenreId == id);
      return View(thisGenre);
    }
    
    public ActionResult Edit(int id)
    {
      var thisGenre = _db.Genres.FirstOrDefault(genres => genres.GenreId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisGenre);
    }
  

    [HttpPost]
    public ActionResult Edit(Genre genre, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.AuthorGenre.Add(new AuthorGenre() {AuthorId = AuthorId, GenreId = genre.GenreId });
      }
      _db.Entry(genre).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAuthor(int id)
    {
      var thisGenre = _db.Genres.FirstOrDefault(genres => genres.GenreId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisGenre);
    }
    
    [HttpPost]
    public ActionResult AddAuthor(Genre genre, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.AuthorGenre.Add(new AuthorGenre() {AuthorId = AuthorId, GenreId = genre.GenreId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult AddBook(int id)
    {
      var thisGenre = _db.Genres.FirstOrDefault(genres => genres.GenreId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "BookName");
      return View(thisGenre);
    }
    
    [HttpPost]
    public ActionResult AddBook(Genre genre, int BookId)
    {
      if (BookId != 0)
      {
        _db.BookGenre.Add(new BookGenre() {BookId = BookId, GenreId = genre.GenreId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisGenre = _db.Genres.FirstOrDefault(genres => genres.GenreId == id);
      _db.Genres.Remove(thisGenre);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisGenre = _db.Genres.FirstOrDefault(genres => genres.GenreId == id);
      _db.Genres.Remove(thisGenre);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteAuthor(int joinId)
    {
      var joinEntry = _db.AuthorGenre.FirstOrDefault(entry => entry.AuthorGenreId == joinId);
      _db.AuthorGenre.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteBook(int genreId, int joinId)
    {
      var joinEntry = _db.BookGenre.FirstOrDefault(entry => entry.BookGenreId == joinId);
      _db.BookGenre.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = genreId});
    }
  
  }
}