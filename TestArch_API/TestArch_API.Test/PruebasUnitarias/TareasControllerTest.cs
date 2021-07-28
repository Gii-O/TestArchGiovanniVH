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
    public class TareasControllerTest : BasePruebas
    {
        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: GET TODOS LOS ELMENTOS -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task GetTareaTest()
        {
            //Preparaci�n
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.tareas.Add(new Tarea() { nombreTarea = "Tarea 1" });
            contexto.tareas.Add(new Tarea() { nombreTarea = "Tarea 2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            //Prueba
            var controller = new TareasController(contexto2);
            var respuesta = await controller.GetTarea();

            //Verificaci�n
            var tareas = respuesta.Value;
            Assert.AreEqual(2, tareas.Count());
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: GET ELMENTOS NO EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task GetTareaIdNoExistenteTest()
        {
            //Preparaci�n
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            //Prueba
            var controller = new TareasController(contexto);
            var respuesta = await controller.GetTarea(1);

            //Verificaci�n
            var resultado = respuesta.Result as StatusCodeResult;
            Assert.AreEqual(404, resultado.StatusCode);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: GET ELMENTOS S� EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task GetTareaIdSiExistenteTest()
        {
            //Preparaci�n
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.tareas.Add(new Tarea() { nombreTarea = "Tarea 1" });
            contexto.tareas.Add(new Tarea() { nombreTarea = "Tarea 2" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new TareasController(contexto);

            //Prueba
            var id = 1;            
            var respuesta = await controller.GetTarea(id);

            //Verificaci�n
            var resultado = respuesta.Value;
            Assert.AreEqual(id, resultado.idTarea);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: POST NUEVO ELMENTO -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task PostTareaTest()
        {
            //Preparaci�n
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);          

            var nuavaTarea = new Tarea() { nombreTarea = "Tarea 1" };

            //Prueba
            var controller = new TareasController(contexto);
            var respuesta = await controller.PostTarea(nuavaTarea);

            //Verificaci�n
            var resultado = respuesta;
            Assert.IsNotNull(resultado);

            var contexto2 = ConstruirContext(nombreBD);
            var cantidad = await contexto2.tareas.CountAsync();
            Assert.AreEqual(1, cantidad);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: PUT EDITAR ELMENTO S� EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task PutTareaTest()
        {
            //Preparaci�n
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.tareas.Add(new Tarea() { nombreTarea = "Tarea 1" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new TareasController(contexto);

            var nuavaTarea = new Tarea() { nombreTarea = "Tarea Editada" };

            //Prueba
            var id = 1;
            var respuesta = await controller.PutTarea(id, nuavaTarea);

            //Verificaci�n
            var resultado = respuesta;
            Assert.IsNotNull(resultado);

            var contexto3 = ConstruirContext(nombreBD);
            var existe = await contexto3.tareas.AnyAsync(x => x.nombreTarea == "Tarea 1");
            Assert.IsTrue(existe);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: DELETE ELIMINAR ELMENTO NO EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task DeleteTareaNoExistenteTest()
        {
            //Preparaci�n
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            //Prueba
            var controller = new TareasController(contexto);
            var respuesta = await controller.DeleteTarea(1);

            //Verificaci�n
            var resultado = respuesta.Result as StatusCodeResult;
            Assert.AreEqual(404, resultado.StatusCode);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: DELETE ELIMINAR ELMENTO S� EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task DeleteTareaSiExistenteTest()
        {
            //Preparaci�n
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.tareas.Add(new Tarea() { nombreTarea = "Tarea 1" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            //Prueba
            var controller = new TareasController(contexto2);
            var respuesta = await controller.DeleteTarea(1);

            //Verificaci�n
            var resultado = respuesta;
            Assert.IsNotNull(resultado);

            var contexto3 = ConstruirContext(nombreBD);
            var existe = await contexto3.tareas.AnyAsync();
            Assert.IsFalse(existe);
        }

    }
}
