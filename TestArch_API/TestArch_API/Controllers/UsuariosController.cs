using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestArch_API.Data;
using TestArch_API.Models;

namespace TestArch_API.Controllers
{
    /* ------------------------------------------------------------------------------------------------------------------*/
    /* ----- API REST: USUARIOS -----*/
    /* ------------------------------------------------------------------------------------------------------------------*/
    [Route("TestArch/[controller]")]
    [ApiController]    
    public class UsuariosController : ControllerBase
    {
        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- CONTEXT ORM -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        private readonly TestArch_APIContext _context;

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- CONSTRUCTOR -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        public UsuariosController(TestArch_APIContext context)
        {
            this._context = context;
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- GET: TestArch/Usuarios -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _context.usuarios.ToListAsync();
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- GET: TestArch/Usuarios/idUsuario -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/   
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- PUT: TestArch/Usuarios/idUsuario -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/       
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.idUsuario)
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

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- POST: TestArch/Usuarios -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/          
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.idUsuario }, usuario);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- DELETE: TestArch/Usuarios/id -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> DeleteUsuario(int id)
        {
            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- MÉTODOS DE UTILERÍA -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        private bool UsuarioExists(int id)
        {
            return _context.usuarios.Any(e => e.idUsuario == id);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- MÉTODO DE ACCESO DE USUARIO PARA SESIÓN -----*/
        /* ----- GET: TestArch/Usuarios/nombreUsuaio/password -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [HttpGet("{nombreUsuario}/{password}")]
        public ActionResult<List<Usuario>> GetIniciarSesion(string nombreUsuario, string password)
        {
            var usuarios = _context.usuarios.Where(usuario => usuario.nombreUsuario.Equals(nombreUsuario) && usuario.password.Equals(password)).ToList();

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }
    }    
}
