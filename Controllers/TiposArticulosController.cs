using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VideoClub.Models;

namespace VideoClub.Controllers;


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
        _context.TiposArticulos.Add(tipoArticulo);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}
