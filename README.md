# GlobalExceptionHandler - ASP.NET Core 8 Web API

This project demonstrates advanced global exception handling in ASP.NET Core 8 using the built-in `ProblemDetails` standard (RFC 7807) and the new IExceptionHandler and IProblemDetailsService interfaces introduced in .NET 7+.

It is a clean and modern implementation for returning standardized and structured error responses to the client, ensuring consistency across the entire API.

## Project Overview
The GlobalExceptionHandler project shows how to:
- Use the built-in AddProblemDetails() and IProblemDetailsService to format error responses.
- Customize global exception handling using the new IExceptionHandler interface.
- Avoid writing try-catch blocks in each controller action.
- Return consistent application/problem+json responses to the client.
- Log exceptions automatically for troubleshooting.

## Technologies Used
- .NET 8 (ASP.NET Core)
- C#
- ProblemDetails (RFC 7807 standard)
- Built-in IExceptionHandler and IProblemDetailsService
- Swagger (OpenAPI)
- Logging with ILogger

## Why Use ProblemDetails?
ProblemDetails provides a standardized structure for representing HTTP errors. It helps:
- Frontend applications to easily parse and show user-friendly error messages.
- Maintain consistency across APIs.
- Avoid exposing stack traces or raw exceptions in production.
- Build extensible and customizable error-handling strategies.

## Features
- Centralized Error Handling using UseExceptionHandler() and IExceptionHandler.
- Custom Error Mapping for known exceptions (e.g., BadRequestException, NotFoundException).
- Enhanced Response with additional metadata like requestId for better traceability.
- Works perfectly with Swagger and Postman.
- Modern and scalable structure — ready for real-world APIs.

## API Endpoints to Try

| Method | Endpoint          | Description                    |
| ------ | ----------------- | ------------------------------ |
| GET    | /api/products/1   | Returns sample product         |
| GET    | /api/products/0   | Returns 400 BadRequest error   |
| GET    | /api/products/999 | Returns 404 NotFound error     |
| POST   | /api/products     | Returns 500 for negative price |

## How It Works (Architecture)

### 1. GlobalExceptionHandler.cs
Implements IExceptionHandler. It maps exceptions to proper HTTP status codes and delegates the response generation to IProblemDetailsService. This avoids manual Response.WriteAsync logic.

### 2. Program.cs
- Registers the GlobalExceptionHandler using AddExceptionHandler<T>().
- Configures the default ProblemDetails behavior using AddProblemDetails().
- Uses app.UseExceptionHandler() middleware to plug in the global handler.

### 3. CustomizeProblemDetails
Adds a custom field (requestId) to the response for correlation and debugging across services.

## Sample Error Response
For a BadRequestException:

```
HTTP/1.1 400 Bad Request
Content-Type: application/problem+json

{
  "type": "BadRequestException",
  "title": "An error occurred",
  "status": 400,
  "detail": "Product ID must be greater than 0.",
  "requestId": "0HMS2AKLQ02UK:00000001"
}
```

## Advantages of This Approach
- No boilerplate try-catch code in controllers
- Flexible and extensible — supports any custom exception
- Uses .NET-native error handling without third-party packages
- Clean separation of concerns: controllers focus on logic; error handler manages errors
- Production-ready and future-proof (based on latest .NET features)
