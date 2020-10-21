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
  
  public class CopiesController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CopiesController (UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userCopies = _db.Copies.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userCopies);
    }

    public ActionResult Create()
    {
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "BookName");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Copy copy, int BookId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //existential operator - if its not null, use the .Value
      var currentUser = await _userManager.FindByIdAsync(userId);
      copy.User = currentUser;
      _db.Copies.Add(copy);
      if (BookId != 0)
      {
        _db.BookCopy.Add (new BookCopy() { BookId = BookId, CopyId = copy.CopyId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Details(int id)
    {
      var thisCopy = _db.Copies
      .Include(copy => copy.Books)
      .ThenInclude(join => join.Book)
      .FirstOrDefault(copy => copy.CopyId == id);
      return View(thisCopy);
    }
    
    public ActionResult Edit(int id)
    {
      var thisCopy = _db.Copies.FirstOrDefault(Copies => Copies.CopyId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "BookName");
      return View(thisCopy);
    }
  

    [HttpPost]
    public ActionResult Edit(Copy copy, int BookId)
    {
      if (BookId != 0)
      {
        _db.BookCopy.Add(new BookCopy() {BookId = BookId, CopyId = copy.CopyId });
      }
      _db.Entry(copy).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    [HttpPost]
    public ActionResult AddBook(Copy copy, int BookId)
    {
      if (BookId != 0)
      {
        _db.BookCopy.Add(new BookCopy() {BookId = BookId, CopyId = copy.CopyId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult AddBook(int id)
    {
      var thisCopy = _db.Copies.FirstOrDefault(Copies => Copies.CopyId == id);
      ViewBag.BookId = new SelectList(_db.Books, "BookId", "BookName");
      return View(thisCopy);
    }
    
    public ActionResult Delete(int id)
    {
      var thisCopy = _db.Copies.FirstOrDefault(Copies => Copies.CopyId == id);
      _db.Copies.Remove(thisCopy);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisCopy = _db.Copies.FirstOrDefault(Copies => Copies.CopyId == id);
      _db.Copies.Remove(thisCopy);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteBook(int joinId)
    {
      var joinEntry = _db.BookCopy.FirstOrDefault(entry => entry.BookCopyId == joinId);
      _db.BookCopy.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult Index(string Search)
    {
      List<Copy> model = _db.Copies.Include(copies => copies.Books).Where(x => x.CopyName.Contains(Search)).ToList();
      List<Copy> SortedList = model.OrderBy(o => o.CopyName).ToList();
      return View("Index", SortedList);
    }
  
  }
}