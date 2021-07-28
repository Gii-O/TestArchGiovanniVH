// -----------------------------------------------------------------------------
// ---- DEPENDENCIAS
// -----------------------------------------------------------------------------
import React, { useState, useEffect } from 'react';
import Cookies from 'universal-cookie';
import axios from 'axios';

import 'bootstrap/dist/css/bootstrap.min.css';
import '../css/Tareas.css';

import { getQueriesForElement } from '@testing-library/react';
import {Modal, ModalBody, ModalFooter, ModalHeader} from 'reactstrap';
import { FaUserCircle, FaAngleDoubleDown, FaTrashAlt, FaEdit, FaPlus, FaSignOutAlt } from "react-icons/fa";

// -----------------------------------------------------------------------------
// ---- CONTENIDO DE LA VISTA
// -----------------------------------------------------------------------------

function Tareas(props) {

  // -----------------------------------------------------------------------------
  // ---- CONSTANTES Y HOOKS
  // -----------------------------------------------------------------------------

  const cookies = new Cookies();

  const baseUrl = "https://localhost:44347/TestArch/Tareas";
  const baseUrl_Filter = "https://localhost:44347/TestArch/usuarios";
  const [data, setData] = useState([]);

  const [modalInsertarTarea, setModalInsertarTarea] = useState(false);
  const [modalEditarTarea, setModalEditarTarea] = useState(false);
  const [modalEliminarTarea, setModalEliminarTarea] = useState(false);

  const [orden, setOrden] = useState(false);

  const [tareaSeleccionada, setTareaSeleccionada] = useState({
    idTarea : '',
    idUsuario : '',
    nombreTarea : '',
    descripcion : '',
    fechaCreacion : '',
    estado : ''
  });

  const [filtroSeleccionado, setFiltroSeleccionada] = useState({
    Estado : '',
    Elemento : '',
    Orden : ''
  });

  // -----------------------------------------------------------------------------
  // ---- MÉTODOS HANDLE PARA LOS HOOKS
  // -----------------------------------------------------------------------------

  const handleChange = e => {
    const {name, value} = e.target;
    setTareaSeleccionada({
      ...tareaSeleccionada,
      [name] : value
    });
    console.log(tareaSeleccionada);
  }

  // -----------------------------------------------------------------------------
  // ---- MÉTODOS DE CONSUMO DE LA API REST
  // -----------------------------------------------------------------------------

  const peticionGet = async() => {
    await axios.get(baseUrl_Filter+"/"+cookies.get('idUsuario')+"/tareas")
    .then(response => {
      setData(response.data);
    })
    .catch(error => {
      console.log(error);
    })
  }

  const peticionGetFiltra = async() => {
    await axios.get(baseUrl_Filter+"/"+cookies.get('idUsuario')+"/tareas/filtra?Estado="+filtroSeleccionado.Estado+"&&Elemento="+filtroSeleccionado.Elemento+"&&Orden="+filtroSeleccionado.Orden)
    .then(response => {
      setData(response.data);
    })
    .catch(error => {
      console.log(error);
    })
  }

  const peticionPost = async() => {
    delete tareaSeleccionada.idTarea;
    delete tareaSeleccionada.fechaCreacion;
    tareaSeleccionada.idUsuario = parseInt(cookies.get('idUsuario'));
    await axios.post(baseUrl, tareaSeleccionada)
    .then(response => {
      setData(data.concat(response.data));
      accionModalInsertarTarea();
      peticionGetFiltra();
    })
    .catch(error => {
      console.log(error);
    })
  }

  const peticionPut = async() => {
    tareaSeleccionada.idUsuario = parseInt(tareaSeleccionada.idUsuario);
    await axios.put(baseUrl+"/"+tareaSeleccionada.idTarea, tareaSeleccionada)
    .then(response => {
      var respuesta = response.data;
      var dataAuxiliar = data;
      dataAuxiliar.map(tarea => {
        if(tarea.idTarea === tareaSeleccionada.idTarea){
          tarea.nombreTarea = respuesta.nombreTarea;
          tarea.descripcion = respuesta.descripcion;
          tarea.estado = respuesta.estado;
        }
      });
      accionModalEditarTarea();
      peticionGetFiltra();
    })
    .catch(error => {
      console.log(error);
    })
  }

  const peticionDelete = async() => {
    await axios.delete(baseUrl+"/"+tareaSeleccionada.idTarea)
    .then(response => {
      setData(data.filter(tarea => tarea.idTarea !== response.data.idTarea));
      accionModalEliminarTarea();
    })
    .catch(error => {
      console.log(error);
    })
  }

  const seleccionarTarea = (tarea, caso) => {
    setTareaSeleccionada(tarea);
    (caso === "Editar") ? accionModalEditarTarea() : accionModalEliminarTarea();
  }

  // -----------------------------------------------------------------------------
  // ---- MÉTODOS DE USO PARA LAS MODALES DEL CRUD
  // -----------------------------------------------------------------------------

  const accionModalInsertarTarea = () => {
    setModalInsertarTarea(!modalInsertarTarea);
  }

  const accionModalEditarTarea = () => {
    setModalEditarTarea(!modalEditarTarea);
  }

  const accionModalEliminarTarea = () => {
    setModalEliminarTarea(!modalEliminarTarea);
  }

  // -----------------------------------------------------------------------------
  // ---- MÉTODOS DE USO PARA EL FILTRADO Y ORDENAMIENTO DE LA TABLA
  // -----------------------------------------------------------------------------

  const seleccionarFiltro = (estado) => {
    filtroSeleccionado.Estado = estado;
    console.log(filtroSeleccionado);
    peticionGetFiltra();
  }

  const seleccionarEstado = (estado) => {
    tareaSeleccionada.Estado = estado;
    console.log(tareaSeleccionada);
  }

  const seleccionarOrden = (elemento) => {
    setOrden(!orden);
    filtroSeleccionado.Orden = (orden) ? "asc" : "desc";

    if(elemento === "Fecha"){
       filtroSeleccionado.Elemento = "fecha";
    }
    else if (elemento === "Nombre") {
      filtroSeleccionado.Elemento = "nombre";
    }

    console.log(filtroSeleccionado);
    peticionGetFiltra();
  }

  // -----------------------------------------------------------------------------
  // ---- MÉTODO PARA EL CIERRE DE SESIÓN
  // -----------------------------------------------------------------------------

  const cerrarSesion = () => {
    cookies.remove('idUsuario', {path: '/'});
    cookies.remove('nombreUsuario', {path: '/'});
    cookies.remove('nombre', {path: '/'});
    cookies.remove('password', {path: '/'});
    props.history.push('./');
  }

  // -----------------------------------------------------------------------------
  // ---- DEFINICIÓN DEL USE EFECT
  // -----------------------------------------------------------------------------

  useEffect(() => {
    if(!cookies.get('idUsuario')){
      props.history.push('./');
    }
    peticionGet();
  }, []);

  // -----------------------------------------------------------------------------
  // ---- CONTENIDO HTML
  // -----------------------------------------------------------------------------

  return (
    <div className="App">

      {/* ----------------------------------------------------------------------------- */}
      {/* ---- TÍTULO                                                                   */}
      {/* ----------------------------------------------------------------------------- */}
      <div className="border-primary bg-primary text-white MiTitulo col-xm-12 col-sm-10 col-md-8 col-lg-6">
        <h2><FaUserCircle /> {cookies.get('nombreUsuario')} : {cookies.get('nombre')} / Mis Tareas</h2>
      </div>

      {/* ----------------------------------------------------------------------------- */}
      {/* ---- ACCIONES SOBRE LA SESIÓN                                                 */}
      {/* ----------------------------------------------------------------------------- */}
      <div className="row">

        {/* ----------------------------------------------------------------------------- */}
        {/* ---- ACCIÓN: AGREGA TAREA                                                     */}
        {/* ----------------------------------------------------------------------------- */}
        <div className="col-sm-2">
          <button onClick={() => accionModalInsertarTarea()} className="btn btn-success MiBotonCircular"><FaPlus /> Nueva Tarea</button>
        </div>

        {/* ----------------------------------------------------------------------------- */}
        {/* ---- ACCIÓN: FILTRA POR ESTADO DE LA TAREA                                    */}
        {/* ----------------------------------------------------------------------------- */}
        <div className="col-sm-2">
          <select className="form-control col-md-5 MiBotonCircular" name="Estado" onChange={(event) => seleccionarFiltro(event.target.value)}  value={filtroSeleccionado.Estado}>
            <option value="todas">Todas</option>
            <option value="pendiente">Pendientes</option>
            <option value="completada">Completadas</option>
          </select>
        </div>

        {/* ----------------------------------------------------------------------------- */}
        {/* ---- ACCIÓN: CIERRA SESIÓN                                                    */}
        {/* ----------------------------------------------------------------------------- */}
        <div className="col-sm-8">
          <button className="btn btn-danger MiBotonCircular" onClick={() => cerrarSesion()}>Salir <FaSignOutAlt /></button>
        </div>
      </div>

      {/* ----------------------------------------------------------------------------- */}
      {/* ---- TABLA DE TAREAS                                                          */}
      {/* ----------------------------------------------------------------------------- */}
      <br /><br />
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>Id</th>
            <th>
              <span>
                Nombre <button className="btn btn-sm btn-dark MiBotonCircular" onClick={() => seleccionarOrden("Nombre")}><FaAngleDoubleDown /></button>
              </span>
            </th>
            <th>Descripción</th>
            <th>
              <span>
              Fecha de Creación <button className="btn btn-sm btn-dark MiBotonCircular" onClick={() => seleccionarOrden("Fecha")}><FaAngleDoubleDown /></button>
              </span>
            </th>
            <th>Estado</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
        {data.map(tarea => (
          <tr key = {tarea.idTarea}>
            <td>{tarea.idTarea}</td>
            <td>{tarea.nombreTarea}</td>
            <td>{tarea.descripcion}</td>
            <td>{(tarea.fechaCreacion).substr(0,19)}</td>
            <td>{tarea.estado}</td>
            <td>
              <button className="btn btn-sm btn-primary MiBotonCircular" onClick={() => seleccionarTarea(tarea, "Editar")}><FaEdit /></button> {" "}
              <button className="btn btn-sm btn-danger MiBotonCircular" onClick={() => seleccionarTarea(tarea, "Eliminar")}><FaTrashAlt /></button>
            </td>
          </tr>
        ))}
        </tbody>
      </table>

      {/* ----------------------------------------------------------------------------- */}
      {/* ---- MODAL: AGREGAR NUEVA TAREA                                               */}
      {/* ----------------------------------------------------------------------------- */}
      <Modal isOpen={modalInsertarTarea}>
        <ModalHeader>Agrega Nueva Tarea</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Nombre: </label>
            <br />
            <input type="text" className="form-control" name="nombreTarea" onChange={handleChange}/>
            <br/>
            <label>Descripción: </label>
            <br />
            <textarea className="form-control" name="descripcion" onChange={handleChange}></textarea>
            <br/>
            <label>Estado: </label>
            <br />
            <select className="form-control" name="estado" onChange={(event) => seleccionarEstado(event.target.value)}>
              <option value=""></option>
              <option value="pendiente">Pendiente</option>
              <option value="completada">Completada</option>
            </select>
            <br/>
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={() => peticionPost()}>Agregar</button>{" "}
          <button className="btn btn-danger" onClick={() => accionModalInsertarTarea()}>Cancelar</button>
        </ModalFooter>
      </Modal>

      {/* ----------------------------------------------------------------------------- */}
      {/* ---- MODAL: EDITAR TAREA                                                      */}
      {/* ----------------------------------------------------------------------------- */}
      <Modal isOpen={modalEditarTarea}>
        <ModalHeader>Edita Tarea</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>ID: </label>
            <br />
            <input type="text" className="form-control" readOnly value={tareaSeleccionada && tareaSeleccionada.idTarea}/>
            <br />
            <label>Nombre: </label>
            <br />
            <input type="text" className="form-control" name="nombreTarea" onChange={handleChange} value={tareaSeleccionada && tareaSeleccionada.nombreTarea}/>
            <br/>
            <label>Descripción: </label>
            <br />
            <textarea className="form-control" name="descripcion" onChange={handleChange} value={tareaSeleccionada && tareaSeleccionada.descripcion}></textarea>
            <br/>
            <label>Estado: </label>
            <br />
            <select className="form-control" name="estado" onChange={handleChange} value={tareaSeleccionada && tareaSeleccionada.estado}>
              <option value=""></option>
              <option value="pendiente">Pendiente</option>
              <option value="completada">Completada</option>
            </select>
            <br/>
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={() => peticionPut()}>Actualizar</button>{" "}
          <button className="btn btn-danger" onClick={() => accionModalEditarTarea()}>Cancelar</button>
        </ModalFooter>
      </Modal>

      {/* ----------------------------------------------------------------------------- */}
      {/* ---- MODAL: ELIMINAR TAREA                                                    */}
      {/* ----------------------------------------------------------------------------- */}
      <Modal isOpen={modalEliminarTarea}>
        <ModalBody>
          ¿Seguro que deseas eliminar la tarea {tareaSeleccionada && tareaSeleccionada.idTarea} : {tareaSeleccionada && tareaSeleccionada.nombreTarea}?
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-danger" onClick={() => peticionDelete()}>Sí</button>{" "}
          <button className="btn btn-secondary" onClick={() => accionModalEliminarTarea()}>No</button>
        </ModalFooter>
      </Modal>

    </div>
  );
}

// -----------------------------------------------------------------------------
// ---- EXPORTAR VISTA
// -----------------------------------------------------------------------------
export default Tareas;
