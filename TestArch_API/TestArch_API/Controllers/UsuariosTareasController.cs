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
    /* ----- API REST: TAREAS POR USUARIO -----*/
    /* ------------------------------------------------------------------------------------------------------------------*/
    [Route("TestArch/usuarios")]
    [ApiController]
    public class UsuariosTareasController : ControllerBase
    {
        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- CONTEXT ORM -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        private readonly TestArch_APIContext _context;

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- CONSTRUCTOR -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        public UsuariosTareasController(TestArch_APIContext context)
        {
            this._context = context;
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- GET: TestArch/Usuarios/idUsuario/tareas -----*/
        /* ----- Devuelve todas las tareas relacionadas a un usuario ----*/
        /* ------------------------------------------------------------------------------------------------------------------*/        
        [HttpGet("{id}/tareas")]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTarea(int id)
        {
            var tareasPorUsuario = _context.tareas.Where(t => t.idUsuario == id);
            return await tareasPorUsuario.ToListAsync();
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- MÉTODOS DE ORDENAMIENTO POR PROPIEDADES DE LA TAREA -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        private IQueryable<Tarea> GetTarea_Ordena_Id(IQueryable<Tarea> tareas, string order = "asc")
        {
            IQueryable<Tarea> tareasPorUsuario;

            if (order == "desc")
            {
                tareasPorUsuario = tareas.OrderByDescending(t => t.idTarea);
            }
            else
            {
                tareasPorUsuario = tareas.OrderBy(t => t.idTarea);
            }

            return tareasPorUsuario;
        }

        private IQueryable<Tarea> GetTarea_Ordena_Fecha(IQueryable<Tarea> tareas, string order = "asc")
        {
            IQueryable<Tarea> tareasPorUsuario;

            if (order == "desc")
            {
                tareasPorUsuario = tareas.OrderByDescending(t => t.fechaCreacion).ThenByDescending(t => t.idTarea);
            }
            else
            {
                tareasPorUsuario = tareas.OrderBy(t => t.fechaCreacion).ThenBy(t => t.idTarea);
            }

            return tareasPorUsuario;
        }

        private IQueryable<Tarea> GetTarea_Ordena_Nombre(IQueryable<Tarea> tareas, string order = "asc")
        {
            IQueryable<Tarea> tareasPorUsuario;

            if (order == "desc")
            {
                tareasPorUsuario = tareas.OrderByDescending(t => t.nombreTarea).ThenByDescending(t => t.idTarea);
            }
            else
            {
                tareasPorUsuario = tareas.OrderBy(t => t.nombreTarea).ThenBy(t => t.idTarea);
            }

            return tareasPorUsuario;
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- MÉTODO DE FILTRADO POR ESTADO DE LA TAREAS -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        private IQueryable<Tarea> GetTarea_Filtra_Estado(IQueryable<Tarea> tareas, string estado = "todas")
        {
            IQueryable<Tarea> tareasPorUsuario;

            if (estado == "todas" || estado == null)
            {
                tareasPorUsuario = tareas;                
            }
            else
            {
                tareasPorUsuario = tareas.Where(t => t.estado == estado);
            }            

            return tareasPorUsuario;
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- GET: TestArch/Usuarios/idUsuario/tareas/filtra?Estado=estado&&Elemento=elemento&&Orden=orden -----*/
        /* ----- Devuelve las tareas tras filtrar y ordenar relacionadas a un usuario ----*/
        /* ------------------------------------------------------------------------------------------------------------------*/     
        [HttpGet("{id}/tareas/filtra")]
        public async Task<ActionResult<IEnumerable<Tarea>>> GetTareaX(int id, string Estado, string Elemento, string Orden)
        {
            var tareasPorUsuario = _context.tareas.Where(t => t.idUsuario == id);
            tareasPorUsuario = GetTarea_Filtra_Estado(tareasPorUsuario, Estado);

            switch (Elemento)
            {
                case "fecha":
                    tareasPorUsuario = GetTarea_Ordena_Fecha(tareasPorUsuario, Orden);
                    break;
                case "nombre":
                    tareasPorUsuario = GetTarea_Ordena_Nombre(tareasPorUsuario, Orden);
                    break;
                default:
                    tareasPorUsuario = GetTarea_Ordena_Id(tareasPorUsuario, Orden);
                    break;
            }

            return await tareasPorUsuario.ToListAsync();
        }     

    }
}
