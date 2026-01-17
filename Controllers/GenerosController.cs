using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VideoClub.Models;

namespace VideoClub.Controllers;

public class GenerosController : Controller
{

    private readonly AppDbContext _context;

    public GenerosController(AppDbContext context) {
        _context = context;
    }
  
    public IActionResult Index()
    {
        var data = _context.Generos.ToList();
        return View(data);
    }

    [HttpGet]
    public IActionResult GetAll() {
        var data = _context.Generos.ToList();
        return Json(data);
    }

    [HttpPost]
    public IActionResult Create(Generos genero) {

        if(!ModelState.IsValid) {
            return View(genero);
        }

        _context.Generos.Add(genero);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpPut]
    public IActionResult Update( [FromBody] Generos genero)
    {
        if (genero == null)
        {
            return BadRequest();
        }

        var existing = _context.Generos.Find(genero.Id);
        if(existing == null)
        {
            return NotFound();
        }

        existing.Descripcion = genero.Descripcion;
        existing.Estado = genero.Estado;
        
        _context.Generos.Update(existing);
        _context.SaveChanges();

        return Ok(existing);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id) {
         var data = _context.Generos.Find(id);
            if(data == null)
             {
               return NotFound();
             }
                _context.Generos.Remove(data);
                _context.SaveChanges();
         return Ok(data);
    }

}
