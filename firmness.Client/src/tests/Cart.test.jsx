import { describe, it, expect } from 'vitest';
import { renderHook, act } from '@testing-library/react';
import { CartProvider, useCart } from '../context/CartContext';

describe('CartContext', () => {
    it('should calculate cart totals correctly', () => {
        const wrapper = ({ children }) => <CartProvider>{children}</CartProvider>;
        const { result } = renderHook(() => useCart(), { wrapper });

        // Add a product to cart
        act(() => {
            result.current.addToCart({ id: 1, name: 'Test Product', price: 100 });
        });

        // Calculate totals
        const { subtotal, tax, total } = result.current.cartTotals();

        // Assertions
        expect(subtotal).toBe(100);
        expect(tax).toBe(19); // 19% tax
        expect(total).toBe(119);
    });

    it('should add products to cart and increment quantity', () => {
        const wrapper = ({ children }) => <CartProvider>{children}</CartProvider>;
        const { result } = renderHook(() => useCart(), { wrapper });

        const product = { id: 1, name: 'Test Product', price: 100 };

        // Add product twice
        act(() => {
            result.current.addToCart(product);
            result.current.addToCart(product);
        });

        // Check cart
        expect(result.current.cart).toHaveLength(1);
        expect(result.current.cart[0].quantity).toBe(2);

        // Calculate totals
        const { subtotal, tax, total } = result.current.cartTotals();
        expect(subtotal).toBe(200);
        expect(tax).toBe(38);
        expect(total).toBe(238);
    });
});
