import React from 'react'
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, render, screen, } from '@testing-library/react';
import { prettyDOM } from '@testing-library/dom';
import AddUser from '../pages/AddUser';

test('Renderizado del Formulario de Registro', () => {
  const component = render(<AddUser />);
  //console.log(prettyDOM(component[0]));
});

test('Renderizar Título', () => {
  const component = render(<AddUser />);
  const titulo = screen.getByText(/Registrate/i);
  expect(titulo).toBeInTheDocument();
});

test('Un Click Para Inicio de Sesión', () => {
  const mockHandler = jest.fn();
  const component = render(<AddUser tooggleImportance={mockHandler} />);
  const button = component.getByText('Registrar');
  fireEvent.click(button);
  expect(mockHandler).toHaveBeenCalledTimes(0);
});
