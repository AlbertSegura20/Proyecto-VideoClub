using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using VideoClub.Models;

namespace VideoClub.Controllers;

[Authorize]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var path = HttpContext.Request.Path.Value ?? string.Empty;
        var today = DateTime.UtcNow.Date;

        var rentasHoy = _context.Rentas.Count(r => r.FechaRenta.Date == today);
        var rentasPendientes = _context.Rentas.Count(r => r.Estado == Renta.EstadoRenta.Activa);
        var entregasAtrasadas = _context.Rentas.Count(r => r.Estado == Renta.EstadoRenta.Activa && r.FechaDevolucion.Date < today);
        var articulosDisponibles = _context.Articulos.Count(a => a.Estado == Articulos.EstadoArticulo.Disponible);

        var ultimasRentas = _context.Rentas
            .Include(r => r.Cliente)
            .Include(r => r.Articulo)
            .OrderByDescending(r => r.FechaRenta)
            .Take(5)
            .ToList();

        var viewModel = new DashboardViewModel
        {
            RentasHoy = rentasHoy,
            RentasPendientes = rentasPendientes,
            EntregasAtrasadas = entregasAtrasadas,
            ArticulosDisponibles = articulosDisponibles,
            UltimasRentas = ultimasRentas,
            Path = path
        };
        
        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult MakeAdmin()
    {
        var firstUser = _context.Empleados.FirstOrDefault();
        if (firstUser != null)
        {
            firstUser.Rol = Models.Empleados.RolEmpleado.Administrador;
            _context.SaveChanges();
            return Content($"El usuario {firstUser.Correo} ahora es Administrador.");
        }
        return Content("No hay usuarios.");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
