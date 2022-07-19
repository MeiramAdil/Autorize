using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
  public class WorksController : Controller
  {
    private readonly ApplicationContext _context;

    public WorksController(ApplicationContext context)
    {
      _context = context;
    }

    // GET: Works
    public async Task<IActionResult> Index()
    {
      return _context.Works != null ?
                  View(await _context.Works.ToListAsync()) :
                  Problem("Пока нету пройзведени");
    }

    // GET: Works/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null || _context.Works == null)
      {
        return NotFound();
      }

      var work = await _context.Works
          .FirstOrDefaultAsync(m => m.Id == id);
      if (work == null)
      {
        return NotFound();
      }

      return View(work);
    }
    
    public async Task<IActionResult> GetUsersWorks(int? id)
    {
      return _context.Works.Where(x => x.UserId == id) != null ?
        View(await _context.Works.Where(x => x.UserId == id).ToListAsync()) :
        Ok("У вас пока нету свойх пройзведени.");
    }
    // GET: Works/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Works/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Genre,PublicationDate,Rating,UserId")] Work work)
    {
      if (ModelState.IsValid)
      {
        _context.Add(work);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(work);
    }

    // GET: Works/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null || _context.Works == null)
      {
        return NotFound();
      }

      var work = await _context.Works.FindAsync(id);
      if (work == null)
      {
        return NotFound();
      }
      return View(work);
    }

    // POST: Works/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,PublicationDate,Rating,UserId")] Work work)
    {
      if (id != work.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(work);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!WorkExists(work.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(work);
    }

    // GET: Works/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
      if (id == null || _context.Works == null)
      {
        return NotFound();
      }

      var work = await _context.Works
          .FirstOrDefaultAsync(m => m.Id == id);
      if (work == null)
      {
        return NotFound();
      }

      return View(work);
    }

    // POST: Works/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      if (_context.Works == null)
      {
        return Problem("Entity set 'ApplicationContext.Works'  is null.");
      }
      var work = await _context.Works.FindAsync(id);
      if (work != null)
      {
        _context.Works.Remove(work);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool WorkExists(int id)
    {
      return (_context.Works?.Any(e => e.Id == id)).GetValueOrDefault();
    }
  }
}
