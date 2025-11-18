# Blogging Platform API
A RESTful API for managing a simple blogging platform, with .NET 8, was developed as a solution 
for the proposed code challenge.

## Technology Stack
- Framework: .NET 8
- Database: SQLite
- ORM: Entity Framework Core
- Architecture: REST API with structured project organization

## Project Structure
```
blog/
├── Controllers/     # API endpoints
├── Dtos/            # Data representation for endpoints ouput
├── Entities/        # Data models (BlogPost, Comment)
├── Exceptions/      # Single point to handle exceptions
├── Middlewares/     # Custom middleware (exception handling)
├── Repositories/    # Data management and access
└── Program.cs       # Application entry point
```

## API Endpoints
### Blog Posts
- `GET /api/posts`- Retrieve all blog posts with comment counts
- `POST /api/posts` - Create a new blog post
- `GET /api/posts/{id}` - Retrieve a specific blog post with its comments

### Comments
- `POST /api/posts/{id}` - Add a comment to a specific blog post

## Installation and Running
After cloning the repository, execute:

```bash
dotnet restore
dotnet run
```

The API will be available at [https://localhost:7079](https://localhost:7079) and [http://localhost:5170](http://localhost:5170).

## Development Notes
### Technical Decisions
- .NET 8: Chosen for its performance, modern features, and long-term support (LTS)
- SQLite Database: Selected due to its ease of use and because of the time constraint, as it would take too long to configure a Docker container for MS SQL Server
- Project Organization: Everything was implemented in the same project with clear folder separation (controllers, middlewares, entities, etc.) for maintainability
- Exception Handling: Implemented a middleware to handle all application exceptions (known and unknown) to prevent exposing critical information to end users

## If I Had More Time...
Given additional development time, I would implement the following improvements:

### 1. Clean Architecture Implementation
I would restructure the solution following Clean Architecture principles, organizing the project into distinct layers:

- Core Layer: Contains business entities, interfaces, and domain logic
- Infrastructure Layer: Implements data access and external services
- API Layer: Handles HTTP requests, controllers, and presentation logic

Advantages of Clean Architecture:
- Separation of Concerns: Each layer has a single responsibility
- Testability: Business logic can be tested without dependencies on infrastructure
- Maintainability: Changes in one layer don't affect others
- Flexibility: Easy to swap out implementations (e.g., database, external services)

### 2. Enhanced Exception Handling
- Implement custom exception types for different error scenarios
- Create detailed error messages with proper error codes
- Add structured error responses for better client handling

### 3. Comprehensive Logging System
Implement a four-level logging system:
- `INFO`: General application flow and operations
- `WARNING`: Potential issues that don't break functionality
- `ERROR`: Recoverable errors that affect specific operations
- `FATAL`: Critical errors that require immediate attention

### 4. Input Validation System
- Implement fluent validation for all API inputs
- Add request/response DTOs with proper validation attributes
- Create custom validation middleware for consistent error responses

### 5. Unit and Integration Testing
- Unit tests for small and isolated components
- Integration tests for end-to-end API functionality
- Use of testing frameworks like xUnit

## Production Readiness
Even with the time constraints, the current solution is fully functional and could be deployed to production with minor adjustments, primarily on the DevOps side, such as:

- Environment-specific configuration management
- Database migration strategies
- Monitoring and health checks
- Security enhancements (authentication/authorization)
