# BMTDb

- This is a basic movie database written in C# using the ASP.NET Core 6.
- The project follows a layered architecture, Code First approach, and incorporates modern software development principles such as Repository Pattern and Unit of Work Pattern.

## Features

- Users can view a list of movies.
- Users can search for movies by title, genre, etc.
- Users can sort movies by dates.
- Users can view details about a specific movie.
- Users can add new movies.
- Recently visited movie titles by users are tracked and displayed on the profile page.
- Movies can be added from The Movie Database (TMDB) using API calls.
- Users receive in-site notifications for actions like login, logout, and movie additions.
- Signup confirmation emails are sent to users upon registration using.
  
  ### Admin Panel
  
  The Admin Panel provides administrators with powerful tools to manage the system. Some key functionalities include:
  
  - Managing user accounts, permissions, and roles.
  - Adding, editing, or removing movies from the database.
  - Adjusting system settings and configurations.
  
## Architecture and Principles

### &darr; Layered Architecture

The project employs a layered architecture for modularity and ease of maintenance. The fundamental layers include:

- **UI Layer:** User interface and user interaction.
- **Business Layer:** Business logic and services.
- **Data Access Layer:** Access to the database and usage of Entity Framework.
- **Entity Layer:** Definition of entities representing the data model.

### &darr; Code First Approach

I define the database schema in code using the Code First approach. Entity Framework Core automatically generates the database schema based on entities. This ensures that code and database schema are kept in sync.

### &darr; Repository Pattern

The Repository Pattern abstracts data access, making the code more modular. The `IMovieRepository` interface defines basic database operations for movie entities, and the `EfCoreMovieRepository` class concretely implements these operations.

### &darr; Unit of Work Pattern

The Unit of Work Pattern is employed to manage transactions and coordinate multiple repository operations within a single transaction. The `UnitOfWork` class orchestrates these operations, ensuring data consistency.

## Screenshots

<div style="display: flex; justify-content: space-between;">

### &darr; Home Screen Carousel and Lists
<img src="https://github.com/unsulliedd/.NET-BMTDb/assets/105812897/262618f2-fb61-43f0-aeac-acf12e7ba4bd" alt="Home Screen Carousel" width="500">
<img src="https://github.com/unsulliedd/.NET-BMTDb/assets/105812897/187a6cca-3950-4b88-bd90-cd61d7099493" alt="Home Screen Movie Lists" width="500">

</div>

### &darr; Movie Details
![Movie Details](https://github.com/unsulliedd/.NET-BMTDb/assets/105812897/3851c3a4-7cb6-46b7-8b78-b1c6f680751b)

### &darr; User Profile
![User Profile](https://github.com/unsulliedd/.NET-BMTDb/assets/105812897/9b8412f8-6b32-4127-a922-8da8d7d2e249)

### &darr; User Favourite Movie list wit Card View
![Favourite View](https://github.com/unsulliedd/.NET-BMTDb/assets/105812897/4caae9d7-ede5-468d-b803-7be7e4c167a7)

### &darr; Movie List
![Movie List](https://github.com/unsulliedd/.NET-BMTDb/assets/105812897/6fa58721-5b5d-45db-95db-ea0d75d708d4)

### &darr; Add Movie
![Add Movie](https://github.com/unsulliedd/.NET-BMTDb/assets/105812897/b08dc53f-849a-45dc-a17f-5fb732904afe)

### &darr; Admin Panel
![Admin Panel](https://github.com/unsulliedd/.NET-BMTDb/assets/105812897/c6b9cb56-4bb8-463c-835d-f2ea48471842)


## Getting Started

To run this project, you'll need to have the following installed:

- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet) or can upgrade to .NET 8
- [Visual Studio](https://visualstudio.com/) or another code editor of your choice.
  
### Dependencies

- [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://www.nuget.org/packages/Microsoft.AspNetCore.Identity.EntityFrameworkCore/6.0.9?_src=template) - Provides the ASP.NET Core Identity entity framework core user, role, and claim stores.
- [Microsoft.EntityFrameworkCore.Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/6.0.9?_src=template) - Design-time Entity Framework Core tools for generating code, adding migrations, and updating the database.
- [Microsoft.EntityFrameworkCore.SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/6.0.9?_src=template) - SQL Server database provider for Entity Framework Core.
- [Microsoft.EntityFrameworkCore.Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/6.0.9?_src=template) - Additional tools for Entity Framework Core commands in the .NET CLI.
- [Newtonsoft.Json](https://www.nuget.org/packages/Newtonsoft.Json/13.0.1?_src=template) - JSON framework for .NET, providing features like JSON serialization and deserialization.

### Api Keys Setup
Open the `appsettings.json` file in the `BMTDb` project.

   ```json
   {
     "EmailSender": {
       "Host": "smtp.office365.com",
       "Port": 587,
       "EnableSSL": true,
       "UserName": "[YourConfirmationEmailSenderAddress]", // Confirmation email sender address
       "Password": "[YourConfirmationEmailSenderPassword]" // Confirmation email sender password
     },
     "ApiKeys": {
       "TmdbApiKey": "[YourTmdbApiKey]", // TMDB API key
       "OmdbApiKey": "[YourOmdbApiKey]"  // OMDB API key
     },
     "AllowedHosts": "*",
     "ConnectionStrings": {
       "SqlConnection": "Server=(LocalDB)\\MSSQLLocalDB; Database=BMTDb; AttachDbFilename=[YourPathToDatabase]; Integrated Security=TRUE"
       // Change AttachDbFilename to your own path of the database  
       // Example: C:\\Users\\YourUsername\\Documents\\BMTDb.mdf
     }
   }
```
#### Configure Email Sender

1. Under the "EmailSender" section, provide the following information:
   - `Host`: SMTP host for sending confirmation emails (e.g., smtp.office365.com).
   - `Port`: Port for the SMTP server (e.g., 587).
   - `EnableSSL`: Set to `true` if SSL is enabled.
   - `UserName`: Confirmation email sender address.
   - `Password`: Confirmation email sender password.

#### Configure API Keys

2. Under the "ApiKeys" section, provide the following API keys:
   - `TmdbApiKey`: Your TMDB API key.
   - `OmdbApiKey`: Your OMDB API key.

#### Configure Database Connection

3. Under the "ConnectionStrings" section, provide the following information:
   - `SqlConnection`: SQL Server connection string. Modify the `AttachDbFilename` parameter to your database path (e.g., `C:\\Users\\YourUsername\\Documents\\BMTDb.mdf`).

4. Save the `appsettings.json` file.

### Database Setup

Before running the application, you need to create the database and apply migrations. Follow these steps:

1. Open a terminal and navigate to the project directory.

2. Run the following command to create the initial migration:

    ```bash
    dotnet ef migrations add InitialCreate
    ```

    This command generates a set of files in the `Data/Migrations` directory representing the initial state of the database.

3. Apply the migration to create the database:

    ```bash
    dotnet ef database update
    ```

    This command creates the database based on the defined model and schema.

4. Now, you can run the application:

    ```bash
    dotnet run
    ```

5. Open a web browser and navigate to [http://localhost:44384](http://localhost:44384) to interact with the movie database application.
## Contributing

If you'd like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create a new branch.
3. Make your changes and commit them.
4. Push your changes to your fork.
5. Open a pull request.
