
# EVALUACIÓN TÉCNICA NUXIBA #

Prueba: **ARQUITECTO DE SOFTWARE**

Deadline: **1 día**

Nombre: Giovanni de la Vega Huerta

------
## Clona y crea tu repositorio para la evaluación ##
* Clona este repositorio en tu máquina local
* Crear un repositorio público en tu cuenta personal de GitHub, BitBucket o Gitlab
* Cambia el origen remoto para que apunte al repositorio público que acabas crear en tu cuenta
* Coloca tu nombre en este archivo README.md y realiza un push al repositorio remoto *(este paso es importante, nos ayuda a saber la hora en la que iniciaste tu examen)*

------
## Prueba ##
* La prueba consistirá en construir una API REST (basada en .NET) y una aplicación web (ocupando algún framework de JavaScript), que solucionen los siguientes puntos:

	* Un formulario de registro de usuario
		* El usuario debe de contemplar al menos estas propiedades:
			* Nombre de usuario
			* Contraseña
			* Nombre
	* Un formulario de inicio de sesión.
	* Una vez que el usuario esté registrado debe de poder iniciar sesión para poder realizar los siguientes puntos:
		* Crear tareas con las siguientes propiedades:
			* Nombre de tarea
			* Descripción
			* Fecha de creación
			* Estado de la tarea (pendiente o completada)
		* Poder realizar actualizaciones a las propiedades necesarias de las tareas
		* Mostrar una lista de las tareas creadas, y dentro de esta lista poder realizar lo siguiente:
			* Ordenar las tareas por fecha de creación
			* Ordenar las tareas por nombre
			* Filtrar las tareas por estado (pendiente o completada)
		* Poder eliminar tareas

> *Nota: Para realizar esta prueba es necesario tener una instancia de [SQL Server Developer o SQL Server Express](https://www.microsoft.com/es-mx/sql-server/sql-server-downloads) en tu equipo para poder guardar usuarios y tareas, y compartir en el repositorio los scripts de creación del esquema de la base de datos.*

* Para la solución de esta prueba es necesario tomar en cuenta:
	* Uso de principios SOLID
	* Uso de patrones de diseño de software
	* Pruebas unitarias en la aplicación web y el API REST
	* Uso de un ORM (opcional)
	* Uso de algún framework CSS (opcional)
	* De ser necesario, agregar la documentación que se necesite para ejecutar las aplicaciones (opcional)


**La estructura del repositorio debe de tener tres directorios en la raíz, aparte de este README.MD, un directorio para la aplicación web, otro para el API REST y un último para los scripts de la base de datos.**

------
### Realiza el push del código de tus pruebas y compártenos el link a tu repositorio remoto 😊

------
Si tienes alguna duda sobre la evaluación puedes mandar un correo electrónico a [desarrollo@nuxiba.com](mailto:desarrollo@nuxiba.com?subject=[Evaluación para el puesto de Arquitecto de Software]%20Duda%20sobre%20evaluación%20técnica)

Manda la liga de tu repositorio público a [Verónica Llerenas](mailto:vllerenas@nuxiba.com?subject=[EvaluaciónDesarrollo]%20Este%20es%20mi%20repositorio)
