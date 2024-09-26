# Human Resource Management - API with Vue 3 Frontend

This project is a Human Resource Management system built using a split architecture. The backend is an API built with .NET 8 and Entity Framework Core, using SQLite as the database. The frontend is developed using Vue 3, Vite, and Tailwind CSS 3 to render assets. The system is designed to manage employees efficiently and includes a preloaded SQLite database file.

## Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [dotnet-ef](https://learn.microsoft.com/en-us/ef/core/cli/dotnet)

## Technology Stack

### Backend (API):
- **.NET 8**: Serves as the backend API.
- **Entity Framework Core**: For database access.
- **SQLite**: Lightweight database for data storage.

### Frontend:
- **Vue 3**: Modern JavaScript framework for building the user interface.
- **Vite**: Development environment and build tool.
- **Tailwind CSS 3**: Utility-first CSS framework for styling.

## Features
- Full employee management system.
- Preloaded SQLite database for testing and demo purposes.
- Backend API accessible for external clients or other frontend frameworks.

## Project Structure
- `InteliblueVueJsHrManagement.Client` - Frontend
  - This project uses npm. Ensure you have NPM installed on your PC. Visual Studio 2022 is automatically configured to run this project when running the solution.
  
- `InteliblueVueJsHrManagement.Server` - API
  - Visual Studio 2022 is automatically configured to run this project when running the solution.

### Migrations
The `Migrations` folder holds the database migration files that track changes to your database schema over time. Each migration represents a change, such as creating tables or modifying columns. Entity Framework Core uses these migrations to update the database schema to match the models in the code.

## Demo
Open `demo.gif` to see a demo of the application.

## To Run
1. Launch the solution in Visual Studio 2022.
2. Start the debugger using `https`.

---

## Adding/Updating/Removing Migrations

To manage database migrations, open Developer Powershell or a Windows Terminal inside the solution root directory:

```bash
# To add a new migration
dotnet ef migrations add <migration name>

# To remove the most recent migration 
dotnet ef migrations remove

# To apply migrations
dotnet ef database update
```
