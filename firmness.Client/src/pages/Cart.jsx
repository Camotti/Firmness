import { useCart } from "../context/CartContext";

export default function Cart() {
    const { cart, addToCart, cartTotals } = useCart();
    const { subtotal, tax, total } = cartTotals();

    return (
        <div className="max-w-3xl mx-auto mt-10 p-6 border rounded shadow">
            <h1 className="text-3xl font-bold mb-6">Shopping Cart</h1>

            {cart.length === 0 ? (
                <p>No items in cart</p>
            ) : (
                <>
                    {cart.map((item) => (
                        <div
                            key={item.id}
                            className="flex justify-between items-center border-b py-2"
                        >
                            <div>
                                <strong>{item.name}</strong> - ${item.price.toFixed(2)} x{" "}
                                {item.quantity}
                            </div>
                            <button
                                onClick={() => addToCart(item)}
                                className="px-2 py-1 bg-green-500 text-white rounded hover:bg-green-600"
                            >
                                +
                            </button>
                        </div>
                    ))}

                    <div className="mt-4 text-right">
                        <p>Subtotal: ${subtotal.toFixed(2)}</p>
                        <p>Tax (19%): ${tax.toFixed(2)}</p>
                        <h3 className="text-xl font-bold">Total: ${total.toFixed(2)}</h3>
                    </div>
                </>
            )}
        </div>
    );
}