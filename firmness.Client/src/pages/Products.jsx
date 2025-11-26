import api from "../api/axiosInstance.js";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useCart } from "../context/CartContext.jsx";

export default function Products() {
    const [products, setProducts] = useState([]);
    const navigate = useNavigate();
    const { addToCart } = useCart();

    useEffect(() => {
        const token = localStorage.getItem("token");

        // Si no hay token → redirigir al login
        if (!token) {
            navigate("/");
            return;
        }

        api.get("/Products", {
            headers: {
                Authorization: "Bearer " + token
            }
        })
            .then(res => {
                setProducts(res.data);
            })
            .catch(err => {
                if (err.response?.status === 401) {
                    // Token inválido → limpiar y volver a login
                    localStorage.removeItem("token");
                    navigate("/");
                }
            });
    }, []);

    return (
        <>
            <h1>Catalog</h1>

            {products.map(prod => (
                <div key={prod.id}>
                    <strong>{prod.name}</strong>  ${prod.price}
                    <button onClick={() => addToCart(prod)}>Add to cart</button>
                </div>
            ))}
        </>
    );
}
