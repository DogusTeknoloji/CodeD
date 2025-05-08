# GitHub Copilot Instructions for CodeD Project

## Overview
This document provides guidelines for using GitHub Copilot effectively within the CodeD project. The project is designed to enhance expertise in **Domain-Driven Design (DDD)** and follows specific conventions and practices to ensure code quality and maintainability.

---

## Coding Conventions

1. **Follow DDD Principles:**
   - Use aggregates, entities, value objects, and bounded contexts as per DDD guidelines.
   - Ensure that domain logic resides in the domain layer (`CodeD.Domain`).

2. **Use CQRS Pattern:**
   - Separate read and write operations using commands and queries.
   - Place command handlers in `CodeD.Application.Commands` and query handlers in `CodeD.Application.Queries`.

3. **Validation:**
   - Use `FluentValidation` for input validation.
   - Place validators in the `CodeD.Application` layer.

4. **Error Handling:**
   - Use the `Result` class for consistent error handling.
   - Avoid throwing exceptions directly; instead, return meaningful error results.

5. **Database Access:**
   - Use `Entity Framework Core` for data access.
   - Place entity configurations in `CodeD.Infrastructure.Data.EntityConfigurations`.

6. **Logging:**
   - Use `ILogger` for logging.
   - Ensure meaningful log messages, especially in controllers and services.

---

## Project Structure

```text
CodeD/
│
├── CodeD.Application/        # Application layer (CQRS, DTOs, validations)
├── CodeD.Domain/             # Core domain models and business rules
├── CodeD.Infrastructure/     # Data access, external services, configurations
├── CodeD.API/                # API layer (controllers, Swagger, middlewares)
├── tests/                    # Unit and integration tests
└── README.md                 # Project documentation
```

---

## Key Technologies

- **.NET 9.0** – Core framework for building the application.
- **Entity Framework Core** – ORM for data access.
- **Polly** – Resilience and transient fault-handling library.
- **PostgreSQL** – Relational database for persistent data storage.
- **MediatR** – CQRS implementation to decouple requests from handlers.
- **FluentValidation** – Validation library for domain models.

---

## Using GitHub Copilot

1. **Code Suggestions:**
   - Use Copilot to generate boilerplate code for controllers, services, and handlers.
   - Review and refactor suggestions to align with project conventions.

2. **Documentation:**
   - Use Copilot to draft comments and documentation.
   - Ensure that generated comments are accurate and meaningful.

3. **Testing:**
   - Use Copilot to generate unit and integration test templates.
   - Place tests in the `tests` directory, following the project structure.

4. **Error Handling:**
   - Use Copilot to suggest error-handling patterns.
   - Ensure that suggestions align with the `Result` class usage.

---

## Best Practices

- **Iterate and Refine:** Use Copilot as a starting point and refine the code to meet project standards.
- **Collaborate:** Share insights and improvements during guild meetings.
- **Learn:** Use Copilot to explore new patterns and practices in DDD.

---

## Contact

For any questions or feedback, please reach out:

- **Project Lead:** [Alper Konuralp](mailto:alper.konuralp@d-teknoloji.com.tr)
- **Company:** Doğuş Teknoloji