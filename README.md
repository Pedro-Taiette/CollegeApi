To get the project running, follow these steps to configure the database connection via `user-secrets`. This keeps sensitive data out of your source code and the Git repository.

### Quick Setup

1. **Navigate to the API project folder** (where the `.csproj` or `Program.cs` is located):
```bash
cd College.WebApi

```


2. **Initialize User Secrets** (if not already done):
```bash
dotnet user-secrets init

```


3. **Set your Connection String**:
Copy and paste the command below, ensuring you use a single backslash (`\`) for the server path:
```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=(localdb)\MSSQLLocalDB;Database=CollegeDb;Trusted_Connection=True;MultipleActiveResultSets=true"

```


4. **Verify the configuration**:
```bash
dotnet user-secrets list

```


*You should see `ConnectionStrings:DefaultConnection` listed with your value.*

---

### Why this works

The .NET `WebApplication.CreateBuilder` automatically loads configuration in this priority order:

1. `appsettings.json` (Base configuration)
2. `appsettings.{Environment}.json` (Environment-specific)
3. **User Secrets** (Local overrides — **Highest priority in Development**)
4. Environment Variables

By setting the value in `user-secrets`, you override any settings in your JSON files without needing to modify them or risk committing your local database path to Git.

**Note:** If you still have a `ConnectionStrings` section in your `appsettings.Development.json` file, **delete it or comment it out**. This ensures the application forces the use of your local `user-secrets` configuration.
