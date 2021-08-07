## NuGet Packages

### Entity Framework Core
```text
_> dotnet tool install --global dotnet-ef
```

#### EF Command

- Add Migrations

```text
_> dotnet ef migrations add "註解字串"
```

- Update DataBase

```text
_> dotnet ef database update
``` 

### Develop Packages

```text
_> dotnet add package Microsoft.EntityFrameworkCore.Sqlite
_> dotnet add package Microsoft.EntityFrameworkCore.Tools
```