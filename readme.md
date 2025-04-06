# Student SMS System

This is the Student SMS System, a simple system for managing student information. It provides basic functionalities such as:

- **Create student**
- **Update student**
- **Edit student**
- **Delete student**
- **Import students**
- **Get student by ID**
- **Get all students**

The project is built following the principles of Clean Architecture and is divided into the following layers:

- **Domain Layer:** Contains the core business logic and entities.
- **Application Layer:** Handles application-specific business rules and use cases.
- **Infrastructure Layer:** Implements data access, external service integration, and other technical concerns.
- **Presentation Layer:** Demonstrates two approaches to presentation:
  - **Web API:** For traditional RESTful HTTP endpoints.
  - **Blazor Server:** For an interactive web application.

## Getting Started

### Prerequisites

- [.NET 8 SDK (or later)](https://dotnet.microsoft.com/download)
- A suitable IDE or code editor (e.g., Visual Studio, VS Code)

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/your-username/student-sms-system.git
   cd CleanArchitectureDemo-SMS
   ```

## Running the Application with Docker and .NET

After navigating to the `src` directory, follow these steps:

1. **Build and Start the Docker Environment**

   In the `src` directory, run:

   ```bash
   docker-compose up --build
   ```

Then, depending on what you want to see:

2. **Run the WebApi project**

   In the `src` directory, run:

   ```bash
   cd WebApi
   dotnet watch run
   ```

3. **Run the View project**
   In the `src` directory, run:

   ```bash
   cd BlazorServer
   dotnet watch run
   ```

## Contributors

- **Ho Chi Minh University of Social Science**
- **22127345 - Nguyễn Hồng Quân**
- **22127389 - Nguyễn Phúc Thành**
