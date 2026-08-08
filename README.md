# Smart Student Management System RESTful API

A professional **Student Management RESTful API** built using **ASP.NET Core Web API** to help educational institutions manage students, courses, grades, and attendance through a secure and scalable backend system.

This project was developed as a **Graduation Project** and designed following modern backend development practices to demonstrate skills in **ASP.NET Core, Entity Framework Core, REST API Design, Authentication, Authorization, and Database Modeling**.

---

## 📌 Project Overview

The Smart Student Management System provides a complete backend solution for managing academic operations.

The system allows administrators, teachers, and students to interact with the platform based on their roles and permissions.

### Main Goals

- Build a secure RESTful API for educational management.
- Apply layered architecture and separation of concerns.
- Implement authentication and role-based authorization.
- Provide efficient data management using Entity Framework Core.
- Create a scalable and maintainable backend system.

---

# ✨ Features

## 🔐 Authentication & Authorization

- User registration and login.
- JWT-based authentication.
- Role-based authorization.
- User roles:
  - Admin
  - Teacher
  - Student

---

## 👨‍🎓 Student Management

Administrators can:

- Create students.
- Update student information.
- View student details.
- Soft delete students.
- Assign students to classes and courses.
- Manage student profiles.

---

## 📚 Course Management

Administrators can:

- Create courses.
- Update courses.
- Delete courses.
- View course details.
- Assign teachers to courses.

---

## 📝 Grade Management

Teachers can:

- Add grades for students.
- Update grades.
- View student grades.

Students can:

- View their own grades only.

Supported grade types:

- Quiz
- Assignment
- Midterm
- Final

---

## 🕒 Attendance Tracking

Teachers can:

- Record daily attendance.
- Update attendance status.

Students can:

- View their attendance history.

Attendance statuses:

- Present
- Absent
- Late

---

## 👨‍💼 Admin Features

Administrators can:

- Manage users.
- Manage roles.
- View system statistics.
- Control system data.

---

# 🏗️ System Architecture

The project follows a **Layered Architecture approach** with separation of concerns between controllers, business logic, domain models, infrastructure, and supporting components.

```text
SmartStudentManagementSystem
│
├── Controllers
│   └── API Controllers
│
├── Domain
│   ├── Entities
│   ├── Enums
│   └── Identity
│
├── Dtos
│   └── Data Transfer Objects
│
├── Infrastructure
│   ├── Configurations
│   ├── Seeders
│   ├── JwtSettings.cs
│   └── SchoolDbContext.cs
│
├── Mapping
│   └── AutoMapper Profiles
│
├── Middlewares
│   └── Custom Middleware Components
│
├── Migrations
│   └── Entity Framework Core Migrations
│
├── Services
│   └── Business Logic
│
├── appsettings.json
│
└── Program.cs
