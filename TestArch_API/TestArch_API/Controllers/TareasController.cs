using System;
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
    /* ----- API REST: TAREAS -----*/
    /* ------------------------------------------------------------------------------------------------------------------*/
    [Route("TestArch/[controller]")]
    [ApiController]    
    public class TareasController : ControllerBase
    {
        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- CONTEXT ORM -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        private readonly TestArch_APIContext _context;

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- CONSTRUCTOR -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        public TareasController(TestArch_APIContext context)
        {
            _context = context;
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- GET: TestArch/Tareas -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTarea()
        {
            return await _context.tareas.ToListAsync();
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- GET: TestArch/Tareas/idTarea -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarea>> GetTarea(int id)
        {
            var tarea = await _context.tareas.FindAsync(id);

            if (tarea == null)
            {
                return NotFound();
            }

            return tarea;
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- PUT: TestArch/Tareas/idTarea -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/    
        [HttpPut("{id}")]
        public async Task<ActionResult<Tarea>> PutTarea(int id, Tarea tarea)
        {
            if (id != tarea.idTarea)
            {
                return BadRequest();
            }
           
            try
            {
                _context.Entry(tarea).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetTarea", new { id = tarea.idTarea }, tarea);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TareaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }            
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- POST: TestArch/Tareas -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [HttpPost]
        public async Task<ActionResult<Tarea>> PostTarea(Tarea tarea)
        {
            tarea.fechaCreacion = DateTime.Now;
            _context.tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarea", new { id = tarea.idTarea }, tarea);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- DELETE: TestArch/Tareas/idTareas -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/   
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tarea>> DeleteTarea(int id)
        {
            var tarea = await _context.tareas.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            _context.tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return tarea;
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- MÉTODOS DE UTILERÍA -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        private bool TareaExists(int id)
        {
            return _context.tareas.Any(e => e.idTarea == id);
        }
    }
}
