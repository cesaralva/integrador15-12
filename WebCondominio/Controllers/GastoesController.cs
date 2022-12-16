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
    public class GastoesController : ControllerBase
    {
        private readonly CondominioContext _context;

        public GastoesController(CondominioContext context)
        {
            _context = context;
        }

        // GET: api/Gastoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gasto>>> GetGastos()
        {
            return await _context.Gastos.ToListAsync();
        }

        // GET: api/Gastoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Gasto>> GetGasto(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);

            if (gasto == null)
            {
                return NotFound();
            }

            return gasto;
        }

        // PUT: api/Gastoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasto(int id, Gasto gasto)
        {
            if (id != gasto.Id)
            {
                return BadRequest();
            }

            _context.Entry(gasto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GastoExists(id))
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

        // POST: api/Gastoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Gasto>> PostGasto(Gasto gasto)
        {
            _context.Gastos.Add(gasto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGasto", new { id = gasto.Id }, gasto);
        }

        // DELETE: api/Gastoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }

            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GastoExists(int id)
        {
            return _context.Gastos.Any(e => e.Id == id);
        }


        [HttpGet]
        [Route("buscarGastoPorUsuario")]
        public List<ReciboGastosDTO> CargarGastoPorUsuario(int idDepart , int annio , string mes)
        {

            List<ReciboGastosDTO> lista = (from d in _context.Recibosdetalles
                                           join r in _context.Recibos
                                           on d.Idrecibo equals r.Id
                                           join g in _context.Gastos
                                           on d.Idgastos equals g.Id
                                           where r.Iddepartamento == idDepart && r.Year == annio && 
                                           r.Mes.Equals(mes) && r.Estadopago.Equals("Pendiente")
                                           select new ReciboGastosDTO()
                                           {
                                               Id = g.Id,
                                               Concepto = g.Concepto,
                                               Monto = d.Monto,
                                               IdRecibo = r.Id
                                           }).ToList();
            return lista;
        }

        [HttpGet]
        [Route("pagarGasto")]
        public string PagarGastoRecibo(int idRecibo)
        {
            string msg = "";

            Recibo obj = _context.Recibos.Find(idRecibo);

            if (obj != null)
            {
                obj.Estadopago = "Por Validar";
                _context.Entry(obj).State = EntityState.Modified;

                if (_context.SaveChanges() > 0)
                {
                    msg = "OK";
                }
                else
                {
                    msg = "No se pudo efectuar pago";
                }
            }
            else
            {
                msg = "No se encontro recibo a pagar";
            }
            
            return msg;
        }
    }
}
