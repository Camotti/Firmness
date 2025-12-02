import { useCart } from "../context/CartContext";
import { ShoppingCart, Plus, Minus, Trash2 } from "lucide-react";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

export default function Cart() {
    const { cart, addToCart, removeFromCart, clearCart, cartTotals } = useCart();
    const { subtotal, tax, total } = cartTotals();
    const navigate = useNavigate();
    const [showCheckoutModal, setShowCheckoutModal] = useState(false);

    const handleCheckout = () => {
        setShowCheckoutModal(true);
        // Simular proceso de checkout
        setTimeout(() => {
            alert(`Purchase completed! Total: $${total.toFixed(2)}\n\nA confirmation email has been sent to your email address.`);
            clearCart();
            setShowCheckoutModal(false);
            navigate('/dashboard');
        }, 2000);
    };

    return (
        <div className="space-y-6">
            {/* HEADER */}
            <div className="flex items-center justify-between">
                <div className="flex items-center gap-3">
                    <ShoppingCart size={28} className="text-[#F97316]" />
                    <h1 className="text-2xl font-bold text-gray-900">Shopping Cart</h1>
                </div>
                <button
                    onClick={() => navigate('/products')}
                    className="text-sm text-gray-600 hover:text-[#F97316] font-medium"
                >
                    ← Continue Shopping
                </button>
            </div>

            {cart.length === 0 ? (
                <div className="bg-white rounded-xl border border-gray-100 p-12 text-center">
                    <ShoppingCart size={64} className="mx-auto text-gray-300 mb-4" />
                    <h2 className="text-xl font-semibold text-gray-900 mb-2">Your cart is empty</h2>
                    <p className="text-gray-500 mb-6">Add some products to get started!</p>
                    <button
                        onClick={() => navigate('/products')}
                        className="bg-[#F97316] hover:bg-[#ea580c] text-white px-6 py-3 rounded-lg font-medium transition-colors"
                    >
                        Browse Products
                    </button>
                </div>
            ) : (
                <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
                    {/* CART ITEMS */}
                    <div className="lg:col-span-2 space-y-4">
                        {cart.map((item) => (
                            <div
                                key={item.id}
                                className="bg-white rounded-xl border border-gray-100 p-4 flex gap-4 hover:shadow-md transition-shadow"
                            >
                                <img
                                    src={item.imageUrl || "https://placehold.co/100x100/e2e8f0/1e293b?text=No+Image"}
                                    alt={item.name}
                                    className="w-24 h-24 object-cover rounded-lg"
                                />
                                <div className="flex-1">
                                    <h3 className="font-semibold text-gray-900 mb-1">{item.name}</h3>
                                    <p className="text-sm text-gray-500 mb-2">{item.category}</p>
                                    <p className="text-lg font-bold text-gray-900">${item.price.toFixed(2)}</p>
                                </div>
                                <div className="flex flex-col items-end justify-between">
                                    <div className="flex items-center gap-2 bg-gray-50 rounded-lg p-1">
                                        <button
                                            onClick={() => removeFromCart(item.id)}
                                            className="p-1 hover:bg-white rounded transition-colors"
                                        >
                                            <Minus size={16} className="text-[#F97316]" />
                                        </button>
                                        <span className="w-8 text-center font-medium">{item.quantity}</span>
                                        <button
                                            onClick={() => addToCart(item)}
                                            className="p-1 hover:bg-white rounded transition-colors"
                                        >
                                            <Plus size={16} className="text-[#F97316]" />
                                        </button>
                                    </div>
                                    <button
                                        onClick={() => removeFromCart(item.id)}
                                        className="text-red-500 hover:text-red-600 p-2"
                                    >
                                        <Trash2 size={18} />
                                    </button>
                                </div>
                            </div>
                        ))}
                    </div>

                    {/* ORDER SUMMARY */}
                    <div className="lg:col-span-1">
                        <div className="bg-white rounded-xl border border-gray-100 p-6 sticky top-6">
                            <h2 className="text-lg font-bold text-gray-900 mb-4">Order Summary</h2>

                            <div className="space-y-3 mb-4 pb-4 border-b border-gray-100">
                                <div className="flex justify-between text-gray-600">
                                    <span>Subtotal</span>
                                    <span className="font-medium">${subtotal.toFixed(2)}</span>
                                </div>
                                <div className="flex justify-between text-gray-600">
                                    <span>Tax (19%)</span>
                                    <span className="font-medium">${tax.toFixed(2)}</span>
                                </div>
                            </div>

                            <div className="flex justify-between text-lg font-bold text-gray-900 mb-6">
                                <span>Total</span>
                                <span className="text-[#F97316]">${total.toFixed(2)}</span>
                            </div>

                            <button
                                onClick={handleCheckout}
                                disabled={showCheckoutModal}
                                className="w-full bg-[#F97316] hover:bg-[#ea580c] text-white py-3 rounded-lg font-medium transition-colors disabled:opacity-50 disabled:cursor-not-allowed"
                            >
                                {showCheckoutModal ? "Processing..." : "Proceed to Checkout"}
                            </button>

                            <p className="text-xs text-gray-500 text-center mt-4">
                                Secure checkout with SSL encryption
                            </p>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}