import React, { useEffect, useState, useMemo, Suspense, lazy } from 'react';
import { Product, productService } from '../services/api';
import { Package, AlertTriangle, TrendingUp, DollarSign } from 'lucide-react';

// Lazy load charts component to reduce initial bundle
const DashboardCharts = lazy(() => import('./DashboardCharts'));

const Dashboard: React.FC = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [lowStockProducts, setLowStockProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        loadData();
    }, []);

    const loadData = async () => {
        try {
            // TODO: Replace with dedicated /api/dashboard endpoint for better performance
            const [productsRes, lowStockRes] = await Promise.all([
                productService.getAll(),
                productService.getLowStock(),
            ]);
            setProducts(productsRes.data);
            setLowStockProducts(lowStockRes.data);
        } catch (error) {
            console.error('Error loading data:', error);
        } finally {
            setLoading(false);
        }
    };

    // Memoize expensive calculations to avoid recomputing on every render
    const stats = useMemo(() => {
        const totalValue = products.reduce((sum, p) => sum + (p.price * p.stockQuantity), 0);
        const totalItems = products.reduce((sum, p) => sum + p.stockQuantity, 0);

        const categoryDistribution = products.reduce((acc, product) => {
            acc[product.category] = (acc[product.category] || 0) + 1;
            return acc;
        }, {} as Record<string, number>);

        return {
            totalValue,
            totalItems,
            categoryDistribution,
            productCount: products.length
        };
    }, [products]);

    // Improved loading state with skeleton UI
    if (loading) {
        return (
            <div className="p-6">
                <h1 className="text-3xl font-bold text-ikea-blue mb-6">Inventory Dashboard</h1>

                {/* Skeleton loading for stats cards */}
                <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
                    {[1, 2, 3, 4].map(i => (
                        <div key={i} className="bg-white rounded-lg shadow p-6 animate-pulse">
                            <div className="h-4 bg-gray-200 rounded w-3/4 mb-4"></div>
                            <div className="h-8 bg-gray-200 rounded w-1/2 mb-2"></div>
                            <div className="h-3 bg-gray-200 rounded w-2/3"></div>
                        </div>
                    ))}
                </div>

                {/* Skeleton for charts */}
                <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                    <div className="bg-white rounded-lg shadow p-6 animate-pulse">
                        <div className="h-6 bg-gray-200 rounded w-1/2 mb-4"></div>
                        <div className="h-64 bg-gray-100 rounded"></div>
                    </div>
                    <div className="bg-white rounded-lg shadow p-6 animate-pulse">
                        <div className="h-6 bg-gray-200 rounded w-1/2 mb-4"></div>
                        <div className="h-64 bg-gray-100 rounded"></div>
                    </div>
                </div>
            </div>
        );
    }

    return (
        <div className="p-6">
            <h1 className="text-3xl font-bold text-ikea-blue mb-6">Inventory Dashboard</h1>

            {/* Stats Cards - Load immediately as they're critical */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
                <div className="bg-white rounded-lg shadow p-6">
                    <div className="flex items-center justify-between">
                        <div>
                            <p className="text-gray-600 text-sm">Total Products</p>
                            <p className="text-2xl font-bold text-ikea-blue">{stats.productCount}</p>
                        </div>
                        <Package className="w-12 h-12 text-ikea-blue opacity-20" />
                    </div>
                </div>

                <div className="bg-white rounded-lg shadow p-6">
                    <div className="flex items-center justify-between">
                        <div>
                            <p className="text-gray-600 text-sm">Total Items</p>
                            <p className="text-2xl font-bold text-ikea-blue">{stats.totalItems}</p>
                        </div>
                        <TrendingUp className="w-12 h-12 text-ikea-blue opacity-20" />
                    </div>
                </div>

                <div className="bg-white rounded-lg shadow p-6">
                    <div className="flex items-center justify-between">
                        <div>
                            <p className="text-gray-600 text-sm">Total Value</p>
                            <p className="text-2xl font-bold text-ikea-blue">
                                ${stats.totalValue.toLocaleString()}
                            </p>
                        </div>
                        <DollarSign className="w-12 h-12 text-ikea-blue opacity-20" />
                    </div>
                </div>

                <div className="bg-white rounded-lg shadow p-6">
                    <div className="flex items-center justify-between">
                        <div>
                            <p className="text-gray-600 text-sm">Low Stock Items</p>
                            <p className="text-2xl font-bold text-red-600">{lowStockProducts.length}</p>
                        </div>
                        <AlertTriangle className="w-12 h-12 text-red-600 opacity-20" />
                    </div>
                </div>
            </div>

            {/* Low Stock Alert */}
            {lowStockProducts.length > 0 && (
                <div className="bg-red-50 border-l-4 border-red-500 p-4 mb-8 rounded">
                    <div className="flex items-start">
                        <AlertTriangle className="w-6 h-6 text-red-600 mr-3 flex-shrink-0" />
                        <div className="flex-1">
                            <h3 className="text-red-800 font-semibold">Low Stock Alert</h3>
                            <p className="text-red-700 text-sm">
                                {lowStockProducts.length} product(s) are running low on stock
                            </p>
                        </div>
                    </div>
                    <div className="mt-3">
                        {lowStockProducts.slice(0, 5).map(product => (
                            <div key={product.id} className="text-sm text-red-700 py-1">
                                • {product.name} - Only {product.stockQuantity} units left (Min: {product.minimumStock})
                            </div>
                        ))}
                        {lowStockProducts.length > 5 && (
                            <div className="text-sm text-red-600 py-1 font-semibold">
                                + {lowStockProducts.length - 5} more items
                            </div>
                        )}
                    </div>
                </div>
            )}

            {/* Category Distribution - Simple version without charts */}
            <div className="bg-white rounded-lg shadow p-6 mb-8">
                <h2 className="text-xl font-semibold text-ikea-blue mb-4">Products by Category</h2>
                <div className="space-y-3">
                    {Object.entries(stats.categoryDistribution).map(([category, count]) => (
                        <div key={category} className="flex items-center justify-between">
                            <span className="text-gray-700">{category}</span>
                            <div className="flex items-center">
                                <div className="w-32 bg-gray-200 rounded-full h-2 mr-3">
                                    <div
                                        className="bg-ikea-blue h-2 rounded-full transition-all duration-300"
                                        style={{ width: `${(count / stats.productCount) * 100}%` }}
                                    ></div>
                                </div>
                                <span className="text-gray-600 font-semibold w-8">{count}</span>
                            </div>
                        </div>
                    ))}
                </div>
            </div>

            {/* Charts - Lazy loaded to reduce initial bundle size */}
            <Suspense fallback={
                <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                    <div className="bg-white rounded-lg shadow p-6">
                        <div className="h-64 bg-gray-100 animate-pulse rounded flex items-center justify-center">
                            <p className="text-gray-400">Loading charts...</p>
                        </div>
                    </div>
                    <div className="bg-white rounded-lg shadow p-6">
                        <div className="h-64 bg-gray-100 animate-pulse rounded flex items-center justify-center">
                            <p className="text-gray-400">Loading charts...</p>
                        </div>
                    </div>
                </div>
            }>
                <DashboardCharts
                    products={products}
                    categoryDistribution={stats.categoryDistribution}
                />
            </Suspense>
        </div>
    );
};

export default Dashboard;