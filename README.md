# Read ME

# Steps
1) Create models
2) Create a DB Context class (This can be done manually)
3) Run command to create migration and update database
```
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4) Scaffold Controllers
```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet aspnet-codegenerator controller -name {ControllerName} -async -api -m {ModelName} -dc {DBContext} -outDir Controllers
```
example:
```
dotnet aspnet-codegenerator controller -name UserController -async -api -m User -dc WeightRaceContext -outDir Controllers
```
5) Store secrets
```
dotnet user-secrets init
dotnet user-secrest set ConnectionStrings:{ConnectionName} "server={SERVER};user={USER};password={PASSWORD};database={DBNAME}"
```