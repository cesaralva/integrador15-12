using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCondominio.Models;
using WebApiCondominio.Utils;

namespace WebApiCondominio.Controllers
{
    [EnableCors("WEBVUE")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly CondominioContext _context;

        public UsuariosController(CondominioContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios(string fnombres)
        {

            // return await _context.Usuarios.ToListAsync();
            return await _context.Usuarios.Where(t=>t.Nombres.Contains(fnombres) || t.Apellidos.Contains(fnombres) || t.Telefono.ToString().Contains(fnombres) || t.Dni.ToString().Contains(fnombres)).ToListAsync();
        }
        // GET: api/Usuarios
        [HttpGet("contarusuario/{ftipo}")]
        public async Task<ActionResult<int>> Getcontartipo(int ftipo)
        {
         int count =await _context.Usuarios.Where(t => t.Idrol==(ftipo)).CountAsync();
            // return await _context.Usuarios.ToListAsync();
            return count;
        }
        // GET: api/Usuarios
        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
        [HttpGet("login")]
        public async Task<ActionResult<Usuario>> login(String usuario, string password)
        {
            return await _context.Usuarios.Where(t => t.Email == usuario && t.Clave == password).FirstOrDefaultAsync();

        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            List<Usuario> correos = await _context.Usuarios.Where(t => t.Email.Contains(usuario.Email)).ToListAsync();

            if (correos.Any())
            {
                return correos.First();
            }
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // GET: api/Usuarios/busqueda/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpGet("busqueda/{correo}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> BuscarCorreo(String correo)
        {
            return await _context.Usuarios.Where(t => t.Email.Contains(correo)).ToListAsync();
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // GET: api/Usuarios/ROL
        [HttpGet("roles/{Idrol}")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios(int Idrol)
        {

            // return await _context.Usuarios.ToListAsync();
            return await _context.Usuarios.Where(t => t.Idrol == (Idrol)).ToListAsync();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }


        [HttpGet]
        [Route("generarCodigo")]
        public string GenerarCodigoRecuperacion(string correo)
        {
            string msg = "";

            Usuario obj = _context.Usuarios.Where(x => x.Email.Trim().Equals(correo.Trim())).FirstOrDefault();

            if (obj != null)
            {
                Random rand = new Random();
                obj.CodigoVerificacion = rand.Next(1000 , 9999 + 1);
                obj.FechaVerificacion = DateTime.Now.AddMinutes(10);
                _context.Entry(obj).State = EntityState.Modified;

                if (_context.SaveChanges() > 0)
                {
                    Correo objCorreo = new Correo();
                    objCorreo.EnviarCorreo(obj);
                    msg = "OK";
                }
                else
                {
                    msg = "No se pudo generar codigo de verificación";
                }
            }
            else
            {
                msg = "Lo sentimos el correo "+correo+" no se encuentra registrado";
            }

            return msg;
        }

        [HttpGet]
        [Route("verificarCodigo")]
        public ApiResponse verificarCodigo(int codigo, string correo)
        {
            ApiResponse api = new ApiResponse();
            string msg = "";

            Usuario obj = _context.Usuarios.Where(x => x.CodigoVerificacion == codigo && x.Email.Equals(correo)).FirstOrDefault();

            if (obj != null)
            {
                DateTime actual = DateTime.Now;

                if (actual <= obj.FechaVerificacion)
                {
                    api.data = obj;
                    api.msg = "OK";
                }
                else
                {
                    api.msg = "La fecha fecha y hora verificación ya expiró";
                }
               
            }
            else
            {
                api.msg = "No se encontraron datos";
            }
           
            return api;
        }

        [HttpGet]
        [Route("actualizarContrasenia")]
        public string actualizarContrasenia(int idUsuario, string contrasenia)
        {
            
            string msg = "";

            Usuario obj = _context.Usuarios.Where(x => x.Id == idUsuario).FirstOrDefault();

            if (obj != null)
            {
                obj.Clave = contrasenia;
                _context.Entry(obj).State = EntityState.Modified;

                if (_context.SaveChanges() > 0)
                {
                   
                    msg = "OK";
                }
                else
                {
                    msg = "No se pudo actualizar contraseña.";
                }
            }
            else
            {
                msg = "No se pudo obtener datos del usuario.";
            }
            return msg;
        }
    }
   
}
