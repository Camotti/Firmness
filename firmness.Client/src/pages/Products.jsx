import api from "../api/axiosInstance";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

export default function Products() {
    const [products, setProducts] = useState([]);
    const navigate = useNavigate();

    useEffect(() => {
        const token = localStorage.getItem("token");

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
                console.log(err);

                if (err.response?.status === 401) {
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
                    <strong>{prod.name}</strong> – ${prod.price}
                </div>
            ))}
        </>
    );
}