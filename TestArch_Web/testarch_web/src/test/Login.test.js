import React from 'react'
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, render, screen, } from '@testing-library/react';
import { prettyDOM } from '@testing-library/dom';
import Login from '../pages/Login';

test('Renderizado del Login', () => {
  const component = render(<Login />);
  //console.log(prettyDOM(component[0]));
});

test('Renderizar Título del Desarrollador', () => {
  const component = render(<Login />);
  const titulo = screen.getByText(/Fase 2: Giovanni de la Vega Huerta/i);
  const label = component.container.querySelector('label');
  expect(titulo).toBeInTheDocument();
});

test('Un Click del Iniciar Sesión', () => {
  const mockHandler = jest.fn();
  const component = render(<Login tooggleImportance={mockHandler} />);
  const button = component.getByText('Iniciar Sesión');
  console.log(prettyDOM(button));
  fireEvent.click(button);
  expect(mockHandler).toHaveBeenCalledTimes(0);
});




// import { render, screen } from '@testing-library/react';
// import App from '../pages/App';
//
// test('renders learn react link', () => {
//   render(<App />);
//   const linkElement = screen.getByText(/learn react/i);
//   expect(linkElement).toBeInTheDocument();
// });
