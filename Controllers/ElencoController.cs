using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VideoClub.Models;

namespace VideoClub.Controllers;

public class ElencoController : Controller
{
     private readonly AppDbContext _context;

    public ElencoController(AppDbContext context)
    {
        _context = context;
    }
  
    public IActionResult Index()    
    {
        
        return View(_context.Elenco.ToList());
    }

     [HttpPost]
    public IActionResult Create(Elenco elenco)
    {
        if(!ModelState.IsValid)
        {
            return View(elenco);
        }
        _context.Elenco.Add(elenco);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpPut]
    public IActionResult Update([FromBody] Elenco elenco)
    {
        var elencoDb = _context.Elenco.Find(elenco.Id);

        if(elencoDb == null)
        {
            return NotFound();
        }

        elencoDb.Nombre = elenco.Nombre;
        elencoDb.Estado = elenco.Estado;
        
        _context.Elenco.Update(elencoDb);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var elenco = _context.Elenco.Find(id);
        if(elenco == null)
        {
            return NotFound();
        }
        _context.Elenco.Remove(elenco);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

}
