📚 Book Shopping Cart — ASP.NET Core MVC

A full-featured online bookstore built with ASP.NET Core MVC, featuring shopping cart, order management, stock tracking, and role-based access control for Admins and Users.

🛠️ Tech Stack
ASP.NET Core MVC 9
Entity Framework Core 9
SQL Server (LocalDB / MSSQL)
ASP.NET Core Identity
Bootstrap 5


🚀 Features
🔐 Authentication & Authorization using ASP.NET Core Identity
👥 Role-Based Access Control (Admin / User)
📖 Browse books with search and genre filtering
🛒 Shopping cart (Add / Remove / Quantity management)
📦 Checkout system with order creation
📉 Automatic stock deduction after purchase
🧾 Order history for users
🖼️ Image upload for book covers
📊 Admin dashboard for managing books, genres, stock, and orders
🌱 Database seeding for roles and default admin user

🧠 Architecture
N-Tier Architecture
Repository Pattern
DTOs
Dependency Injection
Clean Code Principles

🗃️ Database Relationships
Users (Identity) → Authentication & system access
ShoppingCarts → CartDetails → Books → Each user has a cart with multiple books
Orders → OrderDetails → Books → Each order contains multiple books with quantities
Books → Genres → Each book belongs to one genre
Stocks → Books → Each book has stock tracking

⚙️ Getting Started
git clone https://github.com/your-username/BookShoppingCartMvc.git
# Update connection string in appsettings.json
dotnet ef database update
dotnet run

👥 Roles
Role	Permissions
Admin	Manage books, genres, stock, orders
User	Browse books, cart, checkout, view orders

📌 Note
This project demonstrates backend and full-stack development skills using ASP.NET Core MVC following clean architecture principles.

👨‍💻 Author
Mohamed Samir
