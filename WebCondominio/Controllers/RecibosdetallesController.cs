using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCondominio.Dto;
using WebApiCondominio.Models;

namespace WebApiCondominio.Controllers
{
    [EnableCors("WEBVUE")]
    [Route("api/[controller]")]
    [ApiController]
    public class RecibosdetallesController : ControllerBase
    {
        private readonly CondominioContext _context;

        public RecibosdetallesController(CondominioContext context)
        {
            _context = context;
        }

        // GET: api/Recibosdetalles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recibosdetalle>>> GetRecibosdetalles()
        {
            return await _context.Recibosdetalles.ToListAsync();
        }

        // GET: api/Recibosdetalles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recibosdetalle>> GetRecibosdetalle(int id)
        {
            var recibosdetalle = await _context.Recibosdetalles.FindAsync(id);

            if (recibosdetalle == null)
            {
                return NotFound();
            }

            return recibosdetalle;
        }

        // PUT: api/Recibosdetalles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecibosdetalle(int id, Recibosdetalle recibosdetalle)
        {
            if (id != recibosdetalle.Id)
            {
                return BadRequest();
            }

            _context.Entry(recibosdetalle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecibosdetalleExists(id))
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

        // POST: api/Recibosdetalles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recibosdetalle>> PostRecibosdetalle(Recibosdetalle recibosdetalle)
        {
            _context.Recibosdetalles.Add(recibosdetalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecibosdetalle", new { id = recibosdetalle.Id }, recibosdetalle);
        }

        // DELETE: api/Recibosdetalles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecibosdetalle(int id)
        {
            var recibosdetalle = await _context.Recibosdetalles.FindAsync(id);
            if (recibosdetalle == null)
            {
                return NotFound();
            }

            _context.Recibosdetalles.Remove(recibosdetalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecibosdetalleExists(int id)
        {
            return _context.Recibosdetalles.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("ListaDetalleGasto")]
        public List<ReciboGastosDTO> ListaDetalleGasto(int idRecibo)
        {
            List<ReciboGastosDTO> lista = (from  r in _context.Recibosdetalles
                                           join g in _context.Gastos
                                     on r.Idgastos equals g.Id
                                     where r.Idrecibo == idRecibo
                                     select new ReciboGastosDTO()
                                     {
                                         Id = g.Id,
                                         Concepto = g.Concepto,
                                         Monto = r.Monto
                                     }).ToList();
            return lista;

        }
    }
}
