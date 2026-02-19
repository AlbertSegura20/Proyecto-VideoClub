using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoClub.Models;

namespace VideoClub.Controllers;


[Authorize]
public class TiposArticulosController : Controller
{
    private readonly AppDbContext _context;

    public TiposArticulosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var data = _context.TiposArticulos.ToList();
      return View(data);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var data = _context.TiposArticulos.ToList();
        return Json(data);
    }

    [HttpPost]
    public IActionResult Create(TiposArticulos tipoArticulo)
    {
        if(!ModelState.IsValid)
        {
            return View(tipoArticulo);
        }

        tipoArticulo.Estado = (TiposArticulos.EstadoArticulo)tipoArticulo.Estado;
        tipoArticulo.Tipo = (TiposArticulos.TipoArticulo)tipoArticulo.Tipo;
        Console.WriteLine(tipoArticulo.Estado);
        Console.WriteLine(tipoArticulo.Tipo);
        _context.TiposArticulos.Add(tipoArticulo);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpPut]
    public IActionResult Update( [FromBody] TiposArticulos tipoArticulo)
    {
        if (tipoArticulo == null)
        {
            return BadRequest();
        }

        var existing = _context.TiposArticulos.Find(tipoArticulo.Id);
        if(existing == null)
        {
            return NotFound();
        }

        existing.Estado = tipoArticulo.Estado;
        existing.Tipo = tipoArticulo.Tipo;
        
        _context.TiposArticulos.Update(existing);
        _context.SaveChanges();

        return Ok(existing);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var data = _context.TiposArticulos.Find(id);
        if(data == null)
        {
            return NotFound();
        }
        _context.TiposArticulos.Remove(data);
        _context.SaveChanges();
        return Ok(data);
    }
}
