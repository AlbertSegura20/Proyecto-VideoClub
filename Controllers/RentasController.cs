using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoClub.Models;
using VideoClub.ViewModels;

namespace VideoClub.Controllers;

public class RentasController : Controller
{
    private readonly AppDbContext _context;

    public RentasController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var vm = new RentasViewModel
            {
                Rentas = await _context.Rentas
                    .Include(r => r.Empleado)
                    .Include(r => r.Articulo)
                    .Include(r => r.Cliente)
                    .OrderByDescending(r => r.NoRenta)
                    .ToListAsync(),
                Empleados = await _context.Empleados.Where(e => e.Estado == Empleados.EstadoEmpleados.Activo).ToListAsync(),
                Articulos = await _context.Articulos.Where(a => a.Estado == Articulos.EstadoArticulo.Disponible).ToListAsync(),
                Clientes = await _context.Clientes.Where(c => c.Estado == Clientes.EstadoCliente.Activo).ToListAsync()
            };

            ViewBag.Empleados = vm.Empleados;
            ViewBag.Articulos = vm.Articulos;
            ViewBag.Clientes = vm.Clientes;

            return View(vm);
        }
        catch (Exception ex)
        {
            Console.WriteLine("EF CORE ERROR: " + ex.ToString());
            throw;
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody] Renta vm)
    {
        if (ModelState.IsValid)
        {
            var renta = new Renta
            {
                EmpleadoId = vm.EmpleadoId,
                ArticuloId = vm.ArticuloId,
                ClienteId = vm.ClienteId,
                FechaRenta = vm.FechaRenta,
                FechaDevolucion = vm.FechaDevolucion,
                MontoPorDia = vm.MontoPorDia,
                CantidadDias = vm.CantidadDias,
                Comentario = vm.Comentario,
                Estado = vm.Estado
            };

            // Update Article Status to Rentado
            var articulo = _context.Articulos.Find(vm.ArticuloId);
            if (articulo != null)
            {
                articulo.Estado = Articulos.EstadoArticulo.Rentado;
                _context.Articulos.Update(articulo);
            }

            _context.Rentas.Add(renta);
            _context.SaveChanges();

            return Ok();
        }

        return BadRequest(ModelState);
    }

    [HttpPut]
    public IActionResult Update([FromBody] Renta renta)
    {
        if (renta == null) return BadRequest();

        var existing = _context.Rentas.Find(renta.NoRenta);
        if (existing == null) return NotFound();

        // If returned or changed article, handle article status logic here ideally.
        // For simplicity, we just update the Renta record.
        if (existing.ArticuloId != renta.ArticuloId)
        {
            // Set old article to available
            var oldArticulo = _context.Articulos.Find(existing.ArticuloId);
            if(oldArticulo != null){
                 oldArticulo.Estado = Articulos.EstadoArticulo.Disponible;
                _context.Articulos.Update(oldArticulo);
            }
           
            // Set new article to rentado
            var newArticulo = _context.Articulos.Find(renta.ArticuloId);
            if(newArticulo != null){
                newArticulo.Estado = Articulos.EstadoArticulo.Rentado;
                _context.Articulos.Update(newArticulo);
            }
        }

        if (renta.Estado == Renta.EstadoRenta.Devuelta || renta.Estado == Renta.EstadoRenta.Cancelada)
        {
             var articulo = _context.Articulos.Find(renta.ArticuloId);
             if (articulo != null)
             {
                 articulo.Estado = Articulos.EstadoArticulo.Disponible;
                 _context.Articulos.Update(articulo);
             }
        }

        existing.EmpleadoId = renta.EmpleadoId;
        existing.ArticuloId = renta.ArticuloId;
        existing.ClienteId = renta.ClienteId;
        existing.FechaRenta = renta.FechaRenta;
        existing.FechaDevolucion = renta.FechaDevolucion;
        existing.MontoPorDia = renta.MontoPorDia;
        existing.CantidadDias = renta.CantidadDias;
        existing.Comentario = renta.Comentario;
        existing.Estado = renta.Estado;

        _context.Rentas.Update(existing);
        _context.SaveChanges();

        return Ok(existing);
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var data = _context.Rentas.Find(id);
        if (data == null) return NotFound();

        // Optional: restore article status to available if needed
        var articulo = _context.Articulos.Find(data.ArticuloId);
        if (articulo != null && data.Estado != Renta.EstadoRenta.Devuelta)
        {
            articulo.Estado = Articulos.EstadoArticulo.Disponible;
            _context.Articulos.Update(articulo);
        }

        _context.Rentas.Remove(data);
        _context.SaveChanges();
        return Ok(data);
    }
}
