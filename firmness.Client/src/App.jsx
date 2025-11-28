import {BrowserRouter, Routes, Route} from "react-router-dom";
import Register from "./pages/Register.jsx";
import Products from "./pages/Products";
import Cart from "./pages/Cart";
import Login from "./pages/Login";
import Layout from "./components/Layout";

export default function App() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Rutas SIN layout */}
                <Route path="/" element={<Login />} />
                <Route path="/register" element={<Register />} />

                {/* Rutas CON layout */}
                <Route element={<Layout />}>
                    <Route path="/products" element={<Products />} />
                    <Route path="/cart" element={<Cart />} />
                </Route>

            </Routes>
        </BrowserRouter>
    );
}