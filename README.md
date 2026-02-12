🔧 Mechanic Shop Management System

📖 Overview

Mechanic Shop is a comprehensive B2B web application designed to digitize and streamline the daily operations of automotive workshops.

The system replaces traditional paper-based workflows with a centralized digital solution, focusing on solving critical business pain points such as scheduling conflicts, double-booking, and inventory tracking. It is built with a strong focus on Domain-Driven Design (DDD) and Clean Architecture principles to ensure scalability and maintainability.

🚀 Key Features

🛠 Operational Management

Work Order Lifecycle: Full tracking of repair jobs from Scheduled → InProgress → Completed.

Smart Scheduling: An intelligent algorithm that prevents double-booking of technicians or service bays.

Auto-Cancellation: Background jobs to automatically cancel "No-Show" appointments after 15 minutes.

👥 Customer & Vehicle Management

Centralized Database: Store and manage customer profiles and vehicle history (VIN, Make, Model).

Service History: View past repairs and invoices for any specific vehicle.

💰 Billing & Invoicing

Automated Invoicing: Generate invoices automatically upon work order completion.

Labor & Parts Calculation: Accurate calculation of total costs based on labor hours and parts used.

📊 Dashboard & Analytics

Real-time Insights: View active jobs, completed orders, and daily revenue stats.

Performance Metrics: Monitor technician utilization and workshop efficiency.

🏗️ Architecture & Technologies

This project is built using Clean Architecture to separate concerns and ensure testability.

Backend: ASP.NET Core 8 Web API

Database: Microsoft SQL Server (EF Core Code-First)

Design Patterns: CQRS (using MediatR), Repository Pattern, Unit of Work, Factory Pattern.

Domain Logic: Domain-Driven Design (DDD) with Aggregates, Entities, and Value Objects.

Validation: FluentValidation pipeline behaviors.

Real-time: SignalR for notifications.

Testing: xUnit & Moq for Unit and Integration tests.

DevOps:

Docker: Containerized database and application.

GitHub Actions: CI/CD pipeline for automated building and testing.

📂 Project Structure

The solution follows the Clean Architecture layers:

Domain: Contains Enterprise Logic, Entities, and Enums (No dependencies).

Application: Contains Business Logic, CQRS Handlers, and Interfaces.

Infrastructure: Implementation of interfaces (Database, File System, Email).

Api: The entry point (Controllers) and presentation layer.

🏃 Getting Started

Prerequisites

.NET 8 SDK

Docker Desktop (Optional, for SQL Server)

SQL Server (Local or Container)

Installation

Clone the repository

git clone [https://github.com/tareq-11/MechanicShop.git](https://github.com/tareq-11/MechanicShop.git)
cd MechanicShop


Setup Database (using Docker)

docker-compose up -d


Apply Migrations

cd src/MechanicShop.Api
dotnet ef database update


Run the Application

dotnet run


🤝 Contributing

Contributions are welcome! Please fork the repository and submit a Pull Request.

Fork the Project

Create your Feature Branch (git checkout -b feature/AmazingFeature)

Commit your Changes (git commit -m 'Add some AmazingFeature')

Push to the Branch (git push origin feature/AmazingFeature)

Open a Pull Request

📝 License

Distributed under the MIT License. See LICENSE for more information.

Developed by Tareq Almahameed

## 📫 Contact

Tareq Almahameed - [**LinkedIn**](https://www.linkedin.com/in/tareq-almahameed) - [**Email**](mailto:tareqalmahameed543@gmail.com)

Project Link: [https://github.com/tareq-11/MechanicShop](https://github.com/tareq-11/MechanicShop)
