FitTrack 💪
A modern web application for building workout routines, logging training sessions, and tracking your fitness progress. This project was developed as part of our Software Engineering course, built by a team of junior Computer Science students.

Our goal was to create a full-stack application with a clear separation between a robust backend API and an interactive, user-friendly frontend.

✨ Key Features
👤 User Authentication: Secure registration and login system to keep user data private and personalized.

📚 Customizable Exercise Library: Comes pre-loaded with common exercises, but users can add their own custom movements to the library.

📅 Workout Routine Builder: Easily create, view, update, and delete custom workout plans (e.g., "Push Day", "Leg Day", "Full Body").

✍️ Detailed Session Logging: Log every workout session. For strength exercises, track sets, reps, and weight. For cardio, track distance and duration.

📈 Progress Visualization: The core of the application! Select any exercise and view a dynamic line chart that visualizes your performance and progress over time.

🛠️ Technology Stack
This project is built using a modern technology stack:

Backend:

Framework: .NET 8 (ASP.NET Core Web API)

Database: SQL Server

ORM: Entity Framework Core

Authentication: JWT (JSON Web Tokens)

Frontend:

Framework: React (or your chosen framework like Angular/Vue)

Styling: CSS Modules / Tailwind CSS (or your choice)

API Communication: Axios

Charting: Chart.js or Recharts

Tools:

Git & GitHub for version control.

Visual Studio 2022 / VS Code.

🚀 Getting Started
Follow these instructions to get a local copy of the project up and running for development and testing purposes.

Prerequisites
You will need the following software installed on your machine:

.NET 8 SDK

Node.js (which includes npm)

SQL Server Express or another SQL Server instance.

Installation & Setup
Clone the repository:

Bash

git clone https://github.com/your-github-username/FitTrack.git
cd FitTrack
Backend Setup:

Bash

# Navigate to the backend project directory
cd FitTrack.Api

# Restore NuGet packages
dotnet restore

# Configure your database connection
# Open appsettings.Development.json and update the "DefaultConnection" string with your SQL Server credentials.

# Apply Entity Framework migrations to create the database schema
dotnet ef database update
Frontend Setup:

Bash

# Navigate to the frontend project directory from the root
cd fittrack-client

# Install NPM packages
npm install
Running the Application:

Run the Backend: In the FitTrack.Api directory, run the command:

Bash

dotnet run
The API will be running on https://localhost:7xxx and http://localhost:5xxx.

Run the Frontend: In the fittrack-client directory, run the command:

Bash

npm start
The application will open in your browser at http://localhost:3000.

👥 Team
Yousef Ali - [Your Role, e.g., Frontend / API]

[Team Member 2 Name] - [Role]

[Team Member 3 Name] - [Role]

[Team Member 4 Name] - [Role]

📄 License
This project is licensed under the MIT License - see the LICENSE file for details.
