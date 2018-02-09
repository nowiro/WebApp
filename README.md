# API & UI Frameworks comparison
* Visual Studio 2012 for API and UI.AngularJS
** Ready to work on the Windows Server 2012 with IE 10 engine
** .NET Framework 4.5
* Visual Studio 2017 or Core with SDK for other UI projects
** .NET Core 2

## API
* ASP.NET Web API 2
* Url: http://localhost:56011/swagger

## UI.AngularJS - ASP.NET Core
* LESS suppport (without Autoprefixer - polyfill issue; works with Msie Engine >= 11)
* Url: http://localhost:56010/

## UI.Angular - ASP.NET Core
* N/A

## UI.React - ASP.NET Core
* N/A

## MS SQL 
* Restore database form the project path: DB/DB_Backup/WebApp.bak
*
* or
*
* Create Database "WebApp"
* Create Table "Product" form the project path: DB/DB_CustomScripts/SQLQuery_DropCreate.sql
* Insert custom data to the Table "Product" form project path: DB/DB_CustomScripts/SQLQuery_InsertValues.sql
* Count items from the "Product" table: DB/DB_CustomScripts/SQLQuery_CountItems.sql

# How to 
* Run both API and UI apps in the Visual Studio
* Verify in the SwaggerUI data for GET http protocol http://localhost:56011/swagger
* Open UI app based on AngularJS and browse data list http://localhost:56010/