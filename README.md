# IrisExam

Este repositorio cuenta con el examen para optar al cargo de fullstack enginner en iris neofinanciera.

Este Repositorio Cuenta con dos proyectos :

## 1) Proyecto Frontend (todo app)

.Alojado en el directorio UI de la raiz del repositorio
.Esta es una app realizada en angular 12

## Live server
El proyecto frontend se encuentra desplegado en un bucket de amazon S3 como sitio estatico `http://angular-iris.s3-website-us-east-1.amazonaws.com/` y conectado al backend correspondiente.

## Development server

Para correr el proyecto de forma local debe contar con angular en su versión 12 o susperior y node js en su versión
18 o superior

Primero restaure las dependencias usando `npm install`

Abra la carpeta y ejecute el comando `ng serve` para crear un servidor de desarrollo. Navegue a `http://localhost:4200/`.

## 2) Proyecto Back-End (todo api)

.Alojado en el directorio api de la raiz del repositorio
.Esta es una app realizada con asp.net core 6.0 
.Utilizando arquitectura limpia y orientada al dominio
.Guarda los logs en un bucket de S3
.Implementa CI/CD mediante Github Actions la cual construye una imagen de docker y la despliega en amazon apprunner.
.Cuenta con swagger UI alojado en `https://ztj7tcjien.us-east-1.awsapprunner.com/swagger/index.html`

## Anotaciones
El proyecto de backend utiliza una base de datos sql, la cual esta alojada remotamente en un servidor privado,
esto con el fin de facilitar la revisión del examen y evitar la creación de bases de datos.

## Live server
El proyecto backend se encuentra desplegado en una instancia de apprunner de amazon en el siguiente enlace
 `https://ztj7tcjien.us-east-1.awsapprunner.com` endpoint que se utiliza el frontend para consumirlo
## Development server

Para correr el proyecto de forma local debe contar con .net SDK en su versión 6.0

Utilizando Visual Studio abra la solución IrisExam.sln que se encuentra en el directorio api de la raiz del repositorio.

Fije como proyecto de inicio `Iris.Api` y procedar a ejecutarlo con visual studio



