# Project Scope Description
ASP.NET Core Learning project in monolithic architecture using:
1. Domain Driven Design
2. Clean Architecture
3. Repository Pattern
4. EntityFrameworkCore
5. MS SQL Server Database implementation with migrations
6. PostgreSQL Database implementation with migrations
7. Swagger
8. Docker
9. Ms Test

# NuGet Packages used:
1. Microsoft.EntityFrameworkCore
2. Microsoft.EntityFrameworkCore.Design
3. Microsoft.EntityFrameworkCore.Tools
4. Microsoft.EntityFrameworkCore.SQLServer
5. Npgsql.EntityFrameworkCore.PostgreSQL

# To run migrations at Package Manager Console:
```
Add-Migration <migration_name>
Update-Database
```

# To Start the Docker Container with Kafka
```
docker run -d --name broker -p 9092:9092 apache/kafka:latest
```