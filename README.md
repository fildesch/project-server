# Server

Simple ASP.NET Core Web API for managing projects and expenses with MongoDB.

## Features

- `GET /api/projects` — retrieve all projects
- `POST /api/projects` — create a new project
- `GET /api/projects/{id}` — get project details including expenses, total spent, and remaining budget
- `POST /api/projects/{projectId}/expenses` — add an expense to a project

## Requirements

- .NET 9 SDK
- MongoDB instance

The app includes CORS support for `http://localhost:4200`.

## Run

From the `Server` folder:

```powershell
dotnet run
```

## Notes

- Swagger is enabled in development.
- Project creation validates name, budget, dates, and status.
- Expense creation validates project existence, category, amount, and dates.
