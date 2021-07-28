import React from 'react'
import '@testing-library/jest-dom/extend-expect';
import { fireEvent, render, screen, } from '@testing-library/react';
import { prettyDOM } from '@testing-library/dom';
import Tareas from '../pages/Tareas';

test('Renderizado', () => {
  const component = render(<Tareas />);
  //console.log(prettyDOM(component[0]));
});

test('Renderizar Título', () => {
  const component = render(<Tareas />);
  const titulo = screen.getByText(/Mis Tareas/i);
  expect(titulo).toBeInTheDocument();
});

test('Un Click Para Inicio de Sesión', () => {
  const mockHandler = jest.fn();
  const component = render(<Tareas tooggleImportance={mockHandler} />);
  const button = component.getByText('Nueva Tarea');
  fireEvent.click(button);
  expect(mockHandler).toHaveBeenCalledTimes(0);
});
