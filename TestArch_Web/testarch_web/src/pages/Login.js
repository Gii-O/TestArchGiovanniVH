// -----------------------------------------------------------------------------
// ---- DEPENDENCIAS
// -----------------------------------------------------------------------------
import React, {useState, useEffect} from 'react';
import Cookies from 'universal-cookie';

import swal from 'sweetalert';
import axios from 'axios';
import md5 from 'md5';

import 'bootstrap/dist/css/bootstrap.min.css';
import '../css/Login.css';

import { getQueriesForElement } from '@testing-library/react';
import { FaUserPlus } from "react-icons/fa";

// -----------------------------------------------------------------------------
// ---- CONTENIDO DE LA VISTA
// -----------------------------------------------------------------------------

function Login(props) {

  // -----------------------------------------------------------------------------
  // ---- CONSTANTES Y HOOKS
  // -----------------------------------------------------------------------------

  const baseUrl = "https://localhost:44347/TestArch/Usuarios";
  const cookies = new Cookies();

  const [form, setForm] = useState({
    nombreUsuario : '',
    password : ''
  });

  // -----------------------------------------------------------------------------
  // ---- MÉTODOS HANDLE PARA LOS HOOKS
  // -----------------------------------------------------------------------------

  const handleChange = e => {
    const {name, value} = e.target;
    setForm({
      ...form,
      [name] : value
    });
    console.log(form);
  }

  // -----------------------------------------------------------------------------
  // ---- MÉTODOS DE CONSUMO DE LA API REST
  // -----------------------------------------------------------------------------

  const iniciarSesion = async() => {
    await axios.get(baseUrl+`/${form.nombreUsuario}/${md5(form.password)}`)
    .then(response => {
      return response.data;
    })
    .then(response =>{
      if(response.length >0) {
        var respuesta = response[0];
        cookies.set('idUsuario', respuesta.idUsuario, {path: '/'});
        cookies.set('nombreUsuario', respuesta.nombreUsuario, {path: '/'});
        cookies.set('nombre', respuesta.nombre, {path: '/'});
        cookies.set('password', respuesta.password, {path: '/'});
        mostrarAlertaOk(respuesta.nombre);
        props.history.push('/tareas');
      }
      else{
        mostrarAlertaNot();
      }
    })
    .catch(error => {
      console.log(error);
      mostrarAlertaWar();
    })
  }

  // -----------------------------------------------------------------------------
  // ---- MÉTODOS DE USO PARA LA VISTA
  // -----------------------------------------------------------------------------

  const RegistraUsuario = () => {
    props.history.push('/registro');
  }

  // -----------------------------------------------------------------------------
  // ---- ALERTAS SWAL
  // -----------------------------------------------------------------------------

  const mostrarAlertaOk = (nombre) => {
    swal({
      title : "Bienvenido",
      text : nombre,
      icon : "success",
      button : "Aceptar",
      timer : "3500",
    });
  }

  const mostrarAlertaNot = (nombre) => {
    swal({
      title : "Verifica",
      text : "El Usuario o Contraseña No Son Correctos",
      icon : "error",
      button : "Aceptar",
      timer : "3500",
    });
  }

  const mostrarAlertaWar = (nombre) => {
    swal({
      title : "Ingresa",
      text : "Tu Usuario y Contraseña",
      icon : "warning",
      button : "Aceptar",
      timer : "3500",
    });
  }

  // -----------------------------------------------------------------------------
  // ---- DEFINICIÓN DEL USE EFECT
  // -----------------------------------------------------------------------------

  useEffect(() => {
    if(cookies.get('idUsuario')){
      props.history.push('/tareas');
    }
  }, []);

  // -----------------------------------------------------------------------------
  // ---- CONTENIDO HTML
  // -----------------------------------------------------------------------------

  return (
    <div>

    {/* ----------------------------------------------------------------------------- */}
    {/* ---- TÍTULO                                                                   */}
    {/* ----------------------------------------------------------------------------- */}
      <div className="border-primary bg-primary text-white MiTituloLog col-xm-12 col-sm-10 col-md-8 col-lg-6">
        <h2>Fase 2: Giovanni de la Vega Huerta</h2>
      </div>

      {/* ----------------------------------------------------------------------------- */}
      {/* ---- FORMULARIO DE REGISTRO DE USUARIO                                        */}
      {/* ----------------------------------------------------------------------------- */}
      <div className="containerPrincipal">
        <div className="containerLogin">
          <div className="form-group">
            <label>Usuario: </label>
            <br />
            <input type="text" className="form-control" name="nombreUsuario" onChange={handleChange}/>
            <br />
            <label>Contraseña: </label>
            <br />
            <input type="password" className="form-control" name="password" onChange={handleChange}/>
            <br />
            <button className="btn btn-primary" onClick={() => iniciarSesion()}>Iniciar Sesión</button>            
            <br/><br/>
            <button className="btn btn-sm btn-dark" onClick={() => RegistraUsuario()}>Registrarse <FaUserPlus/></button>
          </div>
        </div>
      </div>
    </div>
  );
}

// -----------------------------------------------------------------------------
// ---- EXPORTAR VISTA
// -----------------------------------------------------------------------------
export default Login;
