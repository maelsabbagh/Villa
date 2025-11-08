# Villa: ASP.NET Core RESTful API & Web App

This project is a complete solution featuring a RESTful API and a consuming Web Application, built with ASP.NET Core. It is based on the curriculum of the Udemy course **"RESTful API with ASP.NET Core Web API"** and includes custom code design and implementations.

##  Overview

The "Villa" project demonstrates the creation and consumption of a secure, production-ready RESTful API. It is logically divided into three main components:

1.  **Villa_VillaAPI**: The core RESTful API service.
2.  **Villa_WebApp**: A server-side web application (MVC/Razor) that consumes the API.
3.  **Villa_Utility**: A shared class library for common models, helpers, and utilities.

## Project Structure

* `Villa_VillaAPI/`
    * The main ASP.NET Core Web API project.
    * Handles all business logic, data access, and API endpoints.
    * Implements CRUD operations for Villa and related resources.
    * Secured with authentication and authorization.
* `Villa_WebApp/`
    * An ASP.NET Core Web App (likely MVC or Razor Pages).
    * Acts as a client to `Villa_VillaAPI`.
    * Provides a user interface for interacting with the API (e.g., viewing, creating, and updating villas).
* `Villa_Utility/`
    * A .NET class library.
    * Contains shared resources like Data Transfer Objects (DTOs), static details, and helper functions to avoid code duplication between the API and Web App.

## Core Features

### API (`Villa_VillaAPI`)

* **RESTful Endpoints**: Full CRUD (Create, Read, Update, Delete) operations for villa management.
* **Repository Pattern**: Abstracted data access logic for clean and maintainable code.
* **Entity Framework Core**: Used as the Object-Relational Mapper (ORM) to interact with the database.
* **Authentication & Authorization**: (Likely) JWT-based authentication to secure endpoints.
* **Data Transfer Objects (DTOs)**: Uses AutoMapper to map between database models and API models.
* **API Documentation**: Integrated Swagger/OpenAPI for easy API testing and documentation.
* **Logging**: Implemented for monitoring and debugging.

### Web App (`Villa_WebApp`)

* **API Consumption**: Communicates with the `Villa_VillaAPI` using `HttpClient` to perform operations.
* **User Interface**: A front-end to manage villas, including login and registration pages.
* **Session Management**: Handles user sessions and tokens for interacting with the secure API.

## Technologies Used

* **.NET 9** 
* **ASP.NET Core Web API**
* **ASP.NET Core MVC / Razor Pages**
* **Entity Framework Core**
* **AutoMapper** (for DTOs)
* **Swagger / OpenAPI** (for API documentation)
* **Microsoft SQL Server** 

## Based On

This project is heavily inspired by and built upon the concepts and structure taught in the following Udemy course:

* **Course**: [RESTful API with ASP.NET Core Web API](https://www.udemy.com/course/restful-api-with-asp-dot-net-core-web-api)
* **Instructor**: (e.g., Bhrugen Patel -)

## Author's Note

While this project follows the core structure of the course, it also features my own custom code design, architectural decisions, and additional implementations.

## Getting Started

### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/download)
* [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or update the connection string for your preferred database)
* A code editor like [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1.  **Clone the repository:**
    ```sh
    git clone [https://github.com/maelsabbagh/Villa.git](https://github.com/maelsabbagh/Villa.git)
    cd Villa
    ```

2.  **Configure Database:**
    * Open `appsettings.json` in the `Villa_VillaAPI` project.
    * Update the `DefaultConnection` string to point to your SQL server.
    * Open the Package Manager Console in Visual Studio.
    * Set `Villa_VillaAPI` as the default project.
    * Run the migrations to create the database:
        ```sh
        Update-Database
        ```

3.  **Run the project:**
    * Set the solution to launch multiple startup projects (both `Villa_VillaAPI` and `Villa_WebApp`).
    * Press `F5` or run `dotnet run` for each project.
    * The API documentation will be available at `/swagger` (e.g., `https://localhost:7001/swagger`).
    * The Web App will be running on its own port (e.g., `https://localhost:7002`).

