using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestArch_API.Controllers;
using TestArch_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TestArch_API.Test.PruebasUnitarias
{
    [TestClass]
    public class UsuariosTareasControllerTest : BasePruebas
    {
        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: GET ELMENTOS SÍ EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task GetTareaXSiExistenteTest()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.tareas.Add(new Tarea() { nombreTarea = "Tarea 1" });
            contexto.tareas.Add(new Tarea() { nombreTarea = "Tarea 2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new TareasController(contexto);

            //Prueba
            var id = 1;
            var Estado = "";
            var Elemento = "";
            var Orden = "";
            var respuesta = await controller.GetTarea(id, Estado, Elemento, Orden);

            //Verificación
            var resultado = respuesta.Value;
            Assert.AreEqual(id, resultado.idTarea);
        }
    }
}
