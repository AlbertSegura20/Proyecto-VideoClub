using Microsoft.AspNetCore.Mvc;
using VideoClub.Models;

namespace VideoClub.Controllers;

public class ElencoArticuloController : Controller
{
    private readonly AppDbContext _context;

    public ElencoArticuloController(AppDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        return View(_context.ElencoArticulo.ToList());
    }

    [HttpPost]
    public IActionResult Create(ElencoArticulo elencoArticulo)
    {
        if(!ModelState.IsValid)
        {
            return View(elencoArticulo);
        }

        _context.ElencoArticulo.Add(elencoArticulo);
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
}