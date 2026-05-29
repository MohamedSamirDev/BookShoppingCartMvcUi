📚 Book Shopping Cart — ASP.NET Core MVC

A full-featured online bookstore built with ASP.NET Core MVC, featuring shopping cart, order management,
stock tracking, and role-based access control for Admins and Users.

| Technology            | Version         |
| --------------------- | --------------- |
| ASP.NET Core MVC      | 9.0             |
| Entity Framework Core | 9.0             |
| SQL Server            | LocalDB / MSSQL |
| ASP.NET Core Identity | 9.0             |
| Bootstrap             | 5               |

🚀 Getting Started
Prerequisites
.NET 9 SDK
SQL Server or LocalDB
Visual Studio 2022 / VS Code

Setup
git clone https://github.com/your-username/BookShoppingCartMvc.git
# Update connection string in appsettings.json
dotnet ef database update
dotnet run

👥 Roles
| Role  | Description                                |
| ----- | ------------------------------------------ |
| Admin | Full access (books, genres, stock, orders) |
| User  | Browse books, cart, checkout, view orders  |

🔐 Authentication & Authorization
Implemented using ASP.NET Core Identity (Microsoft Identity System)
User registration and login handled by Identity framework
Role-based access control (Admin / User)
Secure authentication with built-in security features

✅ Features
🔐 Authentication & Authorization using ASP.NET Core Identity
👥 Role-Based Access Control (Admin / User)
📖 Browse books with search & genre filtering
🛒 Shopping cart (Add / Remove / Quantity management)
📦 Checkout flow with order creation
📉 Automatic stock deduction after purchase
🧾 User order history
🖼️ Book cover image upload
📊 Admin dashboard for managing books, genres, stock, and orders
🌱 Auto seed roles and admin user on startup

🧠 Architecture
N-Tier Architecture
Repository Pattern
DTOs
Dependency Injection
Clean Code Principles

📋 Pages & Routes
| Route | Description  |
| ----- | ------------ |
| `/`   | Browse books |

🛒 Cart (Auth required)
| Route               | Description |
| ------------------- | ----------- |
| `/Cart/AddItem`     | Add book    |
| `/Cart/RemoveItem`  | Remove book |
| `/Cart/GetUserCart` | View cart   |
| `/Cart/Checkout`    | Place order |

📦 Orders
| Route                  | Description |
| ---------------------- | ----------- |
| `/UserOrder/UserOrder` | User orders |

📚 Admin
| Route                       | Description   |
| --------------------------- | ------------- |
| `/Book/Index`               | Manage books  |
| `/Genre/Index`              | Manage genres |
| `/Stock/Index`              | Manage stock  |
| `/AdminOperation/AllOrders` | Manage orders |

🗃️ Database Schema
Users (Identity)
├── ShoppingCarts → CartDetails → Books
├── Orders → OrderDetails → Books
├── Books → Genres
└── Stocks → Books

👨‍💻 Author
Mohamed Samir
