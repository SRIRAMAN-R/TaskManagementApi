# Task Management API

A simple task management API built using .NET Core that allows users to perform CRUD operations on tasks, along with basic validation, error handling, and an option to export tasks as CSV. The API also supports pagination and filtering tasks by status and due date.

## Requirements

- .NET 6.0 or later
- Visual Studio or any .NET compatible IDE
- Postman or any API testing tool for making requests

## Features

1. **CRUD Operations**: Create, Read, Update, Delete tasks.
2. **Filters**: Filter tasks by status (`Pending`, `In Progress`, `Completed`) and due date.
3. **Pagination**: Fetch tasks with pagination (page number, page size).
4. **CSV Export**: Export all tasks to a CSV file.
5. **Logging**: Basic logging for task creation, updates, and deletions.
6. **Swagger Documentation**: The API is documented using Swagger for easy testing and interaction.

## API Endpoints

### 1. GET /tasks

- Fetch a list of all tasks.
- Supports optional filters:
  - `status`: Filter tasks by status (`Pending`, `In Progress`, `Completed`).
  - `dueDate`: Filter tasks that are due before a specific date.
  - Pagination:
    - `pageNumber`: The page number (default: 1).
    - `pageSize`: The number of tasks per page (default: 10).

### 2. GET /tasks/{id}

- Fetch the details of a specific task by its ID.

### 3. POST /tasks

- Create a new task.
- **Request body**:
  - `title`: The title of the task (required).
  - `description`: The description of the task (optional).
  - `status`: The status of the task (`Pending`, `In Progress`, `Completed`) (default: `Pending`).
  - `dueDate`: The due date of the task (required).

- PUT /api/tasks/1
- DELETE /api/tasks/1
- GET /api/tasks/export

## Swagger Documentation
  -https://localhost:5206/swagger
  
## Bonus Features
  - CSV Export: Allows you to export tasks to a CSV file by calling the /tasks/export endpoint.
  - Swagger/OpenAPI Documentation: Integrated Swagger for easy testing and documentation of the API.
  
## Testing
  - Unit tests are written using NUnit. To run the tests
  - Navigate to the TaskManagementApi.Tests directory
  - Run the tests using the following command
  - dotnet test

**Technologies Used**
- .NET 6.0: The core framework for the API.
- Entity Framework Core: Used for data persistence with an in-memory database.
- Swagger/OpenAPI: Used for API documentation and testing.
- NUnit: Testing framework used for unit and integration tests.
- CSVHelper: For exporting tasks to CSV format.

### Explanation:
- The **Requirements** section describes the necessary software and tools.
- **Features** and **API Endpoints** sections describe the API's functionality and available endpoints.
- The **Running the Project Locally** section provides instructions on how to run the API locally, including restoring dependencies, running the project, and testing it.
- **Swagger Documentation** is included for easy API exploration.
- **Bonus Features** highlight the CSV export and Swagger integration.
- **Technologies Used** outlines the technologies implemented.
