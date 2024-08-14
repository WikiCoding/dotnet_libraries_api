# Project Scope Description

ASP.NET Core Learning project in monolithic architecture using:

1. Domain Driven Design
2. Clean Architecture
3. Repository Pattern
4. EntityFrameworkCore
5. MS SQL Server Database implementation with migrations
6. PostgreSQL Database implementation with migrations
7. MongoDb Database
8. Swagger
9. Docker
10. Ms Test
11. xUnit
12. Kafka
13. Consul
14. HttpClient (Books_API)

# NuGet Packages used:

1. Microsoft.EntityFrameworkCore
2. Microsoft.EntityFrameworkCore.Design
3. Microsoft.EntityFrameworkCore.Tools
4. Microsoft.EntityFrameworkCore.SQLServer
5. Npgsql.EntityFrameworkCore.PostgreSQL
6. MongoDB.EntityFrameworkCore
7. Confluent.Kafka
8. Steeltoe.Discovery.Consul
9. Swashbuckle.AspNetCore (Swagger)

# To run migrations at Package Manager Console:

```
Add-Migration <migration_name>
Update-Database
```

# To Start the Docker Container with Kafka

```
docker run -d --name broker -p 9092:9092 apache/kafka:latest
```

# To Start the Consul Container\*

```
docker run --name consul -d -p 8500:8500 hashicorp/consul:latest
```

\*The services will register themselves to Consul on that Docker instance but since for the moment the services are running locally, routing will not work. Solution is to create a Docker-Compose file and run all services in the same network.

# To be done: docker-compose file to containerize all the services
