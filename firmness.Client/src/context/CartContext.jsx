import { createContext, useContext, useState } from "react";

// Crear contexto
export const CartContext = createContext();

// Hook para usar el contexto
export function useCart() {
    return useContext(CartContext);
}

// Provider
export function CartProvider({ children }) {
    const [cart, setCart] = useState([]);

    const addToCart = (product) => {
        setCart((prev) => {
            const exists = prev.find((p) => p.id === product.id);
            if (exists) {
                return prev.map((p) =>
                    p.id === product.id ? { ...p, quantity: p.quantity + 1 } : p
                );
            }
            return [...prev, { ...product, quantity: 1 }];
        });
    };

    const removeFromCart = (productId) => {
        setCart((prev) => {
            const exists = prev.find((p) => p.id === productId);
            if (exists && exists.quantity > 1) {
                // Disminuir cantidad
                return prev.map((p) =>
                    p.id === productId ? { ...p, quantity: p.quantity - 1 } : p
                );
            } else {
                // Eliminar del carrito
                return prev.filter((p) => p.id !== productId);
            }
        });
    };

    const clearCart = () => {
        setCart([]);
    };

    const cartTotals = () => {
        const subtotal = cart.reduce((acc, item) => acc + item.price * item.quantity, 0);
        const tax = subtotal * 0.19;
        const total = subtotal + tax;
        return { subtotal, tax, total };
    };

    return (
        <CartContext.Provider value={{ cart, addToCart, removeFromCart, clearCart, cartTotals }}>
            {children}
        </CartContext.Provider>
    );
}