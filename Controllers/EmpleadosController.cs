using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoClub.Models;

namespace VideoClub.Controllers;

[Authorize(Roles = "Administrador")]
public class EmpleadosController : Controller
{
    private readonly AppDbContext _context;

    public EmpleadosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var data = _context.Empleados.ToList();
      return View(data);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var data = _context.Empleados.ToList();
        return Json(data);
    }

    [HttpPost]
    public IActionResult Create(Empleados empleado)
    {
        if(!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        empleado.Estado = (Empleados.EstadoEmpleados)empleado.Estado;
        
        _context.Empleados.Add(empleado);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpPut]
    public IActionResult Update([FromBody] Empleados empleado)
    {
        if (empleado == null)
        {
            return BadRequest();
        }

        var existing = _context.Empleados.Find(empleado.Id);
        if(existing == null)
        {
            return NotFound();
        }

        existing.Nombre = empleado.Nombre;
        existing.Correo = empleado.Correo;
        if (!string.IsNullOrEmpty(empleado.Password)) {
            existing.Password = empleado.Password;
        }
        existing.Cedula = empleado.Cedula;
        existing.TandaLabor = empleado.TandaLabor;
        existing.PorcentajeComision = empleado.PorcentajeComision;
        existing.Fecha_Ingreso = empleado.Fecha_Ingreso;
        existing.Estado = empleado.Estado;
        
        _context.Empleados.Update(existing);
        _context.SaveChanges();

        return Ok(existing);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var data = _context.Empleados.Find(id);
        if(data == null)
        {
            return NotFound();
        }
        _context.Empleados.Remove(data);
        _context.SaveChanges();
        return Ok(data);
    }
}
