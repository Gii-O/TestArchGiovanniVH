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
    public class UsuariosControllerTest : BasePruebas
    {
        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: GET TODOS LOS ELMENTOS -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task GetUsuarioTest()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.usuarios.Add(new Usuario() { nombreUsuario = "username" });
            contexto.usuarios.Add(new Usuario() { nombreUsuario = "usernameDos" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            //Prueba
            var controller = new UsuariosController(contexto2);
            var respuesta = await controller.GetUsuario();

            //Verificación
            var usuarios = respuesta.Value;
            Assert.AreEqual(2, usuarios.Count());
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: GET ELMENTOS NO EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task GetUsuarioIdNoExistenteTest()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            //Prueba
            var controller = new UsuariosController(contexto);
            var respuesta = await controller.GetUsuario(1);

            //Verificación
            var resultado = respuesta.Result as StatusCodeResult;
            Assert.AreEqual(404, resultado.StatusCode);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: GET ELMENTOS SÍ EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task GetUsuarioIdSiExistenteTest()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.usuarios.Add(new Usuario() { nombreUsuario = "username" });
            contexto.usuarios.Add(new Usuario() { nombreUsuario = "usernameDos" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new UsuariosController(contexto);

            //Prueba
            var id = 1;
            var respuesta = await controller.GetUsuario(id);

            //Verificación
            var resultado = respuesta.Value;
            Assert.AreEqual(id, resultado.idUsuario);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: POST NUEVO ELMENTO -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task PostUsuarioTest()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            var nuavaUsuario = new Usuario() { nombreUsuario = "username" };

            //Prueba
            var controller = new UsuariosController(contexto);
            var respuesta = await controller.PostUsuario(nuavaUsuario);

            //Verificación
            var resultado = respuesta;
            Assert.IsNotNull(resultado);

            var contexto2 = ConstruirContext(nombreBD);
            var cantidad = await contexto2.usuarios.CountAsync();
            Assert.AreEqual(1, cantidad);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: PUT EDITAR ELMENTO SÍ EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task PutUsuarioTest()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.usuarios.Add(new Usuario() { nombreUsuario = "username" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);
            var controller = new UsuariosController(contexto);

            var nuavaUsuario = new Usuario() { nombreUsuario = "usernameNuevo" };

            //Prueba
            var id = 1;
            var respuesta = await controller.PutUsuario(id, nuavaUsuario);

            //Verificación
            var resultado = respuesta;
            Assert.IsNotNull(resultado);

            var contexto3 = ConstruirContext(nombreBD);
            var existe = await contexto3.usuarios.AnyAsync(x => x.nombreUsuario == "username");
            Assert.IsTrue(existe);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: DELETE ELIMINAR ELMENTO NO EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task DeleteUsuarioNoExistenteTest()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            //Prueba
            var controller = new UsuariosController(contexto);
            var respuesta = await controller.DeleteUsuario(1);

            //Verificación
            var resultado = respuesta.Result as StatusCodeResult;
            Assert.AreEqual(404, resultado.StatusCode);
        }

        /* ------------------------------------------------------------------------------------------------------------------*/
        /* ----- TEST: DELETE ELIMINAR ELMENTO SÍ EXISTENTE -----*/
        /* ------------------------------------------------------------------------------------------------------------------*/
        [TestMethod]
        public async Task DeleteUsuarioSiExistenteTest()
        {
            //Preparación
            var nombreBD = Guid.NewGuid().ToString();
            var contexto = ConstruirContext(nombreBD);

            contexto.usuarios.Add(new Usuario() { nombreUsuario = "username" });
            await contexto.SaveChangesAsync();

            var contexto2 = ConstruirContext(nombreBD);

            //Prueba
            var controller = new UsuariosController(contexto2);
            var respuesta = await controller.DeleteUsuario(1);

            //Verificación
            var resultado = respuesta;
            Assert.IsNotNull(resultado);

            var contexto3 = ConstruirContext(nombreBD);
            var existe = await contexto3.usuarios.AnyAsync();
            Assert.IsFalse(existe);
        }
    }
}
