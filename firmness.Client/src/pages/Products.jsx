import api from "../api/axiosInstance";
import {useEffect, useState} from "react";

export default function Products() {
    const [products, setProducts] = useState([]);
    
    useEffect(() => {
       api.get("/Products").then((res) => {
           setProducts(res.data);
       });
    }, []);
    
    return (
        <>
        <h1>Catalog</h1>
            {products.map(p => (
                <div key={p.id}>{p.name}</div>
            ))}
        </>
    );
}