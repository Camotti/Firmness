import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import { loginRequest } from "../api/auth";

export default function Login() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [showPassword, setShowPassword] = useState(false);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
        try {
            const res = await loginRequest(email, password);
            localStorage.setItem("token", res.data.token);
            navigate("/products");
        } catch (error) {
            alert("Login failed: " + (error.response?.data?.message || error.message));
        }
    };

    return (
        <div className="flex min-h-screen w-full">
            {/* Left Side - Branding */}
            <div className="hidden w-1/2 flex-col justify-between bg-[#F5F7F9] p-12 lg:flex">
                <div className="text-xl font-bold text-gray-900">AdminPanel</div>
                <div className="mb-20">
                    <h1 className="mb-4 text-5xl font-bold leading-tight text-gray-900">
                        Empowering Your Business Data.
                    </h1>
                    <p className="max-w-md text-lg text-gray-500">
                        Manage, analyze, and visualize your data with unparalleled control and insight.
                    </p>
                </div>
                <div></div> {/* Spacer for alignment */}
            </div>

            {/* Right Side - Login Form */}
            <div className="flex w-full flex-col justify-center px-8 lg:w-1/2 lg:px-24">
                <div className="mx-auto w-full max-w-md">
                    <h2 className="mb-2 text-3xl font-bold text-gray-900">Welcome Back</h2>
                    <p className="mb-8 text-gray-500">Please enter your credentials to sign in.</p>

                    <form onSubmit={handleSubmit} className="flex flex-col gap-6">
                        <div className="flex flex-col gap-2">
                            <label className="font-medium text-gray-700">Email Address</label>
                            <input
                                type="email"
                                placeholder="Enter your email"
                                value={email}
                                onChange={e => setEmail(e.target.value)}
                                required
                                className="rounded-lg border border-gray-200 px-4 py-3 text-gray-900 outline-none focus:border-blue-500 focus:ring-1 focus:ring-blue-500"
                            />
                        </div>

                        <div className="flex flex-col gap-2">
                            <label className="font-medium text-gray-700">Password</label>
                            <div className="relative">
                                <input
                                    type={showPassword ? "text" : "password"}
                                    placeholder="Enter your password"
                                    value={password}
                                    onChange={e => setPassword(e.target.value)}
                                    required
                                    className="w-full rounded-lg border border-gray-200 px-4 py-3 text-gray-900 outline-none focus:border-blue-500 focus:ring-1 focus:ring-blue-500"
                                />
                                <button
                                    type="button"
                                    onClick={() => setShowPassword(!showPassword)}
                                    className="absolute right-4 top-1/2 -translate-y-1/2 text-gray-400 hover:text-gray-600"
                                >
                                    {showPassword ? (
                                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="h-5 w-5">
                                            <path strokeLinecap="round" strokeLinejoin="round" d="M3.98 8.223A10.477 10.477 0 001.934 12C3.226 16.338 7.244 19.5 12 19.5c.993 0 1.953-.138 2.863-.395M6.228 6.228A10.45 10.45 0 0112 4.5c4.756 0 8.773 3.162 10.065 7.498a10.523 10.523 0 01-4.293 5.774M6.228 6.228L3 3m3.228 3.228l3.65 3.65m7.894 7.894L21 21m-3.228-3.228l-3.65-3.65m0 0a3 3 0 10-4.243-4.243m4.242 4.242L9.88 9.88" />
                                        </svg>
                                    ) : (
                                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="h-5 w-5">
                                            <path strokeLinecap="round" strokeLinejoin="round" d="M2.036 12.322a1.012 1.012 0 010-.639C3.423 7.51 7.36 4.5 12 4.5c4.638 0 8.573 3.007 9.963 7.178.07.207.07.431 0 .639C20.577 16.49 16.64 19.5 12 19.5c-4.638 0-8.573-3.007-9.963-7.178z" />
                                            <path strokeLinecap="round" strokeLinejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />
                                        </svg>
                                    )}
                                </button>
                            </div>
                        </div>

                        <div className="flex justify-end">
                            <a href="#" className="text-sm font-medium text-blue-600 hover:text-blue-700">
                                Forgot Password?
                            </a>
                        </div>

                        <button
                            type="submit"
                            className="w-full rounded-lg bg-blue-600 py-3 font-semibold text-white transition-colors hover:bg-blue-700"
                        >
                            Sign In
                        </button>

                        <p className="text-center text-sm text-gray-500">
                            Don't have an account? <Link to="/register" className="font-semibold text-blue-600 hover:text-blue-700">Sign Up</Link>
                        </p>
                    </form>
                </div>
            </div>
        </div>
    );
}