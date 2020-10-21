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
using System;

namespace Library.Controllers
{
  
  public class PatronsController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public PatronsController (UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userPatrons = _db.Patrons.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userPatrons);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Patron patron)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; //existential operator - if its not null, use the .Value
      var currentUser = await _userManager.FindByIdAsync(userId);
      patron.User = currentUser;
      _db.Patrons.Add(patron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    public ActionResult Details(int id)
    {
      var thisPatron = _db.Patrons
      .Include(patron => patron.Copies)
      .ThenInclude(join => join.Copy)
      .FirstOrDefault(patron => patron.PatronId == id);
      return View(thisPatron);
    }
    
    public ActionResult Edit(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "Name");
      return View(thisPatron);
    }
  

    [HttpPost]
    public ActionResult Edit(Patron patron, int CopyId)
    {
      if (CopyId != 0)
      {
        _db.CopyPatron.Add(new CopyPatron() {CopyId = CopyId, PatronId = patron.PatronId });
      }
      _db.Entry(patron).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddCopy(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      ViewBag.CopyId = new SelectList(_db.Copies, "CopyId", "CopyName");
      return View(thisPatron);
    }
    
    [HttpPost]
    public ActionResult AddCopy(Patron patron, int CopyId, DateTime dateTime)
    {
      if (CopyId != 0)
      {
        _db.CopyPatron.Add(new CopyPatron() {CopyId = CopyId, PatronId = patron.PatronId, CopyCheckoutDate = dateTime});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    
    public ActionResult Delete(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      _db.Patrons.Remove(thisPatron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(patrons => patrons.PatronId == id);
      _db.Patrons.Remove(thisPatron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteCopy(int joinId)
    {
      var joinEntry = _db.CopyPatron.FirstOrDefault(entry => entry.CopyPatronId == joinId);
      _db.CopyPatron.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  
  }
}