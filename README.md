# elshaday

1º Configurar a connection string no projeto ElShaday.APi\appsettings.json

Caso não tenha o ef tools instalado, rodar o comando:
```shel
dotnet tool install --global dotnet-ef
```
Caso precise atualiza-lo:
```shel
dotnet tool update --global dotnet-ef
```

O projeto foi feito utilizando conseito de code-first, então após configurar a connection string, rodar os comandos:
```shel
dotnet ef --startup-project .\ElShaday.API\ --project .\ElShaday.Data\ migrations add CreateTables
```
```shel
dotnet ef --startup-project .\ElShaday.API\ --project .\ElShaday.Data\ database update
```
