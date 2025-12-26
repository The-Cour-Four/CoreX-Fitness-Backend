# CoreX Fitness - RESTful API

The CoreX Fitness backend is a secure, scalable API built on the .NET 8 framework. It handles all business logic, user authentication, and physical statistic calculations.

### 🛠 Tech Stack
* **Framework:** ASP.NET Core Web API (.NET 8).
* **Data Access:** Entity Framework Core.
* **Database:** SQL Server.
* **Security:** JWT (JSON Web Tokens) for sessions and BCrypt for password hashing.

### ✨ Key Features
* **Authentication Module:** Implements secure Login/Registration with JWT token generation and 14-day expiry.
* **Security-First Flow:** Includes a dedicated `/PasswordChecker` endpoint required for verifying identity before sensitive profile updates.
* **Async Operations:** All database interactions utilize `async/await` to ensure a responsive, non-blocking execution thread.
* **Data Integrity:** Backend validation prevents SQL injection and rejects requests with missing or invalid data.

### 📊 Data Specifications
The primary **User** entity includes:
* **Id:** Unique Integer (Primary Key).
* **Credentials:** Unique Email and Password (stored as BCrypt Hash).
* **Body Stats:** Weight (float), Height (float), and Age (int) used for health tracking.
