# CoreX Fitness Backend

A RESTful API backend for the CoreX Fitness application, built with ASP.NET Core 9.0 and Entity Framework Core.  This backend provides user authentication, authorization, and user management features for a fitness tracking platform.

## 🚀 Features

- **User Authentication & Authorization**
  - User registration with email validation
  - Secure login with JWT token generation
  - Password hashing using BCrypt
  - Forgot password functionality
  - Password verification endpoint
  
- **User Management**
  - Create user accounts with fitness profile data (weight, height, age)
  - Update user information
  - Secure password storage with BCrypt hashing
  
- **Security Features**
  - JWT (JSON Web Token) authentication
  - Rate limiting (10 requests per minute with fixed window policy)
  - CORS configuration for cross-origin requests
  - Password hashing and verification
  
- **API Documentation**
  - Swagger/OpenAPI integration for API testing and documentation

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 9.0
- **Database**: SQL Server with Entity Framework Core 9.0
- **Authentication**:  JWT Bearer Token
- **Password Hashing**: BCrypt. Net
- **API Documentation**: Swagger/Swashbuckle
- **Rate Limiting**: ASP.NET Core Rate Limiting

## 📦 NuGet Packages

- `BCrypt.Net-Next` (4.0.3) - Password hashing
- `Microsoft.EntityFrameworkCore` (9.0.10)
- `Microsoft.EntityFrameworkCore.SqlServer` (9.0.10)
- `Microsoft.EntityFrameworkCore.Design` (9.0.10)
- `Microsoft.EntityFrameworkCore.Tools` (9.0.10)
- `Microsoft.AspNetCore.Authentication.JwtBearer` (9.0.10)
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (9.0.10)
- `Swashbuckle.AspNetCore` (9.0.6)
- `linq2db` (5.4.1.9)

## 📁 Project Structure

```
CoreX-Fitness-Backend/
├── Project/
│   ├── Controllers/
│   │   ├── AuthenticationController.cs    # User authentication endpoints
│   │   └── WeatherForecastController.cs   # Sample controller
│   ├── Models/
│   │   └── User.cs                        # User entity model
│   ├── DTO/
│   │   ├── UserRegisterDTO.cs             # Registration data transfer object
│   │   ├── UserLoginDTO.cs                # Login data transfer object
│   │   ├── ForgotPasswordDTO.cs           # Password reset data transfer object
│   │   ├── PasswordCherckerDTO.cs         # Password verification DTO
│   │   └── UpdateUserInformationDTO.cs    # User update DTO
│   ├── Data/
│   │   └── ApplicationDbContext.cs        # Database context
│   ├── Migrations/                        # EF Core migrations
│   ├── Program.cs                         # Application entry point
│   ├── appsettings.json                   # Configuration settings
│   └── Project.csproj                     # Project file
├── CoreX. sln                              # Solution file
└── README. md
```

## 🔌 API Endpoints

### Authentication Controller (`/api/Authentication`)

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| POST | `/register` | Register a new user | `UserRegisterDTO` |
| POST | `/Login` | Login and receive JWT token | `UserLoginDTO` |
| POST | `/forgotPassword` | Reset user password | `ForgotPasswordDTO` |
| POST | `/PasswordChecker` | Verify user password | `PasswordCherckerDTO` |
| POST | `/updateUserInformation` | Update user profile | `UpdateUserInformationDTO` |
| POST | `/ApiTesting` | API testing endpoint | `string` |

### Data Transfer Objects (DTOs)

**UserRegisterDTO**
```csharp
{
    "userName": "string",
    "password": "string",
    "email": "string",
    "weight": "float",
    "height": "float",
    "age": "int"
}
```

**UserLoginDTO**
```csharp
{
    "userName": "string",
    "password": "string",
    "email": "string"
}
```

**ForgotPasswordDTO**
```csharp
{
    "Email": "string",
    "newPassword": "string"
}
```

**UpdateUserInformationDTO**
```csharp
{
    "CurrentEmail": "string",
    "userName": "string",
    "password": "string",
    "email": "string",
    "weight": "float",
    "height": "float",
    "age": "int"
}
```

## 🔐 Security Configuration

### JWT Authentication
- Token expiration: 14 days
- Algorithm:  HMAC SHA512 Signature
- Claims: User ID and Name

### Rate Limiting
- Policy: Fixed Window
- Limit: 10 requests per 60 seconds
- Queue limit: 2 requests
- Status code on rejection: 429 (Too Many Requests)

### CORS Policy
Allowed origins: 
- `https://the-cour-four.github. io` (Production frontend)
- `http://127.0.0.1:5500` (VS Code Live Server)
- `http://localhost:5173` (Local development)

## 🗄️ Database

### User Model
```csharp
{
    "Id": "int",
    "Name": "string",
    "Email": "string",
    "passwordHashed": "string",
    "creationDate": "DateTime",
    "Weight": "float",
    "Height": "float",
    "Age": "int"
}
```

## ⚙️ Configuration

### Connection String
Update the connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-connection-string-here"
  },
  "AppSettings": {
    "Token": "your-jwt-secret-key-here"
  }
}
```

## 🚦 Getting Started

### Prerequisites
- .NET 9.0 SDK
- SQL Server
- Visual Studio 2022 or VS Code

### Installation

1. Clone the repository
```bash
git clone https://github.com/The-Cour-Four/CoreX-Fitness-Backend.git
cd CoreX-Fitness-Backend
```

2. Restore NuGet packages
```bash
dotnet restore
```

3. Update the database connection string in `appsettings.json`

4. Apply database migrations
```bash
dotnet ef database update
```

5. Run the application
```bash
dotnet run --project Project
```

6. Access Swagger UI
```
https://localhost:{port}/swagger
```

## 🌐 Deployment

The application is configured for deployment to Azure with:
- Swagger enabled in production
- CORS configured for the hosted frontend
- HTTPS redirection in production mode

## 📝 Notes

- Password hashing is implemented using BCrypt for secure storage
- JWT tokens are generated with a 14-day expiration
- Rate limiting is applied globally to all controllers (except WeatherForecastController)
- The API uses DTOs for data validation and transfer

## 👥 Authors

-Yousef Mahmoud Ali - Fullstack Developer & Project Manager



## 📄 License

This project documentation includes references to the project structure and implementation details.

---

For more information about the project, refer to the included PDF documentation: 
- `Core X Fitness Report With Names and IDs.pdf`
- `meetings_report[1].pdf`
