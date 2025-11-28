# CoreX Fitness 💪

A modern, full-stack web application designed to help users build workout routines, log training sessions, and track fitness progress. This project demonstrates a clear separation of concerns, featuring a robust .NET Web API backend and a lightweight, responsive native frontend.

---

## 🚀 Features

### 👤 **User Authentication & Security**
*   **Secure Access:** JWT (JSON Web Tokens) based authentication system.
*   **Data Protection:** Passwords are hashed and secured using `BCrypt`.

### 📚 **Exercise Library**
*   **Comprehensive Database:** Access a wide range of pre-loaded exercises.
*   **Customization:** Users can add their own custom movements to personal libraries.

### 📅 **Workout Routine Builder**
*   **Personalized Plans:** Create and manage custom workout splits (e.g., "Push Day," "Upper Body").
*   **Flexible Management:** Easy-to-use interface for updating and deleting routines.

### ✍️ **Session Logging**
*   **Detailed Tracking:** Log every set, rep, and weight lifted.
*   **Cardio Support:** Specific fields for distance and duration tracking.

### 📈 **Progress Visualization**
*   **Analytics:** Visualize strength and consistency trends over time.
*   **Dynamic Charts:** Interactive graphs powered by your training data.

---

## 🛠️ Technology Stack

### **Frontend** (Client-Side)
*   **Core:** HTML5, CSS3, JavaScript (ES6+)
*   **HTTP Client:** Fetch API / Axios (for backend communication)
*   **Styling:** Custom CSS / Responsive Design
*   **Visualization:** Chart.js (for progress tracking)

### **Backend** (Server-Side)
*   **Framework:** ASP.NET Core Web API (.NET 9)
*   **Database:** Microsoft SQL Server
*   **ORM:** Entity Framework Core (EF Core)
*   **Auth:** JWT Authentication

---

## 📂 Project Structure

The repository is organized into two main directories:

*   `Project/`: Contains the ASP.NET Core Web API solution.
*   `fittrack-client/`: Contains the static HTML, CSS, and JS files for the frontend.

---

## ⚡ Getting Started

Follow these steps to set up the project locally.

### Prerequisites
*   [.NET 9 SDK](https://dotnet.microsoft.com/download)
*   [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Express or Developer edition)
*   A modern web browser (Chrome, Edge, Firefox)

### 1. Clone the Repository
```bash
git clone https://github.com/YousefAliMLS/CoreX-Fitness.git
cd CoreX-Fitness
```

### 2. Backend Setup
Navigate to the server directory and set up the database.

```bash
cd Project
```

1.  **Restore Dependencies:**
    ```bash
    dotnet restore
    ```
2.  **Configure Database:**
    Open `appsettings.json` and update the `DefaultConnection` string with your local SQL Server credentials.
3.  **Apply Migrations:**
    ```bash
    dotnet ef database update
    ```
4.  **Run API:**
    ```bash
    dotnet run
    ```
    _The backend will start (usually on https://localhost:7xxx). Keep this terminal running._

### 3. Frontend Setup
Since the frontend is built with standard web technologies, no build step is required.

1.  Navigate to the `fittrack-client` folder.
2.  **Option A (Simple):** Open the `index.html` file directly in your browser.
3.  **Option B (Recommended):** Use a simple static server (like "Live Server" in VS Code) to serve the `fittrack-client` folder. This avoids common CORS issues when the browser tries to fetch data from the API.

---

## 👥 Team Members

This project was collaboratively developed by:

*   **Yousef Mahmoud Ali** - Fullstack Developer
*   **Mostafa Abd Elhamied** - Frontend Developer
*   **Mahmoud Khaled** - Frontend Developer
*   **SalahEldin Mohamed** - Backend Developer

---

## 📄 License

This project is licensed under the MIT License.
