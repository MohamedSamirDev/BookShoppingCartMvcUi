# 📚 Book Shopping Cart — ASP.NET Core MVC

A complete online bookstore built with **ASP.NET Core 9 MVC**, featuring secure authentication, shopping cart management, order processing, inventory tracking, and an admin dashboard.

---

## 🎥 Project Demo

Watch the full project walkthrough here:

**▶️ Demo Video:** [Watch Here](https://drive.google.com/file/d/1uUk7aRHsDxnPJRJ3fNEhaEhxvtgSEebA/view?usp=drivesdk)

---

## 🛠️ Technology Stack

| Technology | Version |
|------------|---------|
| ASP.NET Core MVC | 9.0 |
| Entity Framework Core | 9.0 |
| SQL Server | LocalDB / MSSQL |
| ASP.NET Core Identity | 9.0 |
| Bootstrap | 5 |
| HTML5 | Latest |
| CSS3 | Latest |
| JavaScript | ES6 |

---

## 🚀 Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server or LocalDB
- Visual Studio 2022 / VS Code

### Setup

```bash
git clone https://github.com/MohamedSamirDev/BookShoppingCartMvcUi.git
cd BookShoppingCartMvcUi/BookShoppingCartMvc

dotnet restore
dotnet tool install --global dotnet-ef   
dotnet ef database update
dotnet run
```

> **Note:** Update the connection string in `appsettings.json` before running the project.

---

## 👥 Roles

| Role | Description |
|------|-------------|
| **Admin** | Manage books, genres, stock, orders, and access the admin dashboard. |
| **User** | Browse books, manage the shopping cart, complete checkout, and view order history. |

---

## 🔐 Authentication & Authorization

Authentication and authorization are implemented using **ASP.NET Core Identity**.

- User Registration & Login
- Secure Password Hashing
- Role-Based Authorization (Admin / User)
- ASP.NET Core Identity Security

---

## ✅ Features

- 🔐 Authentication & Authorization using ASP.NET Core Identity
- 👥 Role-Based Access Control (Admin / User)
- 📚 Browse books with search and genre filtering
- 🛒 Shopping Cart Management
- ➕ Add / Remove items from the cart
- 🔄 Update item quantity
- 📦 Checkout process with order creation
- 📉 Automatic stock deduction after purchase
- 🧾 User order history
- 🖼️ Book cover image upload
- 📚 Book Management
- 🏷️ Genre Management
- 📦 Stock Management
- 📊 Admin Dashboard
- 🌱 Automatic database seeding for roles and admin user

---

## 🧠 Architecture

This project follows modern ASP.NET Core development practices:

- N-Tier Architecture
- Repository Pattern
- Dependency Injection
- Entity Framework Core
- ASP.NET Core Identity
- Clean Code Principles

---

## 📂 Project Structure

```text
BookShoppingCartMvcUi
│
├── Controllers
├── Models
├── Repositories
├── Data
├── DTOs
├── Views
├── wwwroot
└── Program.cs
```

---

## 🚀 Future Improvements

- 💳 Payment Gateway Integration
- ❤️ Wishlist
- ⭐ Product Reviews & Ratings
- 📧 Email Notifications
- 🐳 Docker Support
- 🧪 Unit Testing

---

## 👨‍💻 Author

**Mohamed Samir**

Junior .NET Backend Developer

- 🐙 GitHub: https://github.com/MohamedSamirDev
- 💼 LinkedIn: https://www.linkedin.com/in/mohamed-samir-4014a1311/
- 📧 Email: mohamedsamir6101@gmail.com

---

⭐ If you found this project helpful, consider giving it a **Star** on GitHub!
