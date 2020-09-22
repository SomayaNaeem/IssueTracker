# IssueTracker

IssueTracker is a sample application built using ASP.NET Core, CQRS and IdentityServer4

## Getting Started
Use these instructions to get the project up and running.

### Prerequisites
You will need the following tools:

* [Visual Studio Code or Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (version 16.3 or later)
* [.NET Core SDK 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
### Setup
Follow these steps to get your development environment set up:

  1. Clone the repository
  2. At the root directory, restore required packages by running:
      ```
     dotnet restore
     ```
  3. Next, build the solution by running:
     ```
     dotnet build
     ```

  4. change the connectionstring with your server name, user Id and password in the following
  * IssueTracker.Services.Identity.WebUI and IssueTracker.Services.Issues.WebUI
  	- appsettings.json 
  * IssueTracker.Services.Identity.Infrastructure.Factories
	- ApplicationDbContextFactory.cs
	- ConfigurationDbContextFactory.cs 
	- PersistedGrantDbContextFactory
  * IssueTracker.Services.Issues.Infrastructure.Factories
  	 - ApplicationDbContextFactory.cs

  5. Next, launch the IssueTracker.Identity.WebUI` and IssueTracker.Issues.WebUI` withen running multiple project:
      ```
	 dotnet run
	 ```
  6. Launch [https://localhost:44374/swagger/index.html) in your browser to view the Issues APIs
  
  7. Launch [https://localhost:44357/swagger/index.html) in your browser to view Identity APIs

## Technologies
* .NET Core 3.1
* Entity Framework Core 3.1
* IdentityServer4
* CQRS

## Versions
The [master](https://github.com/SomayaNaeem/IssueTracker) 
