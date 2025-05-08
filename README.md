# CodeD Project (On Development Phase)

## Overview

CodeD is a sample project designed to enhance our expertise in **Domain-Driven Design (DDD)**. This is an evolving project, meaning that it will be continuously developed as we explore and experiment with various DDD concepts and methodologies. It serves as both a learning platform and a practical demonstration of how DDD can help address complex business challenges by structuring software around business domains.

> **Latest Updates:** Check our [Changelog](docs/ChangeLogs/20250507.md) for recent development activities. [[All Change Logs]](docs/ChangeLogs/AllChanges.md)

The project aligns with the philosophy of **"Teknolojide Mükemmellik"** – a guild within our organization dedicated to continuous learning and excellence in technology. Development of CodeD will be an integral part of the guild's meetings, where members collaborate, share insights, and contribute to the codebase.

By participating in this project, we aim to:

- **Learn** by experimenting with DDD practices such as aggregates, entities, value objects, and bounded contexts.
- **Collaborate** through guild sessions, encouraging knowledge-sharing and teamwork.
- **Iterate** by developing the project incrementally, applying feedback, and refining DDD implementations over time.

This project will not only help us build deeper DDD expertise but will also promote **best practices** in software development, fostering a culture of quality and innovation within our team.

---

## Table of Contents

- [Overview](#overview)
- [Technologies](#technologies)
- [Installation](#installation)
- [Usage](#usage)
- [Project Structure](#project-structure)
- [Features](#features)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

---

## Technologies

- **.NET 9.0** – Core framework for building the application.
- **Entity Framework Core** – ORM for data access.
- **Polly** – Resilience and transient fault-handling library.
- **PostgreSQL** – Relational database for persistent data storage.
- **MediatR** – CQRS implementation to decouple requests from handlers.
- **FluentValidation** – Validation library for domain models.

---

## Installation

Follow these steps to set up the project locally:

1. **Clone the repository:**

   ```bash
   git clone https://github.com/DogusTeknoloji/CodeD.git
   cd coded
   ```

2. **Install dependencies:**
   Ensure that you have the .NET 9.0 SDK installed. Run the following command:

   ```bash
   dotnet restore
   ```

3. **Database setup:**
   Make sure PostgreSQL is running. Update the `appsettings.json` with your PostgreSQL connection string.  
   Run migrations:

   ```bash
   dotnet ef database update
   ```

4. **Run the application:**

   ```bash
   dotnet run
   ```

---

## Usage

After starting the application, it will be available at:

```url
http://localhost:5000
```

Use tools like **Postman** or **Swagger** (if integrated) to explore the available APIs.

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

## Features

- **Domain-Driven Design:** Demonstrates the use of bounded contexts and aggregates.
- **CQRS with MediatR:** Separates read and write operations for better maintainability.
- **Polly Integration:** Handles transient faults with retries and circuit breakers.
- **FluentValidation:** Ensures input validation for API requests.
- **PostgreSQL Support:** Reliable data persistence with relational modeling.

---

## Contributing

We welcome contributions! Please follow these steps if you'd like to contribute:

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature-name`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature-name`).
5. Open a pull request.

---

## License

This project is licensed under the GNU General Public License v3.0 - see the [LICENSE](LICENSE) file for details.

---

## Contact

For any questions or feedback, please reach out:

- **Project Lead:** [Alper Konuralp](mailto:alper.konuralp@d-teknoloji.com.tr)
- **Company:** Doğuş Teknoloji

---
