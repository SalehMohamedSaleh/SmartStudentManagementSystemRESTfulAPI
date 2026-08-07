# Smart Student Management System RESTful API

A professional **Student Management RESTful API** built using **ASP.NET Core Web API** to help educational institutions manage students, courses, grades, and attendance through a secure and scalable backend system.

This project was developed as a **Graduation Project** and designed following modern backend development practices to demonstrate skills in **ASP.NET Core, Entity Framework Core, REST API Design, Authentication, Authorization, and Database Modeling**.

---

## 📌 Project Overview

The Smart Student Management System provides a complete backend solution for managing academic operations.

The system allows administrators, teachers, and students to interact with the platform based on their roles and permissions.

### Main Goals

* Build a secure RESTful API for educational management.
* Apply clean backend architecture principles.
* Implement authentication and role-based authorization.
* Provide efficient data management using Entity Framework Core.
* Create a scalable and maintainable backend system.

---

# ✨ Features

## 🔐 Authentication & Authorization

* User registration and login.
* JWT-based authentication.
* Role-based authorization.
* User roles:

  * Admin
  * Teacher
  * Student

---

## 👨‍🎓 Student Management

Administrators can:

* Create students.
* Update student information.
* View student details.
* Soft delete students.
* Assign students to classes and courses.
* Manage student profiles.

---

## 📚 Course Management

Administrators can:

* Create courses.
* Update courses.
* Delete courses.
* View course details.
* Assign teachers to courses.

---

## 📝 Grade Management

Teachers can:

* Add grades for students.
* Update grades.
* View student grades.

Students can:

* View their own grades only.

Supported grade types:

* Quiz
* Assignment
* Midterm
* Final

---

## 🕒 Attendance Tracking

Teachers can:

* Record daily attendance.
* Update attendance status.

Students can:

* View their attendance history.

Attendance statuses:

* Present
* Absent
* Late

---

## 👨‍💼 Admin Features

Administrators can:

* Manage users.
* Manage roles.
* View system statistics.
* Control system data.

---

# 🏗️ System Architecture

The project follows a layered architecture approach:

```
SmartStudentManagementSystem

│
├── API
│   ├── Controllers
│   ├── Middleware
│   └── Configuration
│
├── Application
│   ├── Services
│   ├── DTOs
│   ├── Validators
│   └── Mapping Profiles
│
├── Domain
│   ├── Entities
│   ├── Enums
│   └── Interfaces
│
└── Infrastructure
    ├── Database
    ├── Entity Configurations
    └── Identity
```

---

# 🛠️ Technologies Used

## Backend

* ASP.NET Core Web API
* C#
* Entity Framework Core
* LINQ
* AutoMapper
* FluentValidation

## Database

* SQL Server

## Security

* ASP.NET Core Identity
* JWT Authentication
* Role-Based Authorization

## Documentation & Testing

* Swagger / OpenAPI
* Postman Collection

---

# 🗄️ Database Design

Main entities:

```
Student
   |
Enrollment
   |
Course

Enrollment
   |
Grade

Enrollment
   |
Attendance


Teacher
   |
CourseInstructor
   |
Course
```

### Main Relationships

* Student → Enrollment → Course
* Teacher → Course (Many-to-Many)
* Enrollment → Grades
* Enrollment → Attendance

---

# 🔑 Authentication Flow

1. User registers.
2. User logs in.
3. API validates credentials.
4. JWT token is generated.
5. Client sends token with every protected request.

Example:

```
Authorization: Bearer {token}
```

---

# 📂 Project Structure

```
SmartStudentManagementSystem
│
├── Controllers
├── Services
├── Models
├── DTOs
├── Data
├── Configurations
├── Migrations
└── Program.cs
```

---

# 🚀 Getting Started

## Prerequisites

Make sure you have installed:

* .NET SDK
* SQL Server
* Visual Studio 2022 or VS Code

---

## Installation

Clone the repository:

```bash
git clone https://github.com/SalehMohamedSaleh/SmartStudentManagementSystemRESTfulAPI.git
```

Navigate to the project:

```bash
cd SmartStudentManagementSystemRESTfulAPI
```

---

## Database Setup

Update your connection string in:

```
appsettings.json
```

Run migrations:

```bash
dotnet ef database update
```

---

## Run Application

```bash
dotnet run
```

The API will be available through Swagger:

```
https://localhost:<port>/swagger
```

---

# 📖 API Documentation

Swagger is included to provide interactive API documentation.

Available endpoints:

```
Authentication
    POST /api/auth/register
    POST /api/auth/login

Students
    GET
    POST
    PUT
    DELETE

Courses
    GET
    POST
    PUT
    DELETE

Grades
    GET
    POST
    PUT

Attendance
    GET
    POST
```

---

# 🧪 Testing

API testing can be performed using:

* Swagger UI
* Postman

A complete Postman collection is included.

---

# 🔒 Security Considerations

The project applies:

* JWT authentication.
* Role-based authorization.
* Input validation.
* Secure password hashing using Identity.
* Database constraints.
* Exception handling.

---

# 📈 Future Improvements

Possible enhancements:

* Add refresh tokens.
* Add email verification.
* Add file storage for student images.
* Add Redis caching.
* Add automated unit testing.
* Add Docker support.
* Add CI/CD pipeline.
* Add logging with Serilog.

---

# 👨‍💻 Author

**Saleh Mohamed Saleh**

Backend Developer

Skills:

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server
* RESTful API Design
* Clean Architecture

---

# ⭐ If you find this project useful

Feel free to give it a star ⭐ and share your feedback.
