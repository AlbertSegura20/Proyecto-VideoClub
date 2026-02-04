using Microsoft.AspNetCore.Mvc;
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
        return View(_context.Articulos.ToList());
    }




    [HttpPost]
    public IActionResult Create(Articulos articulos)
    {
        _context.Articulos.Add(articulos);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }

}
