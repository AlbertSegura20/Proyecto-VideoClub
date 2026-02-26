using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
        var path = HttpContext.Request.Path;
        
        return View(new ViewPathModel { Path = path });
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
