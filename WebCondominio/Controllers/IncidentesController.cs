using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCondominio.Models;

namespace WebApiCondominio.Controllers
{
    [EnableCors("WEBVUE")]
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentesController : ControllerBase
    {
        private readonly CondominioContext _context;

        public IncidentesController(CondominioContext context)
        {
            _context = context;
        }

        // GET: api/Incidentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incidente>>> GetIncidentes()
        {
            return await _context.Incidentes.ToListAsync();
        }

        // GET: api/Incidentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Incidente>> GetIncidente(int id)
        {
            var incidente = await _context.Incidentes.FindAsync(id);

            if (incidente == null)
            {
                return NotFound();
            }

            return incidente;
        }

        // PUT: api/Incidentes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncidente(int id, Incidente incidente)
        {
            if (id != incidente.Id)
            {
                return BadRequest();
            }

            _context.Entry(incidente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidenteExists(id))
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

        // POST: api/Incidentes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Incidente>> PostIncidente(Incidente incidente)
        {
            _context.Incidentes.Add(incidente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncidente", new { id = incidente.Id }, incidente);
        }

        // DELETE: api/Incidentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncidente(int id)
        {
            var incidente = await _context.Incidentes.FindAsync(id);
            if (incidente == null)
            {
                return NotFound();
            }

            _context.Incidentes.Remove(incidente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncidenteExists(int id)
        {
            return _context.Incidentes.Any(e => e.Id == id);
        }
    }
}
