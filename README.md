# WinBoard

A Kanban board application built with ASP.NET Core and PostgreSQL.

## Prerequisites

- [Docker](https://www.docker.com/get-started) and [Docker Compose](https://docs.docker.com/compose/install/)
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) (for running the API)

## Database Setup

This project uses PostgreSQL as the database, which can be easily run using Docker Compose.

### Running the Database

1. **Start the PostgreSQL database:**
   ```bash
   docker-compose up -d
   ```
   This command will:
   - Pull the PostgreSQL 17 image if not already present
   - Create and start a PostgreSQL container
   - Set up the database with the following credentials:
     - Database: `KanbanDb`
     - Username: `postgres`
     - Password: `postgres`
     - Port: `5432`

2. **Verify the database is running:**
   ```bash
   docker-compose ps
   ```
   You should see the postgres service with status "Up".

3. **Check database health:**
   ```bash
   docker-compose logs postgres
   ```

### Stopping the Database

To stop the database:
```bash
docker-compose down
```

To stop the database and remove all data:
```bash
docker-compose down -v
```

### Database Configuration

The database configuration is defined in `docker-compose.yml`:
- **Image**: PostgreSQL 17
- **Port**: 5432 (mapped to host)
- **Data Persistence**: Uses a named volume `postgres_data`
- **Health Check**: Automatic health monitoring
- **Restart Policy**: `unless-stopped`

### Connection String

The application connects to the database using the connection string defined in `appsettings.json`. The default connection string expects:
- Host: `localhost`
- Port: `5432`
- Database: `KanbanDb`
- Username: `postgres`
- Password: `postgres`

## Running the Application

1. **Start the database** (as described above)
2. **Navigate to the API project:**
   ```bash
   cd server/Kanban.API
   ```
3. **Run the application:**
   ```bash
   dotnet run
   ```

The API will be available at:
- HTTPS: `https://localhost:7070`
- HTTP: `http://localhost:5280`
- Swagger UI: `https://localhost:7070/swagger`
- ReDoc: `https://localhost:7070/redoc`

## Database Migrations

The project uses Entity Framework Core migrations to manage database schema changes.

### Prerequisites for EF Commands

Make sure you have the Entity Framework Core tools installed:
```bash
dotnet tool restore 
```

### Applying Migrations

1. **Ensure the database is running** (using docker-compose as described above)

2. **Navigate to the API project:**
   ```bash
   cd server/Kanban.API
   ```

3. **Apply migrations to the database:**
   ```bash
   dotnet ef database update
   ```
   This will apply all pending migrations to the database.

### Managing Migrations

**View migration status:**
```bash
dotnet ef migrations list
```

**Create a new migration:**
```bash
dotnet ef migrations add <MigrationName>
```

**Remove the last migration (if not applied to database):**
```bash
dotnet ef migrations remove
```

**Reset database (remove all data and reapply migrations):**
```bash
dotnet ef database drop
dotnet ef database update
```

### Initial Setup

For a fresh database setup:
1. Start the PostgreSQL database with docker-compose
2. Apply the initial migration:
   ```bash
   cd server/Kanban.API
   dotnet ef database update
   ```

This will create the database schema with the initial migration (`20250802214811_Init`) that sets up the basic tables for the Kanban application.
