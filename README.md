# 🎟️ Event Booking System

A full-stack web application for users to browse, book, and manage event tickets, with a powerful admin dashboard for managing events and tracking insights.

---

## 📌 Project Overview

This project is a modern, scalable Event Booking System built using:

- **Backend**: .NET 8 with Clean Architecture (Domain-Driven Design)
- **Frontend**: Angular 18
- **Authentication**: JWT, Google Sign-In, Email Confirmation, Password Reset
- **Admin Panel**: Analytics, Event Management (CRUD), User Stats
- **User Panel**: Event search, filtering, booking
- **Extra Features**: CQRS, MediatR, AutoMapper, Serilog, FluentValidation, xUnit Testing

---

## 📁 Folder Structure

### 🔧 Backend (.NET API)

```text
EventBookingSystem/
├── API/             # ASP.NET Core Web API Layer (controllers, middlewares)
├── Application/     # Application Layer (CQRS handlers, DTOs, interfaces)
├── Domain/          # Domain Layer (Entities, Enums, Interfaces, Exceptions)
├── Infrastructure/  # Infrastructure Layer (DB context, Repositories, Identity)
├── Tests/           # Unit tests with xUnit & FluentAssertions
```

### 🎨 Frontend (Angular)
```text
event-booking-angular/
├── src/
│ ├── app/
│ │ ├── core/ # Auth services, interceptors, guards
│ │ ├── shared/ # Shared UI components
│ │ ├── user/ # Event listing, booking, filters
│ │ ├── admin/ # Dashboard, charts, event CRUD
│ │ └── pages/ # Auth pages, home, events, booking success
```

---

## 🚀 Features

### 🔐 Authentication & Authorization

- JWT Authentication with Role-based Authorization (Admin/User)
- Google Sign-In
- Email confirmation during registration
- Password reset via email

### 🧑‍💻 Admin Panel

- Add, update, delete events
- View total number of users, events, and revenue
- Data visualization with Chart.js
- Responsive dashboard with role-based access

### 👥 User Functionality

- Event listing with filtering (price, ticket availability)
- Search events by name or description
- Book tickets (only once per event)
- "Booked" status replaces "Book Now" on booked events
- Redirect to a success page after booking

---

## 🛠️ Tech Stack

### Backend

- ASP.NET Core 8
- Clean Architecture + DDD
- Entity Framework Core
- FluentValidation
- CQRS with MediatR
- AutoMapper
- Serilog (Logging)
- xUnit + FluentAssertions (Unit Testing)
- Identity + JWT + Google OAuth

### Frontend

- Angular 18
- Angular Material UI
- Chart.js
- RxJS
- ngx-toastr
- Angular Forms & Reactive Forms

---

## 🧪 Testing

- ✅ Unit Testing with **xUnit**
- ✅ Assertions using **FluentAssertions**
- ✅ Separated test project under `/Tests`

---

## 🔧 Setup Instructions

### 🔙 Backend

1. **Clone the repository**:
   ```bash
   git clone git@github.com:mostafasharaby/Event-Booking-System.git
   cd EventBookingSystem
2.  Configure the database connection string => Update the appsettings.json file with your SQL Server connection string.
3.  Apply migrations
4.  ```bash
     dotnet ef database update
    ```
5. Run the API
   ```bash
    dotnet run
    ```
6.  ✅ The API will run at: https://localhost:7146

### 🔜 Frontend (Angular)

1. **Navigate to the frontend directory**
    ```bash
    cd front-end
    ```
2. **Install dependencies**
   ```bash
    npm install
    ```
3. **Run Angular Development Server**
  ```bash
  ng serve
  ```
4. ✅ The app will be available at: http://localhost:4200
