Package Manager Console Help

https://docs.microsoft.com/en-us/ef/core/cli/powershell


Add-Migration InitialCreate -StartupProject BMTDb.WebUI -context BMTDbContext

Update-Database -StartupProject BMTDb.WebUI -context BMTDbContext

Add-Migration InitialCreateIdentity -StartupProject BMTDb.WebUI -context ApplicationContext

Update-Database -StartupProject BMTDb.WebUI -context ApplicationContext