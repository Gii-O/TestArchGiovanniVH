using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestArch_API.Models
{
    /* ------------------------------------------------------------------------------------------------------------------*/
    /* ----- TABLE MODEL: TAREAS -----*/
    /* ------------------------------------------------------------------------------------------------------------------*/
    public class Tarea
    {
        [Key]
        public int idTarea { get; set; }
        public int idUsuario { get; set; }
        public string nombreTarea { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string estado { get; set; }

    }
}
