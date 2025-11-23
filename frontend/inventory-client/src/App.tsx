import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link, Navigate } from 'react-router-dom';
import { LayoutDashboard, Package, Home } from 'lucide-react';
import Dashboard from './components/Dashboard';
import ProductList from './components/ProductList';
import ProductForm from './components/ProductForm';
import TransactionHistory from './components/TransactionHistory';
import { History } from 'lucide-react';
import ProductDetails from './components/ProductDetails';

function App() {

  return (
    <Router>
      <div className="min-h-screen bg-gray-100">
        {/* Navigation */}
        <nav className="bg-ikea-blue text-white shadow-lg">
          <div className="container mx-auto px-4">
            <div className="flex items-center justify-between h-16">
              <div className="flex items-center space-x-8">
                <h1 className="text-2xl font-bold">IKEA Inventory</h1>
                <div className="flex space-x-4">
                  <Link
                    to="/dashboard"
                    className="flex items-center px-3 py-2 rounded hover:bg-blue-700 transition"
                  >
                    <LayoutDashboard className="w-5 h-5 mr-2" />
                    Dashboard
                  </Link>
                  <Link
                    to="/products"
                    className="flex items-center px-3 py-2 rounded hover:bg-blue-700 transition"
                  >
                    <Package className="w-5 h-5 mr-2" />
                    Products
                  </Link>
                  <Link
                    to="/transactions"
                    className="flex items-center px-3 py-2 rounded hover:bg-blue-700 transition"
                  >
                    <History className="w-5 h-5 mr-2" />
                    Transactions
                  </Link>
                </div>
              </div>
            </div>
          </div>
        </nav>

        {/* Main Content */}
        <main className="container mx-auto">
          <Routes>
            <Route path="/" element={<Navigate to="/dashboard" replace />} />
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/products" element={<ProductList />} />
            <Route path="/products/new" element={<ProductForm />} />
            <Route path="/products/edit/:id" element={<ProductForm />} />
            <Route path="/transactions" element={<TransactionHistory />} />
            <Route path="/products/:id/details" element={<ProductDetails />} />
          </Routes>
        </main>
      </div>
    </Router>
  );
}

export default App;