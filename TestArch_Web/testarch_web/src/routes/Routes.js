// -----------------------------------------------------------------------------
// ---- DEPENDENCIAS
// -----------------------------------------------------------------------------
import React from 'react';
import {BrowserRouter, Switch, Route} from 'react-router-dom';
import Login from '../pages/Login';
import AddUser from '../pages/AddUser';
import Tareas from '../pages/Tareas';

// -----------------------------------------------------------------------------
// ---- SISTEMA DE ENRUTAMIENTO
// -----------------------------------------------------------------------------
function App() {
  return (
    <BrowserRouter>
      <Switch>
        <Route exact path="/" component={Login}/>
        <Route exact path="/registro" component={AddUser}/>
        <Route exact path="/tareas" component={Tareas}/>
      </Switch>
    </BrowserRouter>
  );
}

// -----------------------------------------------------------------------------
// ---- EXPORTAR RUTA
// -----------------------------------------------------------------------------
export default App;
