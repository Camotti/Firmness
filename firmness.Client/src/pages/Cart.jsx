import {useCart} from "../context/cartcontext";

export default function Cart() {
    const {cart, cartTotals} = useCart();
    const {subtotal, tax, total} = cartTotals;
    
    return (
        <>
        <h1>Shopping Cart</h1>
            {cart.length === 0 && <p> No items in cart</p>}

            {cart.map(item => (
                <div key={item.id}>
                    {item.name} - ${item.price} * {item.quantity}
                </div>
                ))}
            
            <hr/>
            <p> Subtotal: ${subtotal.toFixed(2)}</p>
            <p> tax (19%): ${tax.toFixed(2)}</p>
            <h3> Total: ${total.toFixed(2)}</h3>
        </>
    );
}