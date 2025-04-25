
# Products API - ASP.NET Core Minimal API

This repository contains a RESTful API for product management developed with ASP.NET Core using the Minimal API approach. The application allows CRUD operations (Create, Read, Update, Delete) on a collection of products stored in memory.

## Features

- List all products
- Get product by ID
- Create new product
- Update existing product
- Delete product

## Technologies Used

- .NET 9.0
- ASP.NET Core Minimal API
- Entity Framework Core (InMemory Provider)
- Swagger for API documentation
- xUnit for testing

## Project Structure

The project is organized as follows:

```
ProductsCrud/
├── Program.cs              # Application entry point and API configuration
├── Models/
│   └── Product.cs          # Data model for products
├── Data/
│   └── ProductDb.cs        # EF Core context for data management
└── ProductsCrud.csproj     # Project file

ProductsCrud.Tests/
├── ProductsServiceTests.cs # Unit tests for the API
└── ProductsCrud.Tests.csproj # Test project file
```

## How to Run

### Requirements

- .NET 9.0 SDK or higher

### Execution Steps

1. Clone the repository:
   ```bash
   git clone [repository-url]
   cd [folder-name]
   ```

2. Run the API:
   ```bash
   dotnet run --project ProductsCrud
   ```

3. Access the Swagger documentation:
   ```
   https://localhost:7000/swagger
   or
   http://localhost:5234/swagger
   ```

## API Endpoints

| Method | Route | Description |
|--------|------|-----------|
| GET | `/api/products` | List all products |
| GET | `/api/products/{id}` | Get specific product by ID |
| POST | `/api/products` | Create new product |
| PUT | `/api/products/{id}` | Update existing product |
| DELETE | `/api/products/{id}` | Delete product |

## Testing the API

### Using the Swagger Interface

After starting the application, navigate to `/swagger` for an interactive interface that allows you to test all endpoints.

### Using the HTTP File

In the root directory, there is a `ProductsCrud.Http` file that contains example requests for all endpoints. If you are using Visual Studio Code with the REST Client extension, you can send these requests directly from the editor.

Example file content:
```
@ProductsCrud_HostAddress = http://localhost:5234

### Get all products
GET {{ProductsCrud_HostAddress}}/api/products
Accept: application/json

### Get product by ID
GET {{ProductsCrud_HostAddress}}/api/products/1
Accept: application/json

### Create new product
POST {{ProductsCrud_HostAddress}}/api/products
Content-Type: application/json

{
  "name": "New Product",
  "price": 49.99
}

### Update existing product
PUT {{ProductsCrud_HostAddress}}/api/products/1
Content-Type: application/json

{
  "id": 1,
  "name": "Updated Product",
  "price": 59.99
}

### Delete product
DELETE {{ProductsCrud_HostAddress}}/api/products/1
```

## Automated Tests

This project includes unit tests to validate the API functionality. To run the tests:

```bash
dotnet test ProductsCrud.Tests
```

### Solution for Integration Test Errors

If you encounter errors related to the `testhost.deps.json` file when running integration tests, add the following property to the `ProductsCrud.Tests.csproj` project file:

```xml
<PropertyGroup>
  <!-- Other existing properties -->
  <PreserveCompilationContext>true</PreserveCompilationContext>
</PropertyGroup>
```

This configuration ensures that the compilation context is preserved and the necessary files are available during test execution with `WebApplicationFactory`.

### Alternative Testing Approaches

If you continue to experience issues with integration tests, the project offers three alternative approaches:

1. **Basic Unit Tests**: Test business logic directly using the repository pattern
2. **Manual Tests**: Use the ProductsCrud.Http file to test the API manually
3. **Simple Test Wrapper**: Tests that use HttpClient directly against the running API

## Contribution

Contributions are welcome! Please feel free to submit pull requests with improvements, bug fixes, or new features.

## License

[MIT](LICENSE)
