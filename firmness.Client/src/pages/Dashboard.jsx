import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { ShoppingCart, Package, TrendingUp, DollarSign } from "lucide-react";
import { useCart } from "../context/CartContext";

export default function Dashboard() {
    const navigate = useNavigate();
    const { cart, cartTotals } = useCart();
    const { total } = cartTotals();
    const [user, setUser] = useState(null);

    useEffect(() => {
        const token = localStorage.getItem("token");
        if (!token) {
            navigate("/");
            return;
        }

        const storedUser = localStorage.getItem("user");
        if (storedUser) setUser(JSON.parse(storedUser));
    }, []);

    const stats = [
        {
            title: "Cart Total",
            value: `$${total.toFixed(2)}`,
            icon: ShoppingCart,
            color: "bg-blue-500",
            change: "+12.5%"
        },
        {
            title: "Items in Cart",
            value: cart.length,
            icon: Package,
            color: "bg-green-500",
            change: "+3"
        },
        {
            title: "Total Products",
            value: "8",
            icon: TrendingUp,
            color: "bg-purple-500",
            change: "+2 new"
        },
        {
            title: "Avg. Price",
            value: "$9,538",
            icon: DollarSign,
            color: "bg-orange-500",
            change: "+5.2%"
        }
    ];

    return (
        <div className="space-y-6">
            {/* HEADER */}
            <div>
                <h1 className="text-2xl font-bold text-gray-900">Dashboard</h1>
                <p className="text-gray-500">Welcome back, {user?.name || "User"}! Here's your overview.</p>
            </div>

            {/* STATS GRID */}
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
                {stats.map((stat, index) => (
                    <div key={index} className="bg-white rounded-xl border border-gray-100 p-6 hover:shadow-md transition-shadow">
                        <div className="flex items-center justify-between mb-4">
                            <div className={`${stat.color} p-3 rounded-lg`}>
                                <stat.icon size={24} className="text-white" />
                            </div>
                            <span className="text-sm text-green-600 font-medium">{stat.change}</span>
                        </div>
                        <h3 className="text-gray-500 text-sm mb-1">{stat.title}</h3>
                        <p className="text-2xl font-bold text-gray-900">{stat.value}</p>
                    </div>
                ))}
            </div>

            {/* RECENT ACTIVITY */}
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
                {/* Cart Summary */}
                <div className="bg-white rounded-xl border border-gray-100 p-6">
                    <h2 className="text-lg font-bold text-gray-900 mb-4">Cart Summary</h2>
                    {cart.length === 0 ? (
                        <div className="text-center py-8">
                            <ShoppingCart size={48} className="mx-auto text-gray-300 mb-2" />
                            <p className="text-gray-500">Your cart is empty</p>
                            <button
                                onClick={() => navigate('/products')}
                                className="mt-4 text-[#F97316] hover:underline font-medium"
                            >
                                Browse Products
                            </button>
                        </div>
                    ) : (
                        <div className="space-y-3">
                            {cart.slice(0, 3).map((item) => (
                                <div key={item.id} className="flex items-center gap-3 p-3 bg-gray-50 rounded-lg">
                                    <img
                                        src={item.imageUrl || "https://placehold.co/60x60"}
                                        alt={item.name}
                                        className="w-12 h-12 object-cover rounded"
                                    />
                                    <div className="flex-1">
                                        <p className="font-medium text-gray-900 text-sm">{item.name}</p>
                                        <p className="text-xs text-gray-500">Qty: {item.quantity}</p>
                                    </div>
                                    <p className="font-bold text-gray-900">${item.price.toFixed(2)}</p>
                                </div>
                            ))}
                            {cart.length > 3 && (
                                <button
                                    onClick={() => navigate('/cart')}
                                    className="w-full text-center text-[#F97316] hover:underline font-medium text-sm mt-2"
                                >
                                    View all {cart.length} items
                                </button>
                            )}
                        </div>
                    )}
                </div>

                {/* Quick Actions */}
                <div className="bg-white rounded-xl border border-gray-100 p-6">
                    <h2 className="text-lg font-bold text-gray-900 mb-4">Quick Actions</h2>
                    <div className="space-y-3">
                        <button
                            onClick={() => navigate('/products')}
                            className="w-full flex items-center gap-3 p-4 bg-[#FFF4E6] text-[#F97316] rounded-lg hover:bg-[#ffe8cc] transition-colors"
                        >
                            <Package size={20} />
                            <span className="font-medium">Browse Products</span>
                        </button>
                        <button
                            onClick={() => navigate('/cart')}
                            className="w-full flex items-center gap-3 p-4 bg-gray-50 text-gray-700 rounded-lg hover:bg-gray-100 transition-colors"
                        >
                            <ShoppingCart size={20} />
                            <span className="font-medium">View Cart</span>
                        </button>
                        <button className="w-full flex items-center gap-3 p-4 bg-gray-50 text-gray-700 rounded-lg hover:bg-gray-100 transition-colors">
                            <TrendingUp size={20} />
                            <span className="font-medium">View Reports</span>
                        </button>
                    </div>
                </div>
            </div>

            {/* WELCOME MESSAGE */}
            <div className="bg-gradient-to-r from-[#F97316] to-[#ea580c] rounded-xl p-8 text-white">
                <h2 className="text-2xl font-bold mb-2">Welcome to Firmness!</h2>
                <p className="text-white/90 mb-4">
                    Manage your construction materials and equipment with ease. Browse our catalog, add items to your cart, and checkout securely.
                </p>
                <button
                    onClick={() => navigate('/products')}
                    className="bg-white text-[#F97316] px-6 py-2 rounded-lg font-medium hover:bg-gray-100 transition-colors"
                >
                    Start Shopping
                </button>
            </div>
        </div>
    );
}
