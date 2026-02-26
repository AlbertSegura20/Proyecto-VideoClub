using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using VideoClub.Models;
using VideoClub.ViewModels;

namespace VideoClub.Controllers;

// [Authorize]
public class ArticulosController : Controller
{
    private readonly AppDbContext _context;

    public ArticulosController(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        try
        {
            var vm = new ArticuloElencoViewModel
            {
                Articulos = await _context.Articulos.Include(a => a.TiposArticulos).Include(a => a.Idioma).ToListAsync(),
                Idiomas   = await _context.Idiomas.Where(i => i.Estado == Idiomas.EstadoIdioma.Activo).ToListAsync(),
                Elenco    = await _context.Elenco.Where(e => e.Estado == Elenco.EstadoElenco.Activo).ToListAsync(),
                ElencoArticulo = await _context.ElencoArticulo.ToListAsync(),
                TiposArticulos = await _context.TiposArticulos.Where(t => t.Estado == TiposArticulos.EstadoArticulo.Activo).ToListAsync()
            };

            ViewBag.TiposArticulos = vm.TiposArticulos; 
            ViewBag.Idiomas = vm.Idiomas;
            ViewBag.Elenco = vm.Elenco;

            try 
            {
                var jsonTest = System.Text.Json.JsonSerializer.Serialize(vm.Articulos);
            }
            catch(Exception ex)
            {
                Console.WriteLine("SERIALIZATION ERROR: " + ex.ToString());
            }

            return View(vm);
        }
        catch (Exception ex)
        {
            Console.WriteLine("EF CORE ERROR: " + ex.ToString());
            throw;
        }
    }


    [HttpPost]
    public IActionResult Create([FromBody] ArticuloCreationViewModel vm)
    {
        if (ModelState.IsValid)
        {
           
            var articulo = new Articulos
            {
                Titulo = vm.Titulo,
                TiposArticulosId = vm.TiposArticulosId,
                IdiomaId = vm.IdiomaId,
                RentaPorDia = vm.RentaPorDia,
                MontoEntregaTardia = vm.MontoEntregaTardia,
                Estado = (Articulos.EstadoArticulo)vm.Estado
            };

            _context.Articulos.Add(articulo);
            _context.SaveChanges(); 

            if (vm.Elenco != null && vm.Elenco.Any())
            {
                foreach (var item in vm.Elenco)
                {
                    _context.ElencoArticulo.Add(new ElencoArticulo
                    {
                        ArticuloId = articulo.Id,
                        ElencoId = item.ElencoId,
                        Rol = item.Rol
                    });
                }
                _context.SaveChanges();
            }

            return Ok(); 
        }

        return BadRequest(ModelState);
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

    [HttpGet]
    public IActionResult GetElencoByArticuloId(int id)
    {
        var elencoItems = _context.ElencoArticulo
            .Include(ea => ea.Elenco)
            .Where(ea => ea.ArticuloId == id)
            .Select(ea => new 
            {
                ea.Id,
                ea.ElencoId,
                Nombre = ea.Elenco.Nombre,
                ea.Rol
            })
            .ToList();

        return Json(elencoItems);
    }
}
