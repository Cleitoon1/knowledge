## Running the app

### Dependencies
- .NET core: https://www.microsoft.com/net/core?WT.mc_id=Blog_CENews_Announce_CEA

### Install references
```bash
dotnet restore
```

Generate the database
```bash
cd [project_folder]\Web
dotnet ef database update -v -s ../Web
```

If you have to change any model (and thus the database), run the following commands:

```bash
cd [project_folder]\Data
dotnet ef migrations add [Migration name] -s ../Web
# eg: dotnet ef migrations add FirstMigration
dotnet ef database update -v -s ../Web
```