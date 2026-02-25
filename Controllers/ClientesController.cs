using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoClub.Models;

namespace VideoClub.Controllers;

[Authorize]
public class ClientesController : Controller
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var data = _context.Clientes.ToList();
      return View(data);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var data = _context.Clientes.ToList();
        return Json(data);
    }

    [HttpPost]
    public IActionResult Create(Clientes cliente)
    {
        if(!ModelState.IsValid)
        {
            // If invalid, we will just return to the index with the data to show errors, or we can just redirect
            return RedirectToAction(nameof(Index));
        }

        cliente.Estado = (Clientes.EstadoCliente)cliente.Estado;
        cliente.Tipo_Persona = (Clientes.TipoPersona)cliente.Tipo_Persona;
        
        // Remove hyphens added by the frontend mask
        if(cliente.Cedula != null) {
            cliente.Cedula = cliente.Cedula.Replace("-", "");
        }
        if(cliente.NumeroTarjetaCR != null) {
            cliente.NumeroTarjetaCR = cliente.NumeroTarjetaCR.Replace("-", "");
        }

        _context.Clientes.Add(cliente);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    [HttpPut]
    public IActionResult Update([FromBody] Clientes cliente)
    {
        if (cliente == null)
        {
            return BadRequest();
        }

        var existing = _context.Clientes.Find(cliente.Id);
        if(existing == null)
        {
            return NotFound();
        }

        existing.Nombre = cliente.Nombre;
        existing.Cedula = cliente.Cedula;
        existing.NumeroTarjetaCR = cliente.NumeroTarjetaCR;
        existing.Limite_Credito = cliente.Limite_Credito;
        existing.Tipo_Persona = cliente.Tipo_Persona;
        existing.Monto_Entrega_Tardia = cliente.Monto_Entrega_Tardia;
        existing.Estado = cliente.Estado;
        
        _context.Clientes.Update(existing);
        _context.SaveChanges();

        return Ok(existing);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var data = _context.Clientes.Find(id);
        if(data == null)
        {
            return NotFound();
        }
        _context.Clientes.Remove(data);
        _context.SaveChanges();
        return Ok(data);
    }
}
