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
