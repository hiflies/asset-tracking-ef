# Asset Tracking

A console-based asset tracking application built with **C#** and **Entity Framework**, using **SQL Server** as the database backend. The app provides an interactive terminal UI powered by [Spectre.Console](https://spectreconsole.net/) for managing and searching assets across offices.

## Features

- Add new assets
- View all assets
- Update existing assets
- Delete assets
- Search assets
- View assets grouped by office
- Interactive menu-driven interface via Spectre.Console

## Project Structure

```
asset-tracking-ef/
├── Menu/           # Menu definitions (Add, Show, Update, Delete, Search, Office, Exit)
├── Migrations/     # EF Core database migrations
├── Models/         # Entity models
├── Services/       # Database context and service layer
├── Utils/          # Console helpers and utilities
├── Program.cs      # Application entry point
└── appsettings.json
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) or later
- SQL Server (local or remote)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/hiflies/asset-tracking-ef.git
cd asset-tracking-ef
```

### 2. Set up the database

Create a database called `asset_tracking` on your SQL Server instance.

```sql
CREATE DATABASE asset_tracking;
```

### 3. Configure the connection string

Open `appsettings.json` and update the connection string to match your environment:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost;Initial Catalog=asset_tracking;Integrated Security=False;User ID=SA;Password=YOUR_PASSWORD;MultipleActiveResultSets=True;TrustServerCertificate=True;"
  }
}
```

### 4. Apply migrations

```bash
dotnet ef database update
```

### 5. Run the application

```bash
dotnet run
```

## Usage

Once running, you will be presented with an interactive menu:

```
What would you like to do?
> Add Asset
  Show All Assets
  Update Asset
  Delete Asset
  Search Asset
  Show Office
  Exit
```

Use the arrow keys to navigate and press **Enter** to select an option.

## Tech Stack

- **C# / .NET** — Application language and runtime
- **Entity Framework Core** — ORM for database access
- **SQL Server** — Relational database
- **Spectre.Console** — Rich terminal UI
- **Microsoft.Extensions.Configuration** — `appsettings.json` configuration support