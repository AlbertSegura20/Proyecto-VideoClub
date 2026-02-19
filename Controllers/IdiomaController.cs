using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using VideoClub.Models;

namespace VideoClub.Controllers;

[Authorize]
public class IdiomaController : Controller
{   
    private readonly AppDbContext _context;

    public IdiomaController(AppDbContext context)
    {
        _context = context;
    }
  
    public IActionResult Index()
    {
        return View(_context.Idiomas.ToList());
    }

    [HttpPost]
    public IActionResult Create(Idiomas idiomas)
    {
        _context.Idiomas.Add(idiomas);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var idioma = _context.Idiomas.Find(id);
        return View(idioma);
    }

 
    [HttpPut]
    public IActionResult Update( [FromBody] Idiomas idioma)
    {
        if (idioma == null)
        {
            return BadRequest();
        }

        var existing = _context.Idiomas.Find(idioma.Id);
        if(existing == null)
        {
            return NotFound();
        }

        existing.Descripcion = idioma.Descripcion;
        existing.Estado = idioma.Estado;
        
        _context.Idiomas.Update(existing);
        _context.SaveChanges();

        return Ok(existing);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var idioma = _context.Idiomas.Find(id);
        if (idioma == null)
        {
            return NotFound();
        }
        _context.Idiomas.Remove(idioma);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

}
