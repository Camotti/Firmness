import { Link, Outlet, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";

export default function Layout() {
    const navigate = useNavigate();
    const [user, setUser] = useState(null);

    useEffect(() => {
        const storedUser = localStorage.getItem("user");
        if (storedUser) setUser(JSON.parse(storedUser));
    }, []);

    const handleLogout = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("user");
        setUser(null);
        navigate("/login");
    };

    return (
        <div className="min-h-screen flex flex-col bg-gray-100">
            {/* NAVBAR */}
            <nav className="bg-blue-600 text-white p-4 flex justify-between items-center shadow-md">
                <div className="flex items-center gap-6">
                    <Link to="/products" className="font-semibold hover:text-gray-200 transition">Products</Link>
                    <Link to="/cart" className="font-semibold hover:text-gray-200 transition">Cart</Link>
                </div>

                <div className="flex items-center gap-4">
                    {user ? (
                        <>
                            <span className="font-medium">Hello, {user.name || user.email}</span>
                            <button
                                onClick={handleLogout}
                                className="bg-red-500 hover:bg-red-600 px-4 py-1 rounded transition"
                            >
                                Logout
                            </button>
                        </>
                    ) : (
                        <>
                            <Link to="/login" className="hover:text-gray-200 transition px-3 py-1 rounded bg-blue-500 hover:bg-blue-700">Login</Link>
                            <Link to="/register" className="hover:text-gray-200 transition px-3 py-1 rounded bg-green-500 hover:bg-green-700">Register</Link>
                        </>
                    )}
                </div>
            </nav>

            {/* MAIN CONTENT */}
            <main className="flex-1 p-6 md:p-12">
                <Outlet />
            </main>

            {/* FOOTER */}
            <footer className="bg-gray-800 text-white p-4 text-center mt-auto">
                &copy; {new Date().getFullYear()} Firmeza. All rights reserved.
            </footer>
        </div>
    );
}