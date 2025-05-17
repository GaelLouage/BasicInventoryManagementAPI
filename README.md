# 📦 BasicInventoryManagementAPI

A simple ASP.NET Core Web API for managing product inventory using a JSON file as the data store.

This project demonstrates clean code practices, file-based persistence, and minimal API routing using .NET 7/8. It's designed for small-scale inventory management and job-readiness practice.

---

## 🚀 Features

- ✅ Add new products or update existing ones by name
- 📋 Get all products
- ⚠️ Fetch products with low stock (e.g., quantity < 5)
- 🧠 Smart duplicate check: if product exists, increase quantity
- 💾 File-based JSON persistence
- 🧩 Clean architecture using:
  - Services
  - DTOs
  - Helpers
  - Extension Methods

---

## 🛠 Technologies Used

- .NET 7 / 8
- ASP.NET Core Minimal APIs
- Newtonsoft.Json
- Dependency Injection (`IOptions<T>`)
- Swagger / OpenAPI

---

## 📁 Project Structure

```bash
├── Infra
│   ├── Models/
│   ├── Services/
│   ├── Dtos/
│   ├── Mappers/
│   ├── Helpers/
│   └── Extensions/
├── Program.cs
├── appsettings.json
├── products.json

⚙️ Configuration

Edit the file path in appsettings.json to match your local products.json file:

"ProductService": {
  "FilePath": "C:\\path\\to\\your\\products.json"
}

📬 API Endpoints
Method	Route	Description
GET	/products	Returns all products
GET	/low-stock	Returns products with low quantity
POST	/add-product	Adds a new product or updates quantity

Sample POST /add-product Body:

{
  "name": "Laptop",
  "quantity": 3,
  "price": 999.99
}

▶️ Running the Project

# From project root
dotnet run

Then open: https://localhost:<port>/swagger
📌 Example JSON File (products.json)

[
  {
    "id": 1,
    "name": "Mouse",
    "quantity": 10,
    "price": 25.99
  }
]
