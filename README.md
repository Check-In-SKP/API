# CheckInSKP

## Project Description
CheckInSKP is a monitoring system designed to keep track of student arrivals and departures from the work area.
The student uses their student card along with a pin to login from a stand where it promtly provides the user to check in/out.

(An app is currently in the works where the user can store their student card on their mobile unit and use NFC to login see [SKPIdentityCore](https://github.com/Check-In-SKP/SKPIdentityCore) for progress)

## Overview
This repository is the core of CheckInSKP, structured with a focus on Domain-Driven Design (DDD). It includes our API and database components.

## Building the Project 
Currently working on simplifying the setup process but for now this guide will walk you through setting up and building the CheckInSKP project on your local machine.

### Prerequisites
Ensure you have the following prerequisites installed:

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) 
- [PostgreSQL](https://www.postgresql.org/download/)

### Step 1: Setup appsettings.json
1. Navigate to the API project.
2. Create a configuration file named appsettings.[YourEnvironment].json (e.g. appsettings.PC-NAME.json).
3. Populate the file with the following JSON structure and customize it with your specific settings:
```json
{
  "ConnectionStrings": {
    "CheckInDB": "Host=localhost;Database=CheckInDB;Username=[YourUsername];Password=[YourPassword];Port=5432"
  },

  "JwtSettings": {
    "Key": "[YourVeryLongSecretKey(minimum 32 char)]",
    "Issuer": "CheckInSKP",
    "Audience": "CheckInAPI_Users"
  }
}
```

### Step 2: Create the Database
1. Launch PostgreSQL.
2. Create a new database named 'CheckInDB'.

### Step 3: Run Entity Framework Commands
Execute these Entity Framework commands using the .NET CLI or the Package Manager Console in Visual Studio:

1. build the project:
```
dotnet build
```

Add the initial migration:
```
dotnet ef migrations add InitialCreate --project src/Infrastructure --startup-project src/API --output-dir Data/Migrations
```

Update the database:
```
dotnet ef database update InitialCreate --project src/Infrastructure --startup-project src/API
```

### Step 3: Start the project
1. Set the API as the single startup project in your development environment.
2. Run the API!
```
dotnet run --project src/API
```

### Notes:
After successfully creating the database, an administrative user account is automatically generated. (Remember to change the default password after the initial login)
- Username:&emsp;'sysadmin'
- Password:&emsp;'Kode1234!'

You can use this user to access security restricted endpoints.
