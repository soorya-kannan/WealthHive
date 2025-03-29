Project Name: WealthHive
Description:
WealthHive is a family budget management application designed to help families efficiently plan and manage their finances. The app allows multiple Admins (earners) and Members (dependents) within a family to track income, request expenses, and contribute money. Admins oversee budget allocations, approve spending requests, and maintain financial stability within the family's cumulative income.

Key Features:
✅ Multi-User System: Supports multiple Admins and Members per family.
✅ Income & Expense Tracking: Manage earnings, contributions, and spending requests.
✅ Secure Authentication: OTP-based signup, JWT authentication, and secure password storage.
✅ Role-Based Access: Admins manage budgets; Members request and track expenses.
✅ Logging & Activity Tracking: Every action is logged in the database.
✅ Modern & Elegant UI: Inspired by AWS Console for a clean, professional look.

Tech Stack:
Frontend (React 18 + Redux + Tailwind CSS)
React 18 – Component-based, fast, and scalable UI.

Redux + Thunk – State management and async API handling.

Tailwind CSS – Elegant, modern UI with AWS Console-inspired styling.

JWT Authentication – Secure token storage and protected routes.

Backend (ASP.NET Core 8 + Entity Framework Core)
ASP.NET Core 8 – High-performance, scalable API.

Entity Framework Core – Database interaction with Repository Pattern.

SOLID Principles – Clean architecture for maintainability.

Logging to Database – Tracks user actions and security events.

Swagger – API documentation for easy integration.

Database (Microsoft SQL Server)
Users Table: Stores Admins and Members with role-based access.

OTP Table: Handles email verification for secure signup.

Transactions Table: Tracks income, expenses, and approvals.
