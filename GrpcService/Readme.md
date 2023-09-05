```
docker pull postgres
docker run -e POSTGRES_PASSWORD=mysecretpassword -p 5432:5432 postgres
```

```
dotnet tool install -g dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```