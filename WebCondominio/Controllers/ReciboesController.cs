using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApiCondominio.Dto;
using WebApiCondominio.Models;

namespace WebApiCondominio.Controllers
{
    [EnableCors("WEBVUE")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReciboesController : ControllerBase
    {
        private readonly CondominioContext _context;

        public ReciboesController(CondominioContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("listadoRecibos")]
        public List<ReciboDTO> CargarListaRecibos(int annio, string mes)
        {
            List<ReciboDTO> lista = (from d in _context.Departamentos
                                     join u in _context.Usuarios
                                     on d.Id equals u.Iddepartamento
                                     join r in _context.Recibos
                                     on d.Id equals r.Iddepartamento
                                     where u.Estado == 1 && r.Year == annio && r.Mes.Equals(mes)
                                     select new ReciboDTO()
                                     {
                                         IdRecibo = r.Id,
                                         Iddepartamento = d.Id,
                                         torre = d.Torre,
                                         piso = d.Piso,
                                         numero = d.Numero,
                                         nombres = u.Nombres,
                                         apellidos = u.Apellidos,
                                         montopago = r.Montopago,
                                         fechavencimiento = r.Fechavencimiento,
                                         Estadopago = r.Estadopago
                                     }).ToList();
            return lista;

        }


        // GET: api/Reciboes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recibo>>> GetRecibos()
        {
            return await _context.Recibos.ToListAsync();
        }

        // GET: api/Reciboes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recibo>> GetRecibo(int id)
        {
            var recibo = await _context.Recibos.FindAsync(id);

            if (recibo == null)
            {
                return NotFound();
            }

            return recibo;
        }

        // PUT: api/Reciboes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecibo(int id, Recibo recibo)
        {
            if (id != recibo.Id)
            {
                return BadRequest();
            }

            _context.Entry(recibo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReciboExists(id))
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

        // POST: api/Reciboes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recibo>> PostRecibo(Recibo recibo)
        {
            _context.Recibos.Add(recibo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecibo", new { id = recibo.Id }, recibo);
        }
        // POST: api/Reciboes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("/generar")]
        //    public bool PostingresosRecibo(int annio, int mes)
        //{

            //var sp = "sp_generar_recibos";
            //   using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            //{
               
                   
            //        oConexion.Open();
            //    SqlCommand sql_cmnd = new SqlCommand(sp, oConexion);
            //    sql_cmnd.Parameters.AddWithValue("@annio", SqlDbType.Int).Value = annio;
            //    sql_cmnd.Parameters.AddWithValue("@mes", SqlDbType.Int).Value = mes;
            //    sql_cmnd.CommandType = CommandType.StoredProcedure;
            //    sql_cmnd.ExecuteNonQuery();
            //        oConexion.Close();
            ////}
            //return true;
        //}
        // DELETE: api/Reciboes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecibo(int id)
        {
            var recibo = await _context.Recibos.FindAsync(id);
            if (recibo == null)
            {
                return NotFound();
            }

            _context.Recibos.Remove(recibo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReciboExists(int id)
        {
            return _context.Recibos.Any(e => e.Id == id);
        }


       
    }
}
