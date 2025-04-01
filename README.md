# MusicBookingApp
🎵 Music Booking App API
📌 Project Overview
The Music Booking App API is a RESTful API built using C# (.NET 8) and SQL Server to manage artist profiles, event listings, and booking transactions. It provides endpoints for users to book events, artists to manage their performances, and admins to oversee the platform.
________________________________________
🚀 Features
•	User Authentication (JWT-based login & registration)
•	Artist Management (Create and update artist profiles)
•	Event Listings (Create, update, and view events)
•	Booking System (Users can book tickets for events)
•	Role-based Access Control (Admin, Artist, Customer)
________________________________________
🛠️ Tech Stack
•	Backend: C# (.NET 8, ASP.NET Core Web API)
•	Database: SQL Server
•	Authentication: JWT (JSON Web Token)
•	ORM: Entity Framework Core________________________________________
📂 Project Structure
/music-booking-api
│── Controllers/        # API Controllers
│── Models/             # Database Models
│── Data/               # Database Context
│── Migrations/         # EF Core Migrations
│── Program.cs          # Entry Point
│── appsettings.json    # Configurations
________________________________________
📑 Database Schema
1️⃣ Users Table
Column	Type	Constraints
Id	INT	PRIMARY KEY, AUTO_INCREMENT
Name	NVARCHAR(100)	NOT NULL
Email	NVARCHAR(255)	UNIQUE, NOT NULL
PasswordHash	NVARCHAR(255)	NOT NULL
Role	NVARCHAR(20)	CHECK ('Admin', 'Artist', 'Customer')
2️⃣ Artists Table
Column	Type	Constraints
Id	INT	PRIMARY KEY, AUTO_INCREMENT
Name	NVARCHAR(100)	NOT NULL
Bio	NVARCHAR(MAX)	NULL
Genre	NVARCHAR(50)	NOT NULL
UserId	INT	FOREIGN KEY (Users.Id)
3️⃣ Events Table
Column	Type	Constraints
Id	INT	PRIMARY KEY, AUTO_INCREMENT
Name	NVARCHAR(150)	NOT NULL
Date	DATETIME	NOT NULL
Venue	NVARCHAR(255)	NOT NULL
ArtistId	INT	FOREIGN KEY (Artists.Id)
4️⃣ Bookings Table
Column	Type	Constraints
Id	INT	PRIMARY KEY, AUTO_INCREMENT
UserId	INT	FOREIGN KEY (Users.Id)
EventId	INT	FOREIGN KEY (Events.Id)
Status	NVARCHAR(20)	CHECK ('Pending', 'Confirmed', 'Cancelled')
________________________________________
🔥 Installation & Setup
Prerequisites
•	.NET 8 SDK
•	SQL Server
•	Postman (for API testing)
1️⃣ Clone Repository
git clone https://github.com/your-username/music-booking-api.git
cd music-booking-api
2️⃣ Configure Database Connection
Edit appsettings.json:
"ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=MusicBookingDB;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True"
}
3️⃣ Run Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update
4️⃣ Run the API
dotnet run
________________________________________
📌 API Endpoints
1️⃣ Authentication
Method	Endpoint	Description
POST	/api/auth/register	Register a new user
POST	/api/auth/login	User login (JWT)
2️⃣ Artists
Method	Endpoint	Description
GET	/api/artists	Get all artists
POST	/api/artists	Create an artist profile
3️⃣ Events
Method	Endpoint	Description
GET	/api/events	Get all events
POST	/api/events	Create a new event
DELETE	/api/events/{id}	Delete an event
4️⃣ Bookings
Method	Endpoint	Description
GET	/api/bookings	Get all bookings
POST	/api/bookings	Create a booking

