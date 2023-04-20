## Sobre

Aplicação de uma Rede Social sobre Trilhas em C#, ASP.NET, banco de dados Azure e utilizando os conceitos MVC.

<br /><br />
MVC:

https://redesocialguiatrilhawebpb.azurewebsites.net/

<br /><br />
API :

https://redesocialguiatrilhaapipb.azurewebsites.net/api/post

https://redesocialguiatrilhaapipb.azurewebsites.net/api/userprofile


-------------------------------------------------------------------------------

##Abrindo em local host e Colocando o MVC como projeto de inicialização:<br /><br />
```sh
Add-Migration InitialMigration -context ApplicationDbContext  // update-database
```
<br /><br />
##Colocando o API como projeto de inicialização:<br /><br />
```sh
Add-Migration InitialMigration -context RedeSocialGuiaTrilhaDbContext -project RedeSocialGuiaTrilha.Data // update-database
```
