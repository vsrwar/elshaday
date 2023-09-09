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
Realizar o seguinte include no banco:
```sql
INSERT INTO Users (Email, NickName, Active, PasswordHash, PasswordSalt, Role, CreatedAt, UpdatedAt, DeletedAt)
VALUES ('admin@elshaday.com', 'Admin', 1, '123', CONVERT(varbinary, '123'), 1, GETUTCDATE(), NULL, NULL);
```

Realizar os passos de "Forgot password?" para definir a senha de acesso do usuário "Admin".