using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoClub.Models;

namespace VideoClub.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GenerosApiController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Generos>>> GetGeneros()
        {
            return await _context.Generos.OrderBy(g => g.Id).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Generos>> GetGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);

            if (genero == null)
            {
                return NotFound();
            }

            return genero;
        }

        [HttpPost]
        public async Task<ActionResult<Generos>> PostGenero(Generos genero)
        {
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGenero), new { id = genero.Id }, genero);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGenero(int id, Generos genero)
        {
            if (id != genero.Id)
            {
                return BadRequest(new { message = "El ID en la URL no coincide con el ID del cuerpo." });
            }

            var existingGenero = await _context.Generos.FindAsync(id);
            if (existingGenero == null)
            {
                return NotFound();
            }

            existingGenero.Descripcion = genero.Descripcion;
            existingGenero.Estado = genero.Estado;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenero(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }

            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeneroExists(int id)
        {
            return _context.Generos.Any(e => e.Id == id);
        }
    }
}
