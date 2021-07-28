// -----------------------------------------------------------------------------
// ---- DEPENDENCIAS
// -----------------------------------------------------------------------------
import React, { useState, useEffect } from 'react';
import Cookies from 'universal-cookie';

import swal from 'sweetalert';
import axios from 'axios';
import md5 from 'md5';

import 'bootstrap/dist/css/bootstrap.min.css';
import '../css/Tareas.css';
import '../css/Login.css';

import { getQueriesForElement } from '@testing-library/react';
import { FaUserPlus } from "react-icons/fa";

// -----------------------------------------------------------------------------
// ---- CONTENIDO DE LA VISTA
// -----------------------------------------------------------------------------

function AddUser(props) {

  // -----------------------------------------------------------------------------
  // ---- CONSTANTES Y HOOKS
  // -----------------------------------------------------------------------------

  const cookies = new Cookies();
  const baseUrl = "https://localhost:44347/TestArch/Usuarios";
  const [data, setData] = useState([]);

  const [usuario, setUsuario] = useState({
    nombreUsuario : '',
    nombre : '',
    password : '',
    passwordConfirm : ''
  });

  // -----------------------------------------------------------------------------
  // ---- MÉTODOS HANDLE PARA LOS HOOKS
  // -----------------------------------------------------------------------------

  const handleChange = e => {
    const {name, value} = e.target;
    setUsuario({
      ...usuario,
      [name] : value
    });
    console.log(setUsuario);
  }

  // -----------------------------------------------------------------------------
  // ---- MÉTODOS DE CONSUMO DE LA API REST
  // -----------------------------------------------------------------------------

  const peticionPost = async() => {
    if(usuario.password !== usuario.passwordConfirm){
      var message = "Revisa que tus contraseñas sean iguales.";
      mostrarAlertaWar(message);
    }
    else if(usuario.nombreUsuario === '' || usuario.nombre === '' || usuario.password === ''){
      var message = "Asegurate de que todos los campos esten completos.";
      mostrarAlertaWar(message);
    }
    else {
      delete usuario.passwordConfirm;
      usuario.password = md5(usuario.password);
      await axios.post(baseUrl, usuario)
      .then(response => {
        setData(data.concat(response.data));
        mostrarAlertaOk(response.data.nombreUsuario);
        RegresaLogin();
      })
      .catch(error => {
        console.log(error);
        var message = "Asegurate de que todos los campos esten completos.";
        mostrarAlertaWar(message);
      })
    }
  }

  // -----------------------------------------------------------------------------
  // ---- MÉTODOS DE USO PARA LA VISTA
  // -----------------------------------------------------------------------------

  const RegresaLogin = () => {
    props.history.push('./');
  }

  // -----------------------------------------------------------------------------
  // ---- ALERTAS SWAL
  // -----------------------------------------------------------------------------

  const mostrarAlertaOk = (nombre) => {
    swal({
      title : "Increible",
      text : "Te has registrado exitosamente como "+nombre+". Ya puedes ingresar.",
      icon : "success",
      button : "Aceptar",
      timer : "4500",
    });
  }

  const mostrarAlertaWar = (message) => {
    swal({
      title : "Ups!",
      text : message,
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
        <h2>Registrate</h2>
      </div>

      {/* ----------------------------------------------------------------------------- */}
      {/* ---- FORMULARIO DE REGISTRO DE USUARIO                                        */}
      {/* ----------------------------------------------------------------------------- */}
      <div className="containerAmplio">
        <div className="containerLogin">
          <div className="form-group">
            <label>Usuario: </label>
            <br />
            <input type="text" className="form-control" name="nombreUsuario" onChange={handleChange}/>
            <br />
            <label>Nombre Completo: </label>
            <br />
            <input type="text" className="form-control" name="nombre" onChange={handleChange}/>
            <br />
            <label>Contraseña: </label>
            <br />
            <input type="text" className="form-control" name="passwordConfirm" onChange={handleChange}/>
            <br />
            <label>Confirma tu Contraseña: </label>
            <br />
            <input type="password" className="form-control" name="password" onChange={handleChange}/>
            <br />
          </div>

          <button className="btn btn-light MiRight" onClick={() => RegresaLogin()}>Regresar</button>
          <button className="btn btn-dark MiLeft" onClick={() => peticionPost()}>Registrar <FaUserPlus/></button>

        </div>
      </div>

    </div>
  );
}

// -----------------------------------------------------------------------------
// ---- EXPORTAR VISTA
// -----------------------------------------------------------------------------
export default AddUser;
