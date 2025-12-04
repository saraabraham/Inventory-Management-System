import React, { Suspense, lazy } from 'react';
import { BrowserRouter as Router, Routes, Route, Link, Navigate } from 'react-router-dom';
import { LayoutDashboard, Package, History } from 'lucide-react';

// Lazy load all route components to reduce initial bundle size
const Dashboard = lazy(() => import('./components/Dashboard'));
const ProductList = lazy(() => import('./components/ProductList'));
const ProductForm = lazy(() => import('./components/ProductForm'));
const TransactionHistory = lazy(() => import('./components/TransactionHistory'));
const ProductDetails = lazy(() => import('./components/ProductDetails'));
const ReorderSuggestions = lazy(() => import('./components/ReorderSuggestions'));

// Loading component for better UX
const LoadingSpinner = () => (
  <div className="flex justify-center items-center h-64">
    <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-ikea-blue"></div>
  </div>
);

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
                  <Link
                    to="/reorder"
                    className="flex items-center px-3 py-2 rounded hover:bg-blue-700 transition"
                  >
                    <Package className="w-5 h-5 mr-2" />
                    Reorder
                  </Link>
                </div>
              </div>
            </div>
          </div>
        </nav>

        {/* Main Content with Suspense boundary */}
        <main className="container mx-auto">
          <Suspense fallback={<LoadingSpinner />}>
            <Routes>
              <Route path="/" element={<Navigate to="/dashboard" replace />} />
              <Route path="/dashboard" element={<Dashboard />} />
              <Route path="/products" element={<ProductList />} />
              <Route path="/products/new" element={<ProductForm />} />
              <Route path="/products/edit/:id" element={<ProductForm />} />
              <Route path="/transactions" element={<TransactionHistory />} />
              <Route path="/products/:id/details" element={<ProductDetails />} />
              <Route path="/reorder" element={<ReorderSuggestions />} />
            </Routes>
          </Suspense>
        </main>
      </div>
    </Router>
  );
}

export default App;