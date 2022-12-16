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
    public class DepartamentoesController : ControllerBase
    {
        private readonly CondominioContext _context;

        public DepartamentoesController(CondominioContext context)
        {
            _context = context;
        }

        // GET: api/Departamentoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos()
        {
            return await _context.Departamentos.ToListAsync();
        }

        // GET: api/Departamentoes
        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Departamento>>> GetDepartamentos(string fdescripcion)
        {

            // return await _context.Usuarios.ToListAsync();
            return await _context.Departamentos.Where(t => t.Descripcion.Contains(fdescripcion)).ToListAsync();

        }

        // GET: api/Departamentoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Departamento>> GetDepartamento(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);

            if (departamento == null)
            {
                return NotFound();
            }

            return departamento;
        }

        // PUT: api/Departamentoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartamento(int id, Departamento departamento)
        {
            if (id != departamento.Id)
            {
                return BadRequest();
            }

            _context.Entry(departamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartamentoExists(id))
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

        // POST: api/Departamentoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Departamento>> PostDepartamento(Departamento departamento)
        {
            _context.Departamentos.Add(departamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartamento", new { id = departamento.Id }, departamento);
        }

        // DELETE: api/Departamentoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartamento(int id)
        {
            var departamento = await _context.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }

            _context.Departamentos.Remove(departamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartamentoExists(int id)
        {
            return _context.Departamentos.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("buscarDepPorUsuario")]
        public List<DepartamentoDTO> CargarDepartPorUsuario(int idUsuario)
        {

            List<DepartamentoDTO> lista = (from d in _context.Departamentos
                                           join u in _context.Usuarios
                                           on d.Id equals u.Iddepartamento
                                           where u.Id == idUsuario
                                           select new DepartamentoDTO()
                                           {
                                               Id = d.Id,
                                               Torre = d.Torre,
                                               Piso = d.Piso,
                                               Numero = d.Numero
                                           }).ToList();
            return lista;
        }

    }
}
