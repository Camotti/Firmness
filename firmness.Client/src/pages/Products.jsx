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

        if (!token) {
            navigate("/");
            return;
        }

        api
            .get("/Products", { headers: { Authorization: "Bearer " + token } })
            .then((res) => setProducts(res.data))
            .catch((err) => {
                if (err.response?.status === 401) {
                    localStorage.removeItem("token");
                    navigate("/");
                }
            });
    }, []);

    return (
        <div className="max-w-4xl mx-auto mt-10">
            <h1 className="text-3xl font-bold mb-6 text-center">Catalog</h1>
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
                {products.map((prod) => (
                    <div key={prod.id} className="border p-4 rounded shadow">
                        <h2 className="text-xl font-semibold mb-2">{prod.name}</h2>
                        <p className="mb-2">${prod.price.toFixed(2)}</p>
                        <button
                            onClick={() => addToCart(prod)}
                            className="bg-green-500 text-white px-4 py-2 rounded hover:bg-green-600"
                        >
                            Add to cart
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
}
