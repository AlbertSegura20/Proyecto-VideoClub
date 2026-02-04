using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoClub.Models;

namespace VideoClub.Controllers;

public class ArticulosController : Controller
{
    private readonly AppDbContext _context;

    public ArticulosController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        ViewBag.Elenco = _context.Elenco.Where(e => e.Estado == Elenco.EstadoElenco.Activo).ToList();
        ViewBag.Idiomas = _context.Idiomas.Where(i => i.Estado == Idiomas.EstadoIdioma.Activo).ToList();
        ViewBag.TiposArticulos = _context.TiposArticulos.Where(t => t.Estado == TiposArticulos.EstadoArticulo.Activo).ToList();
        return View(_context.Articulos.Include(a => a.TiposArticulos).Include(a => a.Idioma).ToList());
    }


    [HttpPost]
    public IActionResult Create(Articulos articulos)
    {
        if(ModelState.IsValid)
        {
             _context.Articulos.Add(articulos);
             _context.SaveChanges();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPut]
    public IActionResult Update([FromBody] Articulos articulo)
    {
        if (articulo == null) return BadRequest();

        var existing = _context.Articulos.Find(articulo.Id);
        if (existing == null) return NotFound();

        existing.Titulo = articulo.Titulo;
        existing.TiposArticulosId = articulo.TiposArticulosId;
        existing.IdiomaId = articulo.IdiomaId;
        existing.RentaPorDia = articulo.RentaPorDia;
        existing.DiasDeRenta = articulo.DiasDeRenta;
        existing.MontoEntregaTardia = articulo.MontoEntregaTardia;
        existing.Estado = articulo.Estado;

        _context.Articulos.Update(existing);
        _context.SaveChanges();

        return Ok(existing);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var data = _context.Articulos.Find(id);
        if (data == null) return NotFound();

        _context.Articulos.Remove(data);
        _context.SaveChanges();
        return Ok(data);
    }

}
