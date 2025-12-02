import { Link, Outlet, useNavigate, useLocation } from "react-router-dom";
import { useEffect, useState } from "react";
import {
    LayoutDashboard,
    Wrench,
    ShoppingCart,
    Settings,
    HelpCircle,
    LogOut
} from "lucide-react";

export default function Layout() {
    const navigate = useNavigate();
    const location = useLocation();
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

    const navItems = [
        { name: "Dashboard", path: "/dashboard", icon: LayoutDashboard },
        { name: "Products", path: "/products", icon: Wrench },
        { name: "Cart", path: "/cart", icon: ShoppingCart },
    ];

    return (
        <div className="flex min-h-screen bg-white">
            {/* SIDEBAR */}
            <aside className="w-64 bg-white border-r border-gray-100 flex flex-col fixed h-full z-10">
                {/* LOGO */}
                <div className="p-6 flex items-center gap-3">
                    <div className="w-10 h-10 rounded-full bg-[#E3CBA3] flex items-center justify-center text-white font-bold text-xs">
                        FM
                    </div>
                    <div>
                        <h1 className="font-bold text-gray-900 leading-tight">Firmness</h1>
                        <p className="text-xs text-gray-500">Construction Management</p>
                    </div>
                </div>

                {/* NAVIGATION */}
                <nav className="flex-1 px-4 py-4 space-y-1">
                    {navItems.map((item) => {
                        const isActive = location.pathname === item.path;
                        return (
                            <Link
                                key={item.path}
                                to={item.path}
                                className={`flex items-center gap-3 px-4 py-3 rounded-lg text-sm font-medium transition-colors ${isActive
                                        ? "bg-[#FFF4E6] text-[#F97316]"
                                        : "text-gray-600 hover:bg-gray-50 hover:text-gray-900"
                                    }`}
                            >
                                <item.icon size={20} className={isActive ? "text-[#F97316]" : "text-gray-500"} />
                                {item.name}
                            </Link>
                        );
                    })}
                </nav>

                {/* BOTTOM ACTIONS */}
                <div className="p-4 border-t border-gray-100 space-y-1">
                    <Link to="/settings" className="flex items-center gap-3 px-4 py-3 rounded-lg text-sm font-medium text-gray-600 hover:bg-gray-50 hover:text-gray-900">
                        <Settings size={20} className="text-gray-500" />
                        Settings
                    </Link>
                    <Link to="/support" className="flex items-center gap-3 px-4 py-3 rounded-lg text-sm font-medium text-gray-600 hover:bg-gray-50 hover:text-gray-900">
                        <HelpCircle size={20} className="text-gray-500" />
                        Support
                    </Link>

                    {user && (
                        <button
                            onClick={handleLogout}
                            className="w-full flex items-center gap-3 px-4 py-3 rounded-lg text-sm font-medium text-red-600 hover:bg-red-50 mt-4"
                        >
                            <LogOut size={20} />
                            Logout
                        </button>
                    )}
                </div>
            </aside>

            {/* MAIN CONTENT */}
            <main className="flex-1 ml-64 p-8 bg-[#FAFAFA]">
                <Outlet />
            </main>
        </div>
    );
}