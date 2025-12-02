import api from "../api/axiosInstance.js";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useCart } from "../context/CartContext.jsx";
import { Search, Plus, ChevronLeft, ChevronRight, ShoppingCart } from "lucide-react";
import AddProductModal from "../components/AddProductModal.jsx";

const MOCK_PRODUCTS = [
    { id: 1, name: "Portable Concrete Mixer 150L", price: 499.99, category: "Machinery", imageUrl: "https://images.unsplash.com/photo-1605218427368-35b019b8db5c?q=80&w=800&auto=format&fit=crop" },
    { id: 2, name: "Industrial Hammer Drill", price: 189.50, category: "Tools", imageUrl: "https://images.unsplash.com/photo-1504148455328-c376907d081c?q=80&w=800&auto=format&fit=crop" },
    { id: 3, name: "Compact Excavator 305", price: 25000.00, category: "Vehicles", imageUrl: "https://images.unsplash.com/photo-1542621334-a254cf47733d?q=80&w=800&auto=format&fit=crop" },
    { id: 4, name: "Modular Steel Scaffolding", price: 850.00, category: "Structures", imageUrl: "https://images.unsplash.com/photo-1503387762-592deb58ef4e?q=80&w=800&auto=format&fit=crop" },
    { id: 5, name: "Table Circular Saw", price: 320.00, category: "Tools", imageUrl: "https://images.unsplash.com/photo-1572981779307-38b8cabb2407?q=80&w=800&auto=format&fit=crop" },
    { id: 6, name: "Dump Truck 10T", price: 45000.00, category: "Vehicles", imageUrl: "https://images.unsplash.com/photo-1605218427368-35b019b8db5c?q=80&w=800&auto=format&fit=crop" },
    { id: 7, name: "Self-Leveling Laser Level", price: 250.99, category: "Tools", imageUrl: "https://images.unsplash.com/photo-1581092580497-e0d23cbdf1dc?q=80&w=800&auto=format&fit=crop" },
    { id: 8, name: "Electric Generator 5kW", price: 1200.00, category: "Machinery", imageUrl: "https://images.unsplash.com/photo-1455612693675-112974d4880b?q=80&w=800&auto=format&fit=crop" },
];

const ITEMS_PER_PAGE = 8;

export default function Products() {
    const [products, setProducts] = useState([]);
    const navigate = useNavigate();
    const { addToCart } = useCart();
    const [searchTerm, setSearchTerm] = useState("");
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        const token = localStorage.getItem("token");

        if (!token) {
            navigate("/");
            return;
        }

        if (token === "demo-token") {
            setProducts(MOCK_PRODUCTS);
            return;
        }

        api
            .get("/Products", { headers: { Authorization: "Bearer " + token } })
            .then((res) => setProducts(res.data))
            .catch((err) => {
                console.error("API Error, falling back to mock data", err);
                setProducts(MOCK_PRODUCTS);
            });
    }, []);

    const handleSaveProduct = (newProduct) => {
        setProducts([...products, newProduct]);
        console.log("New product added:", newProduct);
    };

    // Filter products based on search term
    const filteredProducts = products.filter(prod =>
        prod.name.toLowerCase().includes(searchTerm.toLowerCase())
    );

    // Pagination logic
    const totalPages = Math.ceil(filteredProducts.length / ITEMS_PER_PAGE);
    const startIndex = (currentPage - 1) * ITEMS_PER_PAGE;
    const endIndex = startIndex + ITEMS_PER_PAGE;
    const currentProducts = filteredProducts.slice(startIndex, endIndex);

    // Calculate total value
    const totalValue = products.reduce((acc, curr) => acc + curr.price, 0);

    // Reset to page 1 when search changes
    useEffect(() => {
        setCurrentPage(1);
    }, [searchTerm]);

    const goToPage = (page) => {
        if (page >= 1 && page <= totalPages) {
            setCurrentPage(page);
            window.scrollTo({ top: 0, behavior: 'smooth' });
        }
    };

    const renderPageNumbers = () => {
        const pages = [];
        const maxVisiblePages = 5;

        if (totalPages <= maxVisiblePages) {
            // Show all pages if total is small
            for (let i = 1; i <= totalPages; i++) {
                pages.push(i);
            }
        } else {
            // Show smart pagination
            if (currentPage <= 3) {
                pages.push(1, 2, 3, '...', totalPages);
            } else if (currentPage >= totalPages - 2) {
                pages.push(1, '...', totalPages - 2, totalPages - 1, totalPages);
            } else {
                pages.push(1, '...', currentPage - 1, currentPage, currentPage + 1, '...', totalPages);
            }
        }

        return pages;
    };

    return (
        <div className="space-y-6">
            {/* HEADER */}
            <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
                <div>
                    <h1 className="text-2xl font-bold text-gray-900">Products Panel</h1>
                    <p className="text-gray-500">Manage all construction tools and vehicles.</p>
                </div>
                <div className="flex items-center gap-6">
                    <div className="text-right hidden md:block">
                        <p className="text-sm text-gray-500">Total Stock Value</p>
                        <p className="text-xl font-bold text-[#F97316]">${totalValue.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</p>
                    </div>
                    <button
                        onClick={() => setIsModalOpen(true)}
                        className="bg-[#F97316] hover:bg-[#ea580c] text-white px-4 py-2 rounded-lg font-medium flex items-center gap-2 transition-colors shadow-sm"
                    >
                        <Plus size={20} />
                        Add New Product
                    </button>
                </div>
            </div>

            {/* SEARCH BAR */}
            <div className="relative">
                <Search className="absolute left-3 top-1/2 -translate-y-1/2 text-gray-400" size={20} />
                <input
                    type="text"
                    placeholder="Search by name, SKU..."
                    className="w-full pl-10 pr-4 py-3 rounded-lg border border-gray-200 focus:outline-none focus:ring-2 focus:ring-[#F97316] focus:border-transparent bg-white shadow-sm"
                    value={searchTerm}
                    onChange={(e) => setSearchTerm(e.target.value)}
                />
            </div>

            {/* PRODUCT GRID */}
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
                {currentProducts.map((prod) => (
                    <div key={prod.id} className="bg-white rounded-xl border border-gray-100 shadow-sm overflow-hidden hover:shadow-md transition-shadow flex flex-col">
                        <div className="h-48 bg-gray-100 relative group">
                            <img
                                src={prod.imageUrl || "https://placehold.co/400x300/e2e8f0/1e293b?text=No+Image"}
                                alt={prod.name}
                                className="w-full h-full object-cover"
                            />
                            {/* Overlay with Add to Cart button */}
                            <div className="absolute inset-0 bg-black/40 flex items-center justify-center opacity-0 group-hover:opacity-100 transition-opacity">
                                <button
                                    onClick={() => addToCart(prod)}
                                    className="bg-white text-gray-900 px-4 py-2 rounded-full font-medium flex items-center gap-2 hover:bg-gray-100 transform translate-y-2 group-hover:translate-y-0 transition-all"
                                >
                                    <ShoppingCart size={18} />
                                    Add to Cart
                                </button>
                            </div>
                        </div>
                        <div className="p-4 flex-1 flex flex-col">
                            <h3 className="font-semibold text-gray-900 mb-1 line-clamp-2">{prod.name}</h3>
                            <p className="text-sm text-gray-500 mb-3">{prod.category || "General"}</p>
                            <div className="mt-auto flex justify-between items-center">
                                <p className="text-lg font-bold text-gray-900">${prod.price.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</p>
                                <button
                                    onClick={() => addToCart(prod)}
                                    className="p-2 rounded-full bg-gray-50 text-gray-600 hover:bg-[#FFF4E6] hover:text-[#F97316] md:hidden"
                                >
                                    <ShoppingCart size={20} />
                                </button>
                            </div>
                        </div>
                    </div>
                ))}
            </div>

            {/* PAGINATION */}
            {totalPages > 1 && (
                <div className="flex justify-center items-center gap-2 mt-8">
                    <button
                        onClick={() => goToPage(currentPage - 1)}
                        disabled={currentPage === 1}
                        className="w-8 h-8 flex items-center justify-center rounded-full text-gray-400 hover:bg-gray-100 disabled:opacity-50 disabled:cursor-not-allowed"
                    >
                        <ChevronLeft size={16} />
                    </button>

                    {renderPageNumbers().map((page, index) => (
                        page === '...' ? (
                            <span key={`ellipsis-${index}`} className="text-gray-400 px-2">...</span>
                        ) : (
                            <button
                                key={page}
                                onClick={() => goToPage(page)}
                                className={`w-8 h-8 flex items-center justify-center rounded-full text-sm font-medium transition-colors ${currentPage === page
                                        ? "bg-[#F97316] text-white"
                                        : "text-gray-600 hover:bg-gray-100"
                                    }`}
                            >
                                {page}
                            </button>
                        )
                    ))}

                    <button
                        onClick={() => goToPage(currentPage + 1)}
                        disabled={currentPage === totalPages}
                        className="w-8 h-8 flex items-center justify-center rounded-full text-gray-400 hover:bg-gray-100 disabled:opacity-50 disabled:cursor-not-allowed"
                    >
                        <ChevronRight size={16} />
                    </button>
                </div>
            )}

            {/* ADD PRODUCT MODAL */}
            <AddProductModal
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                onSave={handleSaveProduct}
            />
        </div>
    );
}
