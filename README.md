# GrowthHormone
Proyecto de Ciclo 3 - MinTic &amp; U. Caldas

## Integrantes

WebApp desarrollada en .net 6.0, con arquitectura MVC.

## Cómo usar?

Una vez que clones el repositorio, debes:

> Editar la ApplContext -> conectionData con la línea de conexión apropiada y tus credenciales, según tengas tus instancia de SQL Server en tu propio PC


> Compilar la capa de Dominio (dotnet run)


> Compilar la capa de Persistencia (dotnet run)


> Compilar la capa de Consola (dotnet run)


> Realizar las migraciones en la capa de persistencia 


> Aplicar dotnet ef database update en la capa de persistencia


> Ejecutar capa de Frontend (dotnet run)

## GrowthHormone Docs

> En este forder encontrarás información de todo el desarrollo de la app (modelos, descripción del proyecto, diagramas, scripts de prueba)

## GrowthHormone.Consola

> Si ejecutas la capa de Consola (dotnet run) ejecutarás las pruebas unitarias establecidas para la app. En el menú hay 6 opciones. Hay una 7 opción (escondida) que si la ejecutas, creará usuarios masivamente (pacientes)
