# CoreX Fitness 💪

A modern full-stack web application for building workout routines, logging training sessions, and tracking your fitness progress. This project was developed as part of our Software Engineering course, built by a team of junior Computer Science students.

Our goal was to create a full-stack application with a clear separation between a robust backend API and an interactive, user-friendly frontend.

## ✨ Key Features

### 👤 User Authentication
*   **Secure Registration & Login:** Secure system to keep user data private and personalized using JWT (JSON Web Tokens).
*   **Data Privacy:** Passwords are securely hashed using `BCrypt`.

### 📚 Customizable Exercise Library
*   **Exercise Database:** Comes pre-loaded with common exercises, but users can add their own custom movements to the library.

### 📅 Workout Routine Builder
*   **Custom Plans:** Easily create, view, update, and delete custom workout plans (e.g., "Push Day", "Leg Day", "Full Body").

### ✍️ Detailed Session Logging
*   **Track Everything:** Log every workout session.
*   **Strength:** Track sets, reps, and weight.
*   **Cardio:** Track distance and duration.

### 📈 Progress Visualization
*   **Dynamic Charts:** The core of the application! Select any exercise and view a dynamic line chart that visualizes your performance and progress over time.

## 🛠️ Technology Stack

This project is built using a modern technology stack:

### Backend
*   **Framework:** .NET 9 (ASP.NET Core Web API)
*   **Database:** SQL Server
*   **ORM:** Entity Framework Core
*   **Authentication:** JWT (JSON Web Tokens) & BCrypt

### Frontend
*   **Framework:** React
*   **Styling:** CSS Modules / Tailwind CSS
*   **API Communication:** Axios
*   **Charting:** Chart.js or Recharts

### Tools
*   **Version Control:** Git & GitHub
*   **IDE:** Visual Studio 2022 / VS Code

## 🚀 Getting Started

Follow these instructions to get a local copy of the project up and running for development and testing purposes.

### Prerequisites
You will need the following software installed on your machine:
*   [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
*   [Node.js](https://nodejs.org/) (which includes npm)
*   [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or another SQL Server instance.

### Installation & Setup

#### 1. Clone the repository
```bash
git clone https://github.com/YousefAliMLS/CoreX-Fitness.git
cd CoreX-Fitness
```

#### 2. Backend Setup
Navigate to the backend project directory:
```bash
cd Project
```

**Configuration:**
1.  Restore NuGet packages:
    ```bash
    dotnet restore
    ```
2.  Configure your database connection:
    *   Open `appsettings.json`.
    *   Update the `"DefaultConnection"` string with your SQL Server credentials.
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=CoreXDataBase;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```
3.  Apply Entity Framework migrations to create the database schema:
    ```bash
    dotnet ef database update
    ```

**Run the Backend:**
```bash
dotnet run
```
The API will be running on `https://localhost:7xxx` (or similar port).

#### 3. Frontend Setup
Open a new terminal window and navigate to the frontend project directory:
```bash
cd fittrack-client
```

**Install & Run:**
1.  Install NPM packages:
    ```bash
    npm install
    ```
2.  Run the Frontend:
    ```bash
    npm start
    ```
The application will open in your browser at `http://localhost:3000`.

## 👥 Team

*   **Yousef Mahmoud Ali** - Fullstack Developer
*   **Mostafa Abd Elhamied** - Frontend Developer
*   **Mahmoud Khaled** - Frontend Developer
*   **SalahEldin Mohamed** - Backend Developer

## 📄 License
This project is licensed under the MIT License - see the LICENSE file for details.