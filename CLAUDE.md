# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

# Requirements

I want to create kanban board. Here're requirements:
- The user can create lists and assign tasks on the Kanban board
- The user can see changes on the Kanban board in near real-time
- The user can make offline changes on the Kanban board
- The Kanban board is distributed
- The primary entities are the boards, the lists, and the tasks tables
- The relationship between the boards and lists tables is 1-to-many
- The relationship between the lists and tasks tables is 1-to-many
- An additional table is created to track the versions of the board
- Document store such as MongoDB persists the metadata of the tasks for improved denormalization support (ability to query and index subfields of the document)
- The transient data (board activity level) is stored on the cache server such as Redis
- The download queue for board activity is implemented using a message queue such as Apache Kafka
- The changes on the Kanban board are synchronized in real-time through a WebSocket connection
- The offline client synchronizes the delta of changes when connectivity is reacquired

## Development Commands

### Database Management
- Start PostgreSQL database: `docker-compose up -d`
- Stop database: `docker-compose down`
- Stop and remove data: `docker-compose down -v`
- Check database status: `docker-compose ps`
- View database logs: `docker-compose logs postgres`

### .NET Development
- Run the application: `cd server/Kanban.API && dotnet run`
- Restore EF Core tools: `dotnet tool restore`
- Apply database migrations: `cd server/Kanban.API && dotnet ef database update`
- Create new migration: `cd server/Kanban.API && dotnet ef migrations add <MigrationName>`
- List migrations: `cd server/Kanban.API && dotnet ef migrations list`
- Remove last migration: `cd server/Kanban.API && dotnet ef migrations remove`
- Reset database: `cd server/Kanban.API && dotnet ef database drop && dotnet ef database update`

### Backend (.NET 9)
- **Framework**: ASP.NET Core 9
- **ORM**: Entity Framework Core 9
- **Database**: PostgreSQL 17 (primary), MongoDB 7.0 (documents/logs)
- **Caching**: Redis 8.0.3
- **Authentication**: ASP.NET Core Identity + JWT
- **API Documentation**: Swagger/OpenAPI

### When providing code assistance:
- **Follow our patterns**: Use the established service/repository patterns shown above
- **Authentication**: Implement passwordless flows with magic links and WebAuthn
- **Database operations**: Always use async EF Core methods with proper error handling
- **API responses**: Use the ` Kanban.API.Common.Result<T>` wrapper for consistency
- **Vue components**: Use Composition API with TypeScript interfaces
- **State management**: Utilize Pinia stores for shared state
- **Security**: Never store passwords, use secure token generation, implement rate limiting

