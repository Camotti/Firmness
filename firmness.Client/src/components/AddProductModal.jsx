import { X, Upload } from "lucide-react";
import { useState } from "react";

export default function AddProductModal({ isOpen, onClose, onSave }) {
    const [formData, setFormData] = useState({
        name: "",
        description: "",
        category: "",
        price: "",
        stock: "",
        imageUrl: ""
    });

    if (!isOpen) return null;

    const handleSubmit = (e) => {
        e.preventDefault();
        onSave({
            ...formData,
            id: Date.now(),
            price: parseFloat(formData.price) || 0,
            stock: parseInt(formData.stock) || 0
        });
        setFormData({ name: "", description: "", category: "", price: "", stock: "", imageUrl: "" });
        onClose();
    };

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    return (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
            <div className="bg-[#1a1a1a] rounded-xl max-w-3xl w-full max-h-[90vh] overflow-y-auto">
                {/* Header */}
                <div className="p-6 border-b border-gray-700">
                    <div className="flex items-center justify-between">
                        <div>
                            <h2 className="text-2xl font-bold text-white">Add New Product</h2>
                            <p className="text-gray-400 text-sm mt-1">Enter the details to register a new product in the inventory.</p>
                        </div>
                        <button onClick={onClose} className="text-gray-400 hover:text-white">
                            <X size={24} />
                        </button>
                    </div>
                </div>

                <form onSubmit={handleSubmit} className="p-6 space-y-6">
                    {/* Basic Information */}
                    <div>
                        <h3 className="text-white font-semibold mb-4">Basic Information</h3>

                        <div className="space-y-4">
                            <div>
                                <label className="block text-gray-300 text-sm mb-2">Product Name</label>
                                <input
                                    type="text"
                                    name="name"
                                    value={formData.name}
                                    onChange={handleChange}
                                    placeholder="E.g., Electric Demolition Hammer"
                                    className="w-full bg-[#2a2a2a] border border-gray-700 rounded-lg px-4 py-3 text-white placeholder-gray-500 focus:outline-none focus:border-[#F97316]"
                                    required
                                />
                            </div>

                            <div>
                                <label className="block text-gray-300 text-sm mb-2">Description</label>
                                <textarea
                                    name="description"
                                    value={formData.description}
                                    onChange={handleChange}
                                    placeholder="Describe the characteristics, specifications and uses of the product."
                                    rows={4}
                                    className="w-full bg-[#2a2a2a] border border-gray-700 rounded-lg px-4 py-3 text-white placeholder-gray-500 focus:outline-none focus:border-[#F97316] resize-none"
                                />
                            </div>
                        </div>
                    </div>

                    {/* Classification and Stock */}
                    <div>
                        <h3 className="text-white font-semibold mb-4">Classification and Stock</h3>

                        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                            <div>
                                <label className="block text-gray-300 text-sm mb-2">Category</label>
                                <select
                                    name="category"
                                    value={formData.category}
                                    onChange={handleChange}
                                    className="w-full bg-[#2a2a2a] border border-gray-700 rounded-lg px-4 py-3 text-white focus:outline-none focus:border-[#F97316]"
                                    required
                                >
                                    <option value="">Select a category</option>
                                    <option value="Tools">Tools</option>
                                    <option value="Machinery">Machinery</option>
                                    <option value="Vehicles">Vehicles</option>
                                    <option value="Structures">Structures</option>
                                </select>
                            </div>

                            <div>
                                <label className="block text-gray-300 text-sm mb-2">Price (€)</label>
                                <input
                                    type="number"
                                    name="price"
                                    value={formData.price}
                                    onChange={handleChange}
                                    placeholder="0.00"
                                    step="0.01"
                                    className="w-full bg-[#2a2a2a] border border-gray-700 rounded-lg px-4 py-3 text-white placeholder-gray-500 focus:outline-none focus:border-[#F97316]"
                                    required
                                />
                            </div>

                            <div>
                                <label className="block text-gray-300 text-sm mb-2">Stock Quantity</label>
                                <input
                                    type="number"
                                    name="stock"
                                    value={formData.stock}
                                    onChange={handleChange}
                                    placeholder="0"
                                    className="w-full bg-[#2a2a2a] border border-gray-700 rounded-lg px-4 py-3 text-white placeholder-gray-500 focus:outline-none focus:border-[#F97316]"
                                    required
                                />
                            </div>
                        </div>
                    </div>

                    {/* Multimedia */}
                    <div>
                        <h3 className="text-white font-semibold mb-4">Multimedia</h3>

                        <div>
                            <label className="block text-gray-300 text-sm mb-2">Product Images</label>
                            <div className="border-2 border-dashed border-gray-700 rounded-lg p-8 text-center bg-[#2a2a2a]">
                                <Upload size={48} className="mx-auto text-gray-500 mb-4" />
                                <p className="text-gray-400 mb-1">
                                    <span className="text-[#F97316] cursor-pointer hover:underline">Upload a file</span> or drag and drop
                                </p>
                                <p className="text-gray-500 text-sm">PNG, JPG, GIF up to 10MB</p>
                                <input
                                    type="text"
                                    name="imageUrl"
                                    value={formData.imageUrl}
                                    onChange={handleChange}
                                    placeholder="Or paste image URL here"
                                    className="mt-4 w-full bg-[#1a1a1a] border border-gray-700 rounded-lg px-4 py-2 text-white placeholder-gray-500 focus:outline-none focus:border-[#F97316]"
                                />
                            </div>
                        </div>
                    </div>

                    {/* Buttons */}
                    <div className="flex gap-3 justify-end pt-4">
                        <button
                            type="button"
                            onClick={onClose}
                            className="px-6 py-3 border border-gray-700 text-white rounded-lg hover:bg-[#2a2a2a] transition-colors"
                        >
                            Cancel
                        </button>
                        <button
                            type="submit"
                            className="px-6 py-3 bg-[#F97316] text-white rounded-lg hover:bg-[#ea580c] transition-colors font-medium"
                        >
                            Save Product
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}
