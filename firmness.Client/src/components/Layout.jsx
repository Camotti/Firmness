import { Link } from "react-router-dom";
import { Outlet } from "react-router-dom";

export default function Layout({ children }) {
    const user = JSON.parse(localStorage.getItem("user"));

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        window.location.href = "/login";
    };

    return (
        <div>
            {/* NAVBAR */}
            <nav className="bg-blue-600 text-white p-4 flex justify-between">
                <div className="flex gap-4">
                    <Link to="/products" className="hover:underline">Products</Link>
                    <Link to="/cart" className="hover:underline">Cart</Link>
                </div>

                {/* Si el usuario está logueado, mostrar su nombre */}
                <div className="flex items-center gap-4">
                    {user ? (
                        <>
                            <span>Hello, {user.name}</span>
                            <button
                                onClick={handleLogout}
                                className="bg-red-500 px-3 py-1 rounded"
                            >
                                Logout
                            </button>
                        </>
                    ) : (
                        <>
                            <Link to="/login" className="hover:underline">Login</Link>
                            <Link to="/register" className="hover:underline">Register</Link>
                        </>
                    )}
                </div>
            </nav>

            {/* CONTENIDO */}
            <main className="p-4">{children}</main>
        </div>
    );
}
