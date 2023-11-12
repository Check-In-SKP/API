# CheckInSKP

## Overview
This repository is the core of CheckInSKP, structured with a focus on Domain-Driven Design (DDD). It includes our API and database components.

## Building the Project 
This guide will walk you through setting up and building the CheckInSKP project on your local machine.

### Prerequisites
Ensure you have the following prerequisites installed:

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) 
- [PostgreSQL](https://www.postgresql.org/download/)

### Step 1: Setup appsettings.json
Navigate to the project's root directory and create a configuration file named 'appsettings.[YourEnvironment].json' (e.g., 'appsettings.PcName.json'). Populate this file with the following structure:
```json
{
  "HttpClientSettings": {

  },

  "ConnectionStrings": {
    "CheckInDB": "Host=localhost;Database=CheckInDB;Username=[YourUsername];Password=[YourPassword];Port=5432"
  },

  "JwtSettings": {
    "Key": "[YourJwtKey]",
    "Issuer": "CheckInSKP",
    "Audience": "CheckInAPI_Users"
  }
}
```
Replace the placeholders with your specific settings.

### Step 2: Create the Database
Open PostgreSQL and create a new database named 'CheckInDB'.

Step 3: Run Entity Framework Commands
In the .NET CLI or the Package Manager Console in Visual Studio, run the following Entity Framework commands to set up the database:
```bash
dotnet build
dotnet ef migrations add InitialCreate --project src/Infrastructure --startup-project src/API --output-dir Data/Migrations
dotnet ef database update InitialCreate --project src/Infrastructure --startup-project src/API
```
### Step 4 (Final): Start the project
Configure your startup settings to run the API as a single startup project.
Now you're ready to run the API.
```dotnet run --project src/API```
