-- --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- CREACIÓN E INSERT DE BASE, TABLAS Y DATOS
-- --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
DROP DATABASE IF EXISTS TestArchGiovanniVH;
CREATE DATABASE TestArchGiovanniVH;

USE TestArchGiovanniVH;

CREATE TABLE usuarios  (
  idUsuario int NOT NULL IDENTITY PRIMARY KEY,
  nombreUsuario varchar(50) NOT NULL,
  password varchar(50) NOT NULL,
  nombre varchar(150) NOT NULL
);

CREATE TABLE tareas  (
  idTarea int NOT NULL IDENTITY PRIMARY KEY,
  idUsuario int NOT NULL,
  nombreTarea varchar(50) NOT NULL,
  descripcion text NOT NULL,
  fechaCreacion dateTime NOT NULL DEFAULT(GETDATE()),
  estado varchar(20) NOT NULL
);
