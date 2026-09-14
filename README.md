# Clean Architecture Template - ASP.NET Core

A reference **Clean Architecture** template for ASP.NET Core APIs, built with .NET-10. Companion project for the *"ASP.NET Core Clean Architecture"* YouTube series.

<!-- Status -->
[![CI](https://github.com/gitEugeneL/Healthcare-CRM/actions/workflows/dotnet.yml/badge.svg)](https://github.com/gitEugeneL/CleanArchitectureTemplate/actions/workflows/dotnet.yml)

<!-- Stack -->
![.NET](https://img.shields.io/badge/.NET-512BD4?logo=dotnet)
![C%23](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![xUnit](https://img.shields.io/badge/xUnit-5E2D8C)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white)
![Redis](https://img.shields.io/badge/Redis-DC382D?logo=redis&logoColor=white)
![Docker](https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=white)

<!-- Architecture & Patterns -->
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-9cf)
![CQRS](https://img.shields.io/badge/CQRS-blueviolet)
![MediatR](https://img.shields.io/badge/MediatR-512BD4)
![Minimal API](https://img.shields.io/badge/MinimalApi-6A1B9A)
![Integration Tests](https://img.shields.io/badge/Integration%20Tests-green)

## 📺 YouTube series

This repository is built step YouTube series on Clean Architecture with ASP.NET Core:

[ASP.NET Core Clean Architecture - YouTube playlist](https://www.youtube.com/watch?v=449sEaIY_5g&list=PLl71qo2x5aNKfQL5K_sp67BVbcotAlsSd)

| # | Episode                                                                                                                                                         |
|---|-----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| 1 | [Domain Layer and Domain Model from Scratch](https://www.youtube.com/watch?v=449sEaIY_5g&list=PLl71qo2x5aNKfQL5K_sp67BVbcotAlsSd&index=1)                       |
| 2 | [Designing Persistence, Setting Up EF Core PostgreSQL](https://www.youtube.com/watch?v=CM3AJDAxTV8&list=PLl71qo2x5aNKfQL5K_sp67BVbcotAlsSd&index=2)             |
| 3 | [Application-слой, CQRS + MediatR](https://www.youtube.com/watch?v=CM3AJDAxTV8&list=PLl71qo2x5aNKfQL5K_sp67BVbcotAlsSd&index=2)                                 |
| 4 | [Presentation Layer, Web API with Minimal API Endpoints](https://www.youtube.com/watch?v=SrM_cUT1NGQ&list=PLl71qo2x5aNKfQL5K_sp67BVbcotAlsSd&index=4)           |
| 5 | [Integration Testing with Testcontainers](https://www.youtube.com/watch?v=noUXnMzFwO8&list=PLl71qo2x5aNKfQL5K_sp67BVbcotAlsSd&index=5)                          |
| 6 | [CQRS, FluentValidation, and Integration Tests](https://www.youtube.com/watch?v=qJ9nCzJqusg&list=PLl71qo2x5aNKfQL5K_sp67BVbcotAlsSd&index=6)                    |
| 7 | [Pagination in CQRS Queries from Minimal API to Integration Tests](https://www.youtube.com/watch?v=SdqnpmWT93k&list=PLl71qo2x5aNKfQL5K_sp67BVbcotAlsSd&index=7) |
| 8 | [Implementing Redis for Caching](https://www.youtube.com/watch?v=Ul2EXvc1R28&list=PLl71qo2x5aNKfQL5K_sp67BVbcotAlsSd&index=8)                                   |
| 9 | [Implementing Hangfire and Redis for Background Jobs](https://www.youtube.com/watch?v=80JqqF1yCrw&list=PLl71qo2x5aNKfQL5K_sp67BVbcotAlsSd&index=9)              |
| 10 | [ASP.NET Core Clean Architecture full course]()                                                                                                                 |

## Tech stack

- [.NET](https://github.com/dotnet/core)
- [C#](https://github.com/dotnet/csharplang)
- [xUnit](https://github.com/xunit/xunit)
- [ASP.NET Core](https://github.com/dotnet/aspnetcore)
- [MediatR](https://github.com/jbogard/MediatR)
- [Redis](https://github.com/redis/redis)
- [Entity Framework Core](https://github.com/dotnet/efcore)
- [PostgreSQL](https://github.com/postgres)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)
- [Docker](https://github.com/docker)
- [Bogus](https://github.com/bchavez/Bogus)

##  List of Docker Containers
- **database** - PostgreSQL database container for persistent data storage
- **redis** - Redis in-memory cache database container
- **redis-commander** - Web-based Redis management interface for cache monitoring and debugging

##  How to run tests

*Allows you to run all integration and unit tests.*

   ```sh
   > dotnet test  # dotnet SKD is required
   ```

##  Infrastructure

***Make*** commands work on Linux/macOS. Alternatively, Docker Compose can be used.


| Action               | Make                                   | Command / Equivalent                                                                                                              |
|----------------------|----------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------|
| **Start & Build**    | `make up`                              | `docker compose -f docker-compose.yml up -d --build`                                                                              |
| **Stop**             | `make down`                            | `docker compose -f docker-compose.yml down`                                                                                       |
| **Stop & Clean**     | `make down-and-clean`                  | `docker compose down -v`                                                                                                          |
| **Create Migration** | `make db-migrate name=<MigrationName>` | `dotnet ef migrations add <Name> --project Infrastructure/Persistence --startup-project Presentation/Api --output-dir Migrations` |
| **Apply Migrations** | `make db-update`                       | `dotnet ef database update --project Infrastructure/Persistence --startup-project Presentation/Api`                               |



## License

No license file yet - but feel free to reuse, fork, or learn from this code. If you build something with it, I'd love to hear about it!