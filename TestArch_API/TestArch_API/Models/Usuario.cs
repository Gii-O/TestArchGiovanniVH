using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestArch_API.Models
{
    /* ------------------------------------------------------------------------------------------------------------------*/
    /* ----- TABLE MODEL: USUARIOS -----*/
    /* ------------------------------------------------------------------------------------------------------------------*/
    public class Usuario
    {
        [Key]
        public int idUsuario  { get; set; }
        public string nombreUsuario { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }         
    }
}
