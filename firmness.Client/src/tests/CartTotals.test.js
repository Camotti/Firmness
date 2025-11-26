import { describe, it, expect } from "vitest";

function calculateCartTotal(items) {
    return items.reduce((acc, item) => acc + item.price * item.quantity, 0);
}

describe("Cart total calculation", () => {

    it("should calculate the correct total", () => {
        const cart = [
            { price: 10, quantity: 2 },
            { price: 5, quantity: 1 },
            { price: 3, quantity: 3 },
        ];

        const total = calculateCartTotal(cart);

        expect(total).toBe(10*2 + 5*1 + 3*3); // 31
    });

});