# 🌟 WealthHive - Family Budget Management App  

WealthHive is a **family budget management application** designed to help families efficiently plan and manage their finances. The app supports multiple **Admins** (earners) and **Members** (dependents) within a family, allowing seamless tracking of income, expense requests, and contributions. **Admins** oversee budget allocations, approve spending requests, and maintain financial stability within the family's cumulative income.  

---

## 🚀 Key Features  

✅ **Multi-User System** – Supports multiple Admins and Members per family.  
✅ **Income & Expense Tracking** – Manage earnings, contributions, and spending requests.  
✅ **Secure Authentication** – OTP-based signup, JWT authentication, and secure password storage.  
✅ **Role-Based Access** – Admins manage budgets; Members request and track expenses.  
✅ **Logging & Activity Tracking** – Every action is logged in the database.  
✅ **Modern & Elegant UI** – AWS Console-inspired, clean, and professional design.  

---

## 🛠️ Tech Stack  

### **Frontend (React 18 + Redux + Tailwind CSS)**  
- ⚛️ **React 18** – Component-based, fast, and scalable UI.  
- 🌐 **Redux + Thunk** – Efficient state management and async API handling.  
- 🎨 **Tailwind CSS** – Elegant and modern UI styling.  
- 🔐 **JWT Authentication** – Secure token storage and protected routes.  

### **Backend (ASP.NET Core 8 + Entity Framework Core)**  
- ⚡ **ASP.NET Core 8** – High-performance, scalable API.  
- 🏛️ **Entity Framework Core** – Database interaction with Repository Pattern.  
- 📏 **SOLID Principles** – Clean architecture for maintainability.  
- 📜 **Logging to Database** – Tracks user actions and security events.  
- 📑 **Swagger** – API documentation for easy integration.  

### **Database (Microsoft SQL Server)**  
- 🗃️ **Users Table** – Stores Admins and Members with role-based access.  
- 🔑 **OTP Table** – Handles email verification for secure signup.  
- 💰 **Transactions Table** – Tracks income, expenses, and approvals.  

---

## 📌 Setup & Installation  

### **1️⃣ Clone the Repository**  
```sh
git clone https://github.com/yourusername/WealthHive.git
cd WealthHive
