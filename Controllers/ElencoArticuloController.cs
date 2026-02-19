using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoClub.Models;

namespace VideoClub.Controllers;

[Authorize]
public class ElencoArticuloController : Controller
{
    private readonly AppDbContext _context;

    public ElencoArticuloController(AppDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public IActionResult Add([FromBody] VideoClub.ViewModels.ElencoArticuloCreationDto dto)
    {
        if (ModelState.IsValid)
        {
            var entity = new ElencoArticulo
            {
                ArticuloId = dto.ArticuloId,
                ElencoId = dto.ElencoId,
                Rol = dto.Rol
            };
            
            _context.ElencoArticulo.Add(entity);
            _context.SaveChanges();
            return Ok();
        }
        return BadRequest(ModelState);
    }


    [HttpDelete]
    public IActionResult Delete([FromBody] int id)
    {
        var data = _context.ElencoArticulo.Find(id);
        if (data == null) return NotFound();

        _context.ElencoArticulo.Remove(data);
        _context.SaveChanges();
        return Ok(data);
    }
}