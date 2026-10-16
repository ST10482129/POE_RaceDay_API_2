 POE_RaceDay_API

An ASP.NET Core Web API developed in C# to support the management of race day activities, including events, categories, participant enrolments, and race results.

 Table of Contents

1. [Project Overview](#1-project-overview)
2. [Technologies Used](#2-technologies-used)
3. [Features](#3-features)
4. [Prerequisites](#4-prerequisites)
5. [Installation and Setup](#5-installation-and-setup)
6. [Database Configuration](#6-database-configuration)
7. [Authentication and Authorisation](#7-authentication-and-authorisation)
8. [API Endpoints](#8-api-endpoints)
9. [Testing](#9-testing)
10. [Continuous Integration](#10-continuous-integration)
11. [Project Structure](#11-project-structure)
12. [Troubleshooting](#12-troubleshooting)
13. [Security Considerations](#13-security-considerations)
14. [Conclusion](#14-conclusion)

 1. Project Overview

**POE_RaceDay_API** is a RESTful Web API built using ASP.NET Core and C#. It provides backend functionality for managing race day information and related operations.

The API uses controller-based request handling, SQL Server for data storage, request validation, and JWT-based authentication to protect restricted endpoints.

The project also includes unit testing and a GitHub Actions workflow to help automate build validation and test execution.

## 2. Technologies Used

- **Programming language:** C#
- **Framework:** ASP.NET Core Web API
- **Platform:** .NET
- **Database:** Microsoft SQL Server
- **Data access:** ADO.NET using `Microsoft.Data.SqlClient`
- **Authentication:** JSON Web Tokens (JWT)
- **API architecture:** REST
- **Testing framework:** xUnit
- **Version control:** Git and GitHub
- **Continuous integration:** GitHub Actions

The exact SDK version and package versions should match those specified in the project files.

 3. Features

The API is designed to support the following functionality:

- **Event management:** Retrieve events and allow authorised organisers to create, update, and delete their events.
- **Category management:** Retrieve race categories and allow authorised organisers to create categories for events they own.
- **Participant enrolments:** View enrolments and allow authenticated participants to register for eligible categories.
- **Result management:** Support race result operations according to the implemented result controller.
- **Request validation:** Validate incoming data and return appropriate HTTP status codes for invalid requests.
- **Authentication and authorisation:** Restrict selected operations based on JWT identity and user roles.
- **Database integration:** Read and write race day information using SQL Server.
- **Unit testing:** Test selected controller validation and response behaviour.
- **Automated build and testing:** Use GitHub Actions to validate changes.

The available functionality depends on the implementation of the controllers and the database schema.

 4. Prerequisites

Before running the project, ensure that you have installed:

- A .NET SDK compatible with the target framework in the project file.
- Visual Studio, Visual Studio Code, or another compatible C# IDE.
- Microsoft SQL Server.
- SQL Server Management Studio (SSMS) or another SQL client.
- Git.
- A GitHub account if you intend to use GitHub repository hosting and Actions.

## 5. Installation and Setup

### Step 1: Clone the repository

Open a terminal and run:

```bash
git clone <YOUR_GITHUB_REPOSITORY_URL>
```

Navigate to the cloned repository:

```bash
cd POE_RaceDay_API
```

Replace the placeholder repository URL with your actual GitHub repository URL.

### Step 2: Restore dependencies

From the directory containing the API project file, run:

```bash
dotnet restore
```

This restores the NuGet packages required by the project.

### Step 3: Build the API

Run:

```bash
dotnet build
```

Resolve any build errors before proceeding.

### Step 4: Configure the database

Configure the SQL Server connection string as described in Section 6.

Ensure that the database and required tables exist before running endpoints that access the database.

### Step 5: Run the API

From the API project directory, execute:

```bash
dotnet run
```

The terminal will display the address on which the application is listening.

Use the configured address to access the API. If Swagger is enabled in the application, open its Swagger UI address to inspect and test the available endpoints.

## 6. Database Configuration

The API retrieves its SQL Server connection string using the configuration key:

`RaceDayConnection`

Configure this value using an appropriate local configuration source or environment variable.

For example, in a local development configuration file:

```json
{
  "ConnectionStrings": {
    "RaceDayConnection": "YOUR_SQL_SERVER_CONNECTION_STRING"
  }
}
```

Replace the placeholder with the connection string for your environment. Do not commit real passwords or sensitive database credentials to GitHub.

The database must contain the tables and relationships expected by the API, including the relevant users, events, categories, enrolments, and results tables.

Use the database SQL scripts supplied with the project, if available, to create the required schema.

## 7. Authentication and Authorisation

The API uses JSON Web Tokens (JWT) to authenticate requests to protected endpoints.

Depending on the endpoint, access may be restricted to particular roles, such as:

- **Organiser:** Manages events and creates categories for events they own.
- **Participant:** Registers for eligible race categories.

### Accessing a protected endpoint

1. Obtain a valid JWT through the authentication functionality implemented by the application.
2. Copy the issued token.
3. Include it in the HTTP request's `Authorization` header.
4. Use the `Bearer` authentication scheme.

Example:

```http
Authorization: Bearer YOUR_JWT_TOKEN
```

A request may be rejected if the token is missing, invalid, expired, or does not contain the required role.

The endpoint's authorisation attributes and the application's JWT configuration determine the actual access rules.

## 8. API Endpoints

The following table summarises the main controller routes described by the project.

| HTTP Method | Endpoint | Purpose |
|---|---|---|
| GET | `/api/events` | Retrieve events |
| POST | `/api/events` | Create an event |
| PUT | `/api/events/{id}` | Update an event |
| DELETE | `/api/events/{id}` | Delete an event |
| GET | `/api/category` | Retrieve categories |
| POST | `/api/category` | Create a category |
| GET | `/api/enrolment` | Retrieve enrolments |
| POST | `/api/enrolment` | Create a participant enrolment |

These routes are based on the supplied controller definitions. The complete endpoint list, including result and authentication routes, should be confirmed from the remaining controllers and application configuration.

### Common HTTP status codes

| Status code | Meaning |
|---|---|
| `200 OK` | The request succeeded. |
| `201 Created` | A resource was created successfully. |
| `400 Bad Request` | The request contains invalid input. |
| `401 Unauthorized` | Valid authentication is missing or could not be established. |
| `403 Forbidden` | The authenticated user does not have permission to perform the operation. |
| `404 Not Found` | The requested resource was not found or is unavailable to the caller. |
| `409 Conflict` | The request conflicts with existing data, such as a duplicate enrolment. |
| `500 Internal Server Error` | An unexpected server-side error occurred. |

Actual responses depend on the controller implementation and application error handling.

## 9. Testing

The project includes a test project for checking API behaviour.

### Run all tests

From the solution directory, run:

```bash
dotnet test
```

To run tests for a specific test project:

```bash
dotnet test <YOUR_TEST_PROJECT.csproj>
```

Replace the placeholder with the actual test project path.

### Testing objectives

Tests should cover:

- Valid and invalid request data.
- Required fields and numeric validation.
- Expected HTTP status codes.
- Missing or invalid user identifiers.
- Authentication and role-based authorisation.
- Event ownership and enrolment rules.
- Database-related behaviour where suitable test infrastructure is available.

Controller tests that directly invoke methods do not automatically execute ASP.NET Core authentication and authorisation filters. Those behaviours should be tested separately using appropriate integration tests.

Tests that open a real SQL Server connection require a correctly configured test database. Tests that exercise only early validation branches may not need database access.

## 10. Continuous Integration

GitHub Actions can automate the build and test process whenever changes are pushed or a pull request is opened.

Workflow files are stored in:

`.github/workflows/`

A typical workflow includes the following steps:

1. Check out the repository.
2. Install the required .NET SDK.
3. Restore dependencies.
4. Build the API.
5. Run the unit tests.
6. Report the workflow result.

The workflow should use the target framework and project paths defined in the repository.

To check the outcome of a workflow:

1. Open the repository on GitHub.
2. Select the **Actions** tab.
3. Open the relevant workflow run.
4. Review the logs for build errors or failed tests.

A successful workflow indicates that the configured steps completed successfully; it does not guarantee that every feature is free of defects.

## 11. Project Structure

The following is an illustrative project structure. Your actual repository may contain additional files or different folder names.

```text
POE_RaceDay_API/
├── Controllers/
│   ├── EventController.cs
│   ├── CategoryController.cs
│   ├── EnrolmentController.cs
│   └── ResultController.cs
├── DTOs/
├── Properties/
├── Program.cs
├── appsettings.json
├── POE_RaceDay_API.csproj
├── TestProject3/
│   └── [Unit test files]
├── .github/
│   └── workflows/
│       └── [Workflow file]
└── README.md
```

Keep your existing directory structure when updating the README. Replace the illustrative test and workflow names with the actual names in your repository.

## 12. Troubleshooting

### The project does not build

- Confirm that the required .NET SDK is installed.
- Run `dotnet restore`.
- Run `dotnet build`.
- Review the error messages and verify project references and NuGet packages.

### The API cannot connect to SQL Server

- Verify the `RaceDayConnection` configuration.
- Confirm that SQL Server is running and reachable.
- Check the database name and authentication details.
- Ensure the required tables exist and match the SQL queries used by the controllers.

### The unit tests fail

- Confirm that the test project references the API project.
- Verify that the required xUnit and test SDK packages are installed.
- Check whether the failing test requires a database connection.
- Review the test output to identify the failed assertion or exception.

### Authentication fails

- Confirm that JWT authentication is configured in the application.
- Verify the token, signing configuration, and expiry.
- Check that the required role claims are present.
- Confirm that the request uses the `Bearer` scheme.

### GitHub Actions fails

- Open the failed workflow run in the repository's Actions tab.
- Review the build and test logs.
- Confirm that the workflow uses the correct SDK version and project paths.
- Ensure the workflow does not depend on unavailable local configuration or secrets.

## 13. Security Considerations

- Never commit passwords, signing keys, or database credentials.
- Store sensitive values in environment variables or an appropriate secret-management solution.
- Validate all incoming request data.
- Use authentication and role-based authorisation for restricted operations.
- Enforce resource ownership checks where users manage their own records.
- Use HTTPS in deployed environments.
- Avoid returning sensitive internal exception details to API clients.
- Use a separate test database when integration tests require database access.

## 14. Conclusion

POE_RaceDay_API demonstrates the use of ASP.NET Core, C#, SQL Server, RESTful API design, JWT authentication, unit testing, and automated build validation.

The project provides a foundation for managing race day information and can be extended through improved test coverage, robust error handling, stronger security controls, and continued CI automation.

For implementation-specific details, refer to the controllers, DTOs, database scripts, test project, and GitHub Actions workflow in the repository.


