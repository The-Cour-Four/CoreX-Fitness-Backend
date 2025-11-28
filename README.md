# CoreX Fitness API 💪

A robust backend Web API for the **CoreX Fitness** platform. This project serves as the server-side foundation for building workout routines, managing user identities, and tracking fitness progress.

Built using **.NET 9** and **Entity Framework Core**, this API ensures secure and efficient data handling for the CoreX ecosystem.

## ✨ Current Features

### 👤 User Authentication & Security
*   **Secure Registration:** User account creation with `BCrypt` password hashing.
*   **JWT Authorization:** Secure Login system issuing JSON Web Tokens (JWT) for stateless authentication.
*   **Password Management:** Functionality to handle password resets/updates.
*   **User Management:** Dedicated database schema for storing user profiles and credentials.

### 🔌 API Infrastructure
*   **Swagger UI:** Interactive API documentation available for testing endpoints directly in the browser.
*   **SQL Server Integration:** Fully configured Entity Framework Core context for SQL Server.

## 🛠️ Technology Stack

This project is built using the latest Microsoft technologies:

*   **Framework:** .NET 9 (ASP.NET Core Web API)
*   **Database:** SQL Server
*   **ORM:** Entity Framework Core 9
*   **Authentication:** JWT (JSON Web Tokens) & BCrypt.Net
*   **Documentation:** Swagger / OpenAPI

## 🚀 Getting Started

Follow these instructions to get the backend API running on your local machine.

### Prerequisites
*   [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
*   [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any SQL Server instance)
*   Visual Studio 2022 or VS Code

### Installation & Setup

1.  **Clone the repository**
    ```bash
    git clone https://github.com/YousefAliMLS/CoreX-Fitness.git
    ```

2.  **Navigate to the project directory**
    ```bash
    cd CoreX-Fitness/Project
    ```

3.  **Configure Database**
    *   Open `appsettings.json`.
    *   Update the `DefaultConnection` string if your SQL Server instance name is different from `Yousef-PC`.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=CoreXDataBase;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```

4.  **Apply Migrations**
    Create the database and tables using Entity Framework:
    ```bash
    dotnet ef database update
    ```

5.  **Run the Application**
    ```bash
    dotnet run
    ```

    The API will launch, and you can access the Swagger UI at:
    *   `https://localhost:7xxx/swagger` or `http://localhost:5xxx/swagger`

## 🛣️ Roadmap (Coming Soon)
*   [ ] Exercise Library Management
*   [ ] Workout Routine Builder ("Push Day", "Leg Day", etc.)
*   [ ] Session Logging (Sets, Reps, Weights)
*   [ ] Progress Visualization Charts

## 👥 Team

*   **Yousef Mahmoud Ali** - Fullstack Developer
*   *Mostafa abd Elhamied* - *Frontend Developer*
*   *Mahmoud Khaled* - *Frontend Developer*
*   *SalahEldin Mohamed* - *Backend Developer*


