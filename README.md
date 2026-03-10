# Database Setup & Migrations

This project uses **.NET User Secrets** to manage the database connection locally without exposing sensitive information in the repository.

## Configure Database Connection

Follow the steps below to configure your local database connection.

### 1. Navigate to the API project folder

Go to the folder where the `.csproj` or `Program.cs` file is located.

```bash
cd College.WebApi
```

### 2. Initialize User Secrets

If the project does not already have User Secrets enabled, run:

```bash
dotnet user-secrets init
```

### 3. Configure the Connection String

Run the command below to set your local database connection.

> ⚠️ Use a **single backslash (`\`)** in the server path.

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\MSSQLLocalDB;Database=CollegeDb;Trusted_Connection=True;MultipleActiveResultSets=true"
```

### 4. Verify the Configuration

You can confirm that the secret was saved correctly by running:

```bash
dotnet user-secrets list
```

You should see something like:

```
ConnectionStrings:DefaultConnection = Server=(localdb)\MSSQLLocalDB;Database=CollegeDb;Trusted_Connection=True;MultipleActiveResultSets=true
```

---

# Configuration Priority in .NET

`WebApplication.CreateBuilder` loads configuration in the following order:

1. `appsettings.json` — Base configuration
2. `appsettings.{Environment}.json` — Environment-specific configuration
3. **User Secrets** — Local development overrides
4. Environment Variables — Highest priority in production environments

Using **User Secrets** ensures that your **local database configuration is not committed to Git**.

> ⚠️ If `ConnectionStrings` exists in `appsettings.Development.json`, remove or comment it out to avoid conflicts.

---

# Reset and Recreate Database Migrations

If you need to **reset the database and recreate migrations from scratch**, follow these steps.

### 1. Revert the database to the state before any migrations

```bash
dotnet ef database update 0 --project College.Infrastructure --startup-project College.WebApi
```

### 2. Remove the last migration

```bash
dotnet ef migrations remove --project College.Infrastructure --startup-project College.WebApi
```

### 3. Create a new migration

```bash
dotnet ef migrations add InitialCreate --project College.Infrastructure --startup-project College.WebApi
```

### 4. Apply the migration to the database

```bash
dotnet ef database update --project College.Infrastructure --startup-project College.WebApi
```

---
