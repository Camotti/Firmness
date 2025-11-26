import { createContext, useContext, useState } from "react";

// 1️⃣ Crear el contexto
export const CartContext = createContext();

// 2️⃣ Hook personalizado para usar el contexto
export function useCart() {
    return useContext(CartContext);
}

// 3️⃣ Tu CartProvider EXACTO como lo enviaste (no modifiqué nada)
export function CartProvider({ children }) {
    const [cart, setCart] = useState([]);

    const addToCart = (product) => {
        setCart(prev => {
            const exists = prev.find(p => p.id === product.id);

            if (exists) {
                return prev.map(p => p.id === product.id ? { ...p, quantity: p.quantity + 1 } : p);
            }
            return [...prev, { ...product, quantity: 1 }];
        });
    };

    const cartTotals = () => {
        const subtotal = cart.reduce((acc, item) => acc + item.price * item.quantity, 0);
        const tax = subtotal * 0.19;
        const total = subtotal + tax;

        return { subtotal, tax, total };
    };

    return (
        <CartContext.Provider value={{ cart, addToCart, cartTotals }}>
            {children}
        </CartContext.Provider>
    );
}