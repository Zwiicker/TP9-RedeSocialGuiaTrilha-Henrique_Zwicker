# TP9-RedeSocialGuiaTrilha-Henrique_Zwicker
MVC:

https://redesocialguiatrilhawebpb.azurewebsites.net/


API :

https://redesocialguiatrilhaapipb.azurewebsites.net/api/post

https://redesocialguiatrilhaapipb.azurewebsites.net/api/userprofile



Documento TP9 : https://drive.google.com/file/d/142q8_EQmqkbmibDcdsAzkylKWpGHNN7C/view?usp=sharing

Vídeo Explicativo: https://drive.google.com/file/d/14uGFlYeYa6VBICKK2SsjC27iPZ7Mvlhn/view?usp=sharing


-------------------------------------------------------------------------------

Abrindo em local host:

Colocando o MVC como projeto de inicialização:

Add-Migration InitialMigration -context ApplicationDbContext  // update-database

Colocando o API como projeto de inicialização:

Add-Migration InitialMigration -context RedeSocialGuiaTrilhaDbContext -project RedeSocialGuiaTrilha.Data // update-database
