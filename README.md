# Productivity Hub API 

## ASP .NET Core 8, SQL Server Express LocalDB (Integrada en Visual Studio 2022) y Entity Framework

## Requisitos previos

- .NET SDK 8
- Visual Studio

## Configuración

1. Clona este repositorio o descarga el código fuente.

2. Abre el proyecto en  Visual Studio 2022

## Base de datos
### Crear la base de datos

1. Abre la **Consola del Administrador de Paquetes** (Package Manager Console).

2. Aplica las migraciones a la base de datos:
````
Update-Database
````

### Limpiar la base de datos
Si deseas limpiar la base de datos y empezar desde cero:

1. Ejecuta el siguiente comando para eliminar todas las migraciones:

````
Remove-Migration -Force
````

2. Crea una nueva migración inicial:
````
Add-Migration Initial
````

3. Actualiza la base de datos:
````
Update-Database
````

### Eliminar la base de datos
Si deseas eliminar completamente la base de datos:

1. Ejecuta el siguiente comando para eliminar la base de datos:
````
Drop-Database
````

## Ejecución

1. Compila y ejecuta el proyecto.
2. Accede a la documentacion de Swagger en tu navegador.