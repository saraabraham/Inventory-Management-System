# Inventory Management System

A full-stack inventory management system built with React, TypeScript, .NET 8, and SQLite. Designed for furniture retail operations with real-time stock tracking, low-stock alerts, and comprehensive reporting features.


## 📋 Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [API Documentation](#api-documentation)
- [Key Features Explained](#key-features-explained)


## ✨ Features

### Core Functionality
- ✅ **Product Management** - Complete CRUD operations for inventory items
- ✅ **Real-time Stock Tracking** - Monitor inventory levels with instant updates
- ✅ **Low Stock Alerts** - Automatic notifications when items reach minimum threshold
- ✅ **Multi-Supplier Support** - Manage products from different suppliers
- ✅ **Warehouse Location Tracking** - Track physical location of items

### Advanced Features
- 📊 **Interactive Dashboard** - Visual analytics with charts and key metrics
- 📈 **Stock Movement History** - Complete audit trail of all transactions
- 🔍 **Advanced Search & Filters** - Filter by category, price range, stock status
- 📤 **CSV Export** - Download inventory data for reporting
- 📥 **Bulk CSV Import** - Upload multiple products at once
- 🖼️ **Product Images** - Visual product catalog with image support
- 🔖 **Barcode Generation** - Generate and print product barcodes
- 🔄 **Reorder Suggestions** - Smart restocking recommendations based on stock levels

## 🛠️ Tech Stack

### Frontend
- **React 18** - Modern UI library with hooks
- **TypeScript** - Type-safe JavaScript
- **Tailwind CSS** - Utility-first styling
- **React Router** - Client-side routing
- **Axios** - HTTP client for API calls
- **Recharts** - Data visualization
- **Lucide React** - Beautiful icons

### Backend
- **.NET 8 Web API** - RESTful API with C#
- **Entity Framework Core** - ORM for database operations
- **SQLite** - Lightweight, file-based database
- **Repository Pattern** - Clean architecture implementation
- **Swagger/OpenAPI** - API documentation

### Development Tools
- **Git** - Version control
- **VS Code / Visual Studio** - IDEs
- **Postman** - API testing (optional)

## 🚀 Getting Started

### Prerequisites

- [Node.js](https://nodejs.org/) (v18 or higher)
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Git](https://git-scm.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/ikea-inventory-system.git
   cd ikea-inventory-system
   ```

2. **Backend Setup**
   ```bash
   cd backend/InventoryAPI
   
   # Restore dependencies
   dotnet restore
   
   # Create database and seed data
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   
   # Run the API
   dotnet run
   ```
   The API will be available at `http://localhost:5174`

3. **Frontend Setup**
   ```bash
   cd frontend/inventory-client
   
   # Install dependencies
   npm install
   
   # Start development server
   npm start
   ```
   The app will be available at `http://localhost:3000`

4. **Access the Application**
   - Frontend: http://localhost:3000
   - Backend API: http://localhost:5174/api
   - Swagger UI: http://localhost:5174/swagger

## 📁 Project Structure

```
ikea-inventory-system/
├── backend/
│   └── InventoryAPI/
│       ├── Controllers/          # API endpoints
│       ├── Models/               # Domain entities
│       ├── DTOs/                 # Data transfer objects
│       ├── Data/                 # Database context & migrations
│       ├── Repositories/         # Data access layer
│       └── Program.cs            # App configuration
│
├── frontend/
│   └── inventory-client/
│       ├── public/               # Static files
│       └── src/
│           ├── components/       # React components
│           ├── services/         # API service layer
│           ├── App.tsx           # Main app component
│           └── index.tsx         # Entry point
│
└── docs/                         # Documentation & screenshots
```

## 📚 API Documentation

### Endpoints

#### Products
```http
GET    /api/products              # Get all products
GET    /api/products/{id}         # Get product by ID
GET    /api/products/low-stock    # Get low stock products
GET    /api/products/export       # Export to CSV
POST   /api/products              # Create new product
POST   /api/products/import       # Bulk import from CSV
PUT    /api/products/{id}         # Update product
PATCH  /api/products/{id}/stock   # Adjust stock quantity
DELETE /api/products/{id}         # Delete product
```

#### Transactions
```http
GET    /api/transactions                    # Get all transactions
GET    /api/transactions/product/{id}       # Get product transactions
POST   /api/transactions                    # Create transaction
```

#### Reorder Suggestions
```http
GET    /api/products/reorder-suggestions    # Get reorder recommendations
```

### Sample Request/Response

**Create Product**
```http
POST /api/products
Content-Type: application/json

{
  "sku": "BILLY-001",
  "name": "BILLY Bookcase",
  "description": "Adjustable shelves",
  "category": "Storage",
  "price": 79.99,
  "stockQuantity": 150,
  "minimumStock": 20,
  "supplierId": 1,
  "location": "Warehouse A-1",
  "imageUrl": "https://example.com/billy.jpg"
}
```

**Response**
```json
{
  "id": 1,
  "sku": "BILLY-001",
  "name": "BILLY Bookcase",
  "category": "Storage",
  "price": 79.99,
  "stockQuantity": 150,
  "minimumStock": 20,
  "isLowStock": false,
  "supplierName": "Scandinavian Furniture Co.",
  "location": "Warehouse A-1"
}
```

## 🎯 Key Features Explained

### 1. Low Stock Alert System
Automatically monitors inventory levels and highlights products that fall below their minimum stock threshold. The dashboard displays a real-time count and detailed list of affected items.

### 2. Stock Transaction History
Every stock change is logged with:
- Transaction type (Purchase, Sale, Adjustment, Return)
- Quantity changed
- Timestamp
- User who performed the action
- Optional notes and reference numbers

### 3. Reorder Suggestions Algorithm
Smart algorithm that:
- Identifies products below 150% of minimum stock
- Calculates optimal order quantity (enough to reach 200% of minimum)
- Prioritizes by urgency (Critical, High, Medium)
- Estimates total cost
- Groups by supplier for efficient ordering

### 4. Bulk Import/Export
- **Export**: Downloads current inventory as CSV for reporting
- **Import**: Upload CSV to add/update multiple products
- Validates data and provides detailed error reporting
- Maintains referential integrity

### 5. Barcode System
- Generates CODE128 barcodes from SKU
- Print-ready format for labels
- Downloadable as PNG images
- Integrates with product details page


## 🙏 Acknowledgments

- IKEA for design inspiration
- React and .NET communities for excellent documentation
- All open-source contributors whose libraries made this possible

---

